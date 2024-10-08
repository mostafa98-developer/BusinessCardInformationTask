import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PagesRoutingModule } from './pages-routing.module';
import { MatStepperModule } from '@angular/material/stepper';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatButtonModule } from '@angular/material/button';
import { ReactiveFormsModule } from '@angular/forms';
import { BusinessCardInformationManageComponent } from './business-card-information-manage/business-card-information-manage.component';
import { BusinessCardInformationListComponent } from './business-card-information-list/business-card-information-list.component';
import { MatTableModule } from '@angular/material/table';
import { MatIcon } from '@angular/material/icon';
import { FormsModule } from '@angular/forms';  // Import FormsModule
import { ImportBusinessCardsFromFileComponent } from './import-business-cards-from-file/import-business-cards-from-file.component';
import { MatDialogModule } from '@angular/material/dialog';
import { MatSnackBarModule } from '@angular/material/snack-bar'; // Import Material Snackbar

@NgModule({
  declarations: [
    BusinessCardInformationManageComponent,
    BusinessCardInformationListComponent,
    ImportBusinessCardsFromFileComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    PagesRoutingModule,
     // other modules
     ReactiveFormsModule,
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
     MatSnackBarModule
  ],
})
export class PagesModule { }
