import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ConfirmationDialogComponent } from './dialogs/confirmation-dialog/confirmation-dialog.component';
import { PhotoUploadComponent } from './photo-upload/photo-upload.component';
import { MatButtonModule } from '@angular/material/button';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIcon } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatStepperModule } from '@angular/material/stepper';
import { MatTableModule } from '@angular/material/table';
import { HeaderComponent } from './header/header.component';
import { MatMenuModule } from '@angular/material/menu'; // Import MatMenuModule
import { MatToolbarModule } from '@angular/material/toolbar'; // Import MatToolbarModule
import { RouterModule } from '@angular/router';  // <-- Import RouterModule



@NgModule({
  declarations: [ConfirmationDialogComponent,PhotoUploadComponent,HeaderComponent],
  imports: [
    CommonModule,
    MatStepperModule,
     MatInputModule,
     MatFormFieldModule,
     MatSelectModule,
     MatDatepickerModule,
     MatNativeDateModule,
     MatButtonModule,
     MatTableModule,
     MatIcon,
     MatDialogModule,
     MatSnackBarModule,
     MatToolbarModule, // Add MatToolbarModule here
     MatMenuModule,
     RouterModule
  ],
  exports: [
    MatStepperModule,
    MatInputModule,
    MatFormFieldModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatButtonModule,
    MatTableModule,
    MatIcon,
    MatDialogModule,
    MatSnackBarModule,
    MatToolbarModule, // Add MatToolbarModule here
    MatMenuModule,
    PhotoUploadComponent,
    HeaderComponent,
    RouterModule]
})
export class SharedModule { }
