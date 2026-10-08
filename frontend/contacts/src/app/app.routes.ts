import { Routes } from '@angular/router';
import { ContactListComponent } from './features/contacts/components/contact-list/contact-list.component';
import { ContactFormComponent } from './features/contacts/components/contact-form/contact-form.component';

export const routes: Routes = [
  { path: '', redirectTo: 'contacts', pathMatch: 'full' },
  { path: 'contacts', component: ContactListComponent },
  { path: 'contacts/new', component: ContactFormComponent },
  { path: 'contacts/:id/edit', component: ContactFormComponent },
  { path: '**', redirectTo: 'contacts' },
];
