import { DestroyRef, Component, computed, inject, signal } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Store } from '@ngxs/store';
import { PhoneType } from '../../models/phone-number.model';
import { InputText } from 'primeng/inputtext';
import { DatePicker } from 'primeng/datepicker';
import { Select } from 'primeng/select';
import { Checkbox } from 'primeng/checkbox';
import { Button } from 'primeng/button';
import { Message } from 'primeng/message';
import { Contact } from '../../models/contact.model';
import { CreateContact, LoadContactById, UpdateContact } from '../../state/contact.actions';
import { SaveContactRequest } from '../../dtos/contacts.dto';
import { ContactState } from '../../state/contact.state';
import { finalize } from 'rxjs';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

type PhoneForm = {
  number: FormControl<string>;
  type: FormControl<PhoneType>;
  isPrimary: FormControl<boolean>;
};

type ContactForm = {
  firstName: FormControl<string>;
  surname: FormControl<string>;
  dateOfBirth: FormControl<Date | null>;
  country: FormControl<string>;
  city: FormControl<string>;
  postalCode: FormControl<string>;
  street: FormControl<string>;
  iban: FormControl<string>;
  phoneNumbers: FormArray<FormGroup<PhoneForm>>;
};

@Component({
  imports: [
    ReactiveFormsModule,
    InputText,
    DatePicker,
    Select,
    Checkbox,
    Button,
    Message
  ],
  selector: 'app-contact-form',
  styleUrl: './contact-form.component.css',
  templateUrl: './contact-form.component.html',
})
export class ContactFormComponent {
  private readonly fb = inject(FormBuilder);  
  private readonly store = inject(Store);
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);
  private readonly destroyRef = inject(DestroyRef);

  readonly contactId = signal<number | null>(null);
  readonly isEditMode = computed(() => this.contactId() !== null);
  readonly saving = signal(false);
  readonly loadError = signal<string | null>(null);

  readonly phoneTypes: { label: string; value: PhoneType }[] = [
    { label: 'Mobile', value: 'Mobile' },
    { label: 'Home', value: 'Home' },
    { label: 'Work', value: 'Work' }
  ];

  readonly today = new Date();

  readonly form: FormGroup<ContactForm> = this.fb.group<ContactForm>({    
    firstName: this.fb.nonNullable.control('', [
      Validators.required,
      Validators.maxLength(100)
    ]),
    surname: this.fb.nonNullable.control('', [
      Validators.required,
      Validators.maxLength(100)
    ]),
    dateOfBirth: this.fb.control<Date | null>(new Date(), 
      Validators.required),
    country: this.fb.nonNullable.control('', [
      Validators.required,
      Validators.maxLength(100)
    ]),
    city: this.fb.nonNullable.control('', [
      Validators.required,
      Validators.maxLength(100)
    ]),
    postalCode: this.fb.nonNullable.control('', [
      Validators.required,
      Validators.maxLength(20)
    ]),
    street: this.fb.nonNullable.control('', [
      Validators.required,
      Validators.maxLength(200)
    ]),
    iban: this.fb.nonNullable.control('', [
      Validators.maxLength(34)
    ]),
    phoneNumbers: this.fb.array<FormGroup<PhoneForm>>([])
  });  

  get phoneNumbers(): FormArray<FormGroup<PhoneForm>> {
    return this.form.controls.phoneNumbers;
  }
  
  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    if (id) {
      this.contactId.set(id);
      this.store.dispatch(new LoadContactById(id)).subscribe({
        next: () => {
          const contact = this.store.selectSnapshot(
            ContactState.selectedContact
          );

          if (contact) {
            this.populateForm(contact);
          } else {
            this.loadError.set('Contact could not be loaded.');
          }
        },
        error: () => this.loadError.set('Failed to load contact.')
      });
    } else {
      this.addPhone();
    }

    this.form.valueChanges
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe(() => {
        if (this.loadError() !== null) {
          this.loadError.set(null);
        }
      });    
  }
  
  private createPhoneGroup(
    phone?: Partial<{
      number: string;
      type: PhoneType;
      isPrimary: boolean;
    }>
  ): FormGroup<PhoneForm> {
    return this.fb.group<PhoneForm>({
      number: this.fb.nonNullable.control(phone?.number ?? '', [
        Validators.required,
        Validators.maxLength(50)
      ]),
      type: this.fb.nonNullable.control(
        phone?.type ?? 'Mobile',
        Validators.required
      ),
      isPrimary: this.fb.nonNullable.control(phone?.isPrimary ?? false)
    });
  }

  addPhone(): void {
    this.phoneNumbers.push(this.createPhoneGroup());
  }

  removePhone(index: number): void {
    this.phoneNumbers.removeAt(index);
  }

  setPrimaryPhone(index: number, checked: boolean): void {
    if (!checked) {
      return;
    }

    this.phoneNumbers.controls.forEach((group, i) => {
      group.controls.isPrimary.setValue(i === index);
    });
  }

  private populateForm(contact: Contact): void {
    this.form.patchValue({
      firstName: contact.firstName,
      surname: contact.surname,
      dateOfBirth: new Date(contact.dateOfBirth),
      country: contact.country,
      city: contact.city,
      postalCode: contact.postalCode,
      street: contact.street,
      iban: contact.iban
    });

    this.phoneNumbers.clear();
    contact.phoneNumbers.forEach(phone => {
      this.phoneNumbers.push(this.createPhoneGroup(phone));
    });
  }

  save(): void {
    if (this.form.invalid || this.saving()) {
      this.form.markAllAsTouched();
      return;
    }

    this.saving.set(true);

    const value = this.form.getRawValue();

    const request: SaveContactRequest = {
      id: this.isEditMode() ? this.contactId()! : 0,
      firstName: value.firstName.trim(),
      surname: value.surname.trim(),
      dateOfBirth: value.dateOfBirth
        ? this.toDateOnlyString(value.dateOfBirth)
        : null,
      country: value.country.trim(),
      city: value.city.trim(),
      postalCode: value.postalCode.trim(),
      street: value.street.trim(),
      iban: value.iban.trim(),
      phoneNumbers: value.phoneNumbers.map(phone => ({
        number: phone.number.trim(),
        type: phone.type,
        isPrimary: phone.isPrimary
      }))
    };

    const action = this.isEditMode()
      ? new UpdateContact(request)
      : new CreateContact(request);

    this.store.dispatch(action)
      .pipe(finalize(() => this.saving.set(false)))
      .subscribe({
        next: (data: unknown) => {
          const stateError = this.store.selectSnapshot(ContactState.error);
          if (stateError) {
            this.loadError.set(stateError);
          } else {
            this.router.navigate(['/contacts'])
          }
        },
        error: () => this.loadError.set('Failed to save contact.')
      });
  }

  cancel(): void {
    this.router.navigate(['/contacts']);
  }

  private toDateOnlyString(date: Date): string {
    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const day = String(date.getDate()).padStart(2, '0');

    return `${year}-${month}-${day}`;
  }  
}
