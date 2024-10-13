import { HttpEvent, HttpHandler, HttpHandlerFn, HttpInterceptor, HttpInterceptorFn, HttpRequest } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { finalize } from 'rxjs/operators';
import { LoaderService } from './loader.service';


export const InterceptorService: HttpInterceptorFn =(req: HttpRequest<any>, next: HttpHandlerFn): Observable<HttpEvent<any>> => {
  const loadrServices = inject(LoaderService);

  loadrServices.isLoading.next(true);
    return next(req).pipe(
      finalize(
        () => {
          loadrServices.isLoading.next(false);
        }
      )
    );
};
