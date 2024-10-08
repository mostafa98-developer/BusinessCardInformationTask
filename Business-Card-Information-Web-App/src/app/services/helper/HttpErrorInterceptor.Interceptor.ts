// src/app/core/interceptors/http-error.interceptor.ts
import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { MatSnackBar } from '@angular/material/snack-bar';  // Import for showing user-friendly messages
import { Router } from '@angular/router';

@Injectable({providedIn: 'root'})
export class HttpErrorInterceptor implements HttpInterceptor {

  constructor(private snackBar: MatSnackBar, private router: Router) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      catchError((error: HttpErrorResponse) => {
        let errorMessage = '';

        // Handle different error types
        if (error.error instanceof ErrorEvent) {
          // Client-side error
          errorMessage = `Client Error: ${error.error.message}`;
        } else {
          // Server-side error
          errorMessage = `Server Error: ${error.status} - ${error.message}`;
        }

        // Show a snackbar with the error message
        this.snackBar.open(errorMessage, 'Close', {
          duration: 5000,
          panelClass: ['error-snackbar']
        });

        // Handle specific status codes
        if (error.status === 401) {
          // Unauthorized, navigate to login page or show message
          this.router.navigate(['/login']);
        } else if (error.status === 404) {
          // Not found
          this.snackBar.open('Resource not found', 'Close', {
            duration: 5000,
            panelClass: ['error-snackbar']
          });
        }

        // Rethrow the error to allow further handling if needed
        return throwError(() => new Error(errorMessage));
      })
    );
  }
}
