import { HttpInterceptorFn } from '@angular/common/http';
import { HttpErrorResponse, HttpRequest, HttpHandlerFn, HttpEvent } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ServiceResult } from '../../models/common/serviceResult.common';
import { ResultCode } from '../../shared/Enums/ResultCode.enums';
import { Error } from '../../models/common/error.common';
import { inject } from '@angular/core'; // Import inject
import { MatSnackBar } from '@angular/material/snack-bar';

export const httpErrorInterceptor: HttpInterceptorFn = (req: HttpRequest<any>, next: HttpHandlerFn): Observable<HttpEvent<any>> => {
  const snackBar = inject(MatSnackBar);

  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      const result = new ServiceResult(ResultCode.Ok);
      let message = 'An unknown error occurred';

      if (error.error) {
        // Handle specific error based on error structure
        if (typeof error.error === 'string') {
          message = error.error; // Assume error is a string message
        } else {
          message = error.message; // Fallback to the general message
        }
        result.addError(new Error('ServerError', message));
      } else {
        message = error.message; // For errors without specific payload
        result.addError(new Error('ServerError', message));
      }

      // Show the error message in a Snackbar
      snackBar.open(message, 'Close', {
        duration: 3000,
        verticalPosition: 'top',
        horizontalPosition: 'right',
      });

      return throwError(() => result);
    })
  );
};
