import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ErrorResponse, GetContactResponse, GetContactsSummaryResponse, SaveContactRequest } from '../dtos/contacts.dto';
import { HttpClient, HttpParams } from '@angular/common/http';
import { API_BASE_URL } from '../../../core/tokens/api-url.token';

@Injectable({
  providedIn: 'root',
})
export class ContactService {
  private readonly http = inject(HttpClient);
  private readonly apiBaseUrl = inject(API_BASE_URL);

  getContacts(page: number, pageSize: number): Observable<GetContactsSummaryResponse> {
    const params = new HttpParams()
      .set('page', page)
      .set('pageSize', pageSize);

    const url = `${this.apiBaseUrl}/contacts`;
    return this.http.get<GetContactsSummaryResponse>(url, { params });
  }

  getContactById(id: number): Observable<GetContactResponse> {
    const url = `${this.apiBaseUrl}/contacts/${id}`;
    return this.http.get<GetContactResponse>(url);
  }

  createContact(contact: SaveContactRequest): Observable<ErrorResponse> {
    const url = `${this.apiBaseUrl}/contacts`;
    return this.http.post<GetContactResponse>(url, contact);
  }  

  updateContact(contact: SaveContactRequest): Observable<ErrorResponse> {
    const url = `${this.apiBaseUrl}/contacts`;
    return this.http.put<GetContactResponse>(url, contact);
  }  
}
