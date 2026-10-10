import { Component, inject } from '@angular/core';
import { AsyncPipe } from '@angular/common';
import { TableLazyLoadEvent, TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { Store } from '@ngxs/store';
import { Observable } from 'rxjs';
import { ContactSummary } from '../../models/contact.model';
import { ContactState } from '../../state/contact.state';
import { LoadContacts } from '../../state/contact.actions';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { MenuItem } from 'primeng/api';
import { Menu } from 'primeng/menu';
import { Button } from 'primeng/button';

@Component({
  imports: [
    AsyncPipe,
    TableModule, 
    ButtonModule, 
    CardModule, 
    ProgressSpinnerModule,
    Menu,
    Button
  ],
  selector: 'app-contact-list',
  styleUrl: './contact-list.component.css',
  templateUrl: './contact-list.component.html',
})
export class ContactListComponent {
  private store = inject(Store);
  
  contacts$: Observable<ContactSummary[]> =
    this.store.select(ContactState.contacts);

  loading$ = this.store.select(ContactState.loading);

  error$ = this.store.select(ContactState.error);

  pagination$ = this.store.select(ContactState.pagination);

  menuItems: MenuItem[] = [];

  ngOnInit() {
  }

  onLazyLoad(event: TableLazyLoadEvent): void {
    const pageSize = event.rows ?? 10;
    const page = Math.floor((event.first ?? 0) / pageSize) + 1;

    this.store.dispatch(new LoadContacts(page, pageSize));
  }

  openMenu(event: MouseEvent, contact: ContactSummary, menu: Menu): void {
    this.menuItems = [
      {
        label: 'View',
        icon: 'pi pi-eye',
        command: () => this.viewContact(contact)
      },
      {
        label: 'Edit',
        icon: 'pi pi-pencil',
        command: () => this.editContact(contact)
      },
      {
        separator: true
      },
      {
        label: 'Delete',
        icon: 'pi pi-trash',
        command: () => this.deleteContact(contact)
      }
    ];

    menu.toggle(event);
  }

  viewContact(contact: ContactSummary): void {
    console.log('View contact:', contact.id);
  }

  editContact(contact: ContactSummary): void {
    console.log('Edit contact:', contact.id);
  }

  deleteContact(contact: ContactSummary): void {
    console.log('Delete contact:', contact.id);
  } 
  
  addContact(): void {
    console.log('Add new contact');
  }
}
