import { ApplicationConfig, provideBrowserGlobalErrorListeners } from '@angular/core';
import { provideRouter } from '@angular/router';
import { routes } from './app.routes';
import { providePrimeNG } from 'primeng/config';
import Aura from '@primeuix/themes/aura';
import { provideStore } from '@ngxs/store';
import { API_BASE_URL } from './core/tokens/api-url.token';
import { ContactState } from './features/contacts/state/contact.state';

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideRouter(routes),
    providePrimeNG({
      theme: {
        preset: Aura,
      },
    }),
    provideStore([ContactState]),
    {
      provide: API_BASE_URL,
      useValue: 'https://localhost:7266/api'
    }
  ],
};
