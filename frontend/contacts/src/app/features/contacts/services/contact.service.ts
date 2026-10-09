import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { GetContactsSummaryResponse } from '../dtos/contacts.dto';
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
}
