import { Component } from '@angular/core';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { ContactSummaryDto } from '../../dtos/contact-summary.dto';

@Component({
  imports: [TableModule, ButtonModule, CardModule],
  selector: 'app-contact-list',
  styleUrl: './contact-list.component.css',
  templateUrl: './contact-list.component.html',
})
export class ContactListComponent {
  contacts!: ContactSummaryDto[];

  ngOnInit() {
    this.contacts = [
      {
        firstName: 'John',
        surname: 'Doe',
        country: 'USA',
        city: 'New York',
        primaryPhoneNumber: '+1 123-456-7890',
      },
      {
        firstName: 'Jane',
        surname: 'Smith',
        country: 'Canada',
        city: 'Toronto',
        primaryPhoneNumber: '+1 987-654-3210',
      },
      {
        firstName: 'Michael',
        surname: 'Johnson',
        country: 'UK',
        city: 'London',
        primaryPhoneNumber: '+44 20 1234 5678',
      },
    ];
  }
}
