import { ApplicationConfig, provideBrowserGlobalErrorListeners } from '@angular/core';
import { provideRouter } from '@angular/router';
import { routes } from './app.routes';
import { providePrimeNG } from 'primeng/config';
import Aura from '@primeuix/themes/aura';
//import Lara from '@primeuix/themes/lara';
//import Nora from '@primeng/themes/nora';
//import Material from '@primeng/themes/material';

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideRouter(routes),
    providePrimeNG({
      theme: {
            preset: Aura,
//            preset: Lara,
//            preset: Nora,
//            preset: Material
        }
    })
  ]
};
