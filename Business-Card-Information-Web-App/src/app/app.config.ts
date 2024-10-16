import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideClientHydration } from '@angular/platform-browser';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptors } from '@angular/common/http';
import { httpErrorInterceptor } from './services/helper/HttpErrorInterceptor.Interceptor';
import { InterceptorService } from './shared/loader/interceptor.service';

export const appConfig: ApplicationConfig = {
  providers: [provideZoneChangeDetection({ eventCoalescing: true }),
  provideRouter(routes),
  provideClientHydration(),
  provideAnimationsAsync(),
  provideHttpClient(withInterceptors([httpErrorInterceptor,InterceptorService])),
  provideAnimationsAsync() ],

};


