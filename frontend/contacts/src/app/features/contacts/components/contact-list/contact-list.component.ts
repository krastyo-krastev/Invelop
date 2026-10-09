import { Component, inject } from '@angular/core';
import { AsyncPipe } from '@angular/common';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { Store } from '@ngxs/store';
import { Observable } from 'rxjs';
import { ContactSummary } from '../../models/contact.model';
import { ContactState } from '../../state/contact.state';
import { LoadContacts } from '../../state/contact.actions';
import { ProgressSpinnerModule } from 'primeng/progressspinner';

@Component({
  imports: [
    AsyncPipe,
    TableModule, 
    ButtonModule, 
    CardModule, 
    ProgressSpinnerModule
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

  ngOnInit() {
    this.store.dispatch(new LoadContacts(1, 10));
  }
}
