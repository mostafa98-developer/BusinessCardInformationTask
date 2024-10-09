// confirmation-dialog.service.ts
import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { ConfirmationDialogComponent } from '../../shared/dialogs/confirmation-dialog/confirmation-dialog.component';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root',
})
export class NotificationService  {

  constructor(private dialog: MatDialog,private snackBar: MatSnackBar) {}

  public confirm(message: string): Observable<boolean> {
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      width: '300px',
      data: { message },
    });

    return dialogRef.afterClosed();
  }


  showSuccess(message: string): void {
    this.snackBar.open(message, 'Close', {
      duration: 3000,
      panelClass: ['snack-bar-success'],
      horizontalPosition: 'right',
      verticalPosition: 'top',
    });
  }


  showError(message: string): void {
    this.snackBar.open(message, 'Close', {
      duration: 3000,
      panelClass: ['snack-bar-error'],
      horizontalPosition: 'right',
      verticalPosition: 'top',
    });
  }
}
