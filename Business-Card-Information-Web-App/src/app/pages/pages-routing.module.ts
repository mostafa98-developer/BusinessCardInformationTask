import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BusinessCardInformationManageComponent } from './business-card-information-manage/business-card-information-manage.component';
import { BusinessCardInformationListComponent } from './business-card-information-list/business-card-information-list.component';

const routes: Routes = [
  { path: '', redirectTo: 'business-bard-information-list', pathMatch: 'full' },
  { path: 'business-bard-information-manage', component: BusinessCardInformationManageComponent },
  { path: 'business-bard-information-list', component: BusinessCardInformationListComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PagesRoutingModule { }
