import { Injectable } from '@angular/core';
import { State, Action, StateContext, Selector } from '@ngxs/store';
import { EMPTY, tap, catchError } from 'rxjs';

import { ContactService } from '../services/contact.service';
import { ContactSummary } from '../models/contact.model';
import { LoadContacts } from './contact.actions'
import { HttpErrorResponse } from '@angular/common/http';

export interface ContactStateModel {
  contacts: ContactSummary[];
  loading: boolean;
  error: string | null;
}

@State<ContactStateModel>({
  name: 'contact',
  defaults: {
    contacts: [],
    loading: false,
    error: null
  }
})
@Injectable()
export class ContactState {
  constructor(private contactService: ContactService) {}

  @Selector()
  static contacts(state: ContactStateModel) {
    return state.contacts;
  }

  @Selector()
  static loading(state: ContactStateModel) {
    return state.loading;
  }

  @Selector()
  static error(state: ContactStateModel) {
    return state.error;
  }

  @Action(LoadContacts)
  loadContacts(
    ctx: StateContext<ContactStateModel>,
    action: LoadContacts
  ) {
    ctx.patchState({ loading: true, error: null });

    return this.contactService
      .getContacts(action.page, action.pageSize)
      .pipe(
        tap(response => {
          ctx.patchState({
            contacts: response.data,
            loading: false
          });
        }),
        catchError((error: HttpErrorResponse) => {
          ctx.patchState({
            loading: false,
            error: this.getErrorMessage(error)
          });

          return EMPTY;
        })
      );
  }

  private getErrorMessage(error: HttpErrorResponse): string {
    if (error.status === 0) {
        return 'Unable to connect to the server.';
    }

    if (error.status >= 500) {
        return 'A server error occurred. Please try again later.';
    }

    return error.error?.message ?? 'Failed to load contacts.';
  }  
}