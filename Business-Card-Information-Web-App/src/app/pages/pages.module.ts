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
import { SharedModule } from '../shared/shared.module';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';

@NgModule({
  declarations: [
    BusinessCardInformationManageComponent,
    BusinessCardInformationListComponent,
    ImportBusinessCardsFromFileComponent,
    PageNotFoundComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    PagesRoutingModule,
     // other modules
     ReactiveFormsModule,

     SharedModule
  ],
})
export class PagesModule { }
