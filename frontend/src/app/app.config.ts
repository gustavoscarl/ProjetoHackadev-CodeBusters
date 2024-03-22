import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { BrowserModule, provideClientHydration } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS, withInterceptors,provideHttpClient, withFetch, HttpClientModule } from '@angular/common/http';
import { provideEnvironmentNgxMask } from 'ngx-mask';

import { CookieService } from 'ngx-cookie-service';
import { TokenInterceptor } from './servicos/interceptors/token.interceptor';

export const appConfig: ApplicationConfig = {
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    },
    importProvidersFrom(BrowserModule, HttpClientModule),
    provideRouter(routes),
    provideClientHydration(), 
    provideHttpClient(withFetch()),
    provideEnvironmentNgxMask()]
};
