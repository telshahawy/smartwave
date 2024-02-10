import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CountriesListComponent } from './countries-list/countries-list.component';
import { ClientGuard } from '../../core/auth/guards/client.guard';
import { CreatecountryComponent } from './createcountry/createcountry.component';
import { GovernoratesCreateComponent } from './governorates-create/governorates-create.component';
import { GovernoratesListComponent } from './governorates-list/governorates-list.component';
import { AgeSegmentListComponent } from './age-segment/age-segment-list/age-segment-list.component';
import { AgeSegmentAddEditComponent } from './age-segment/age-segment-add-edit/age-segment-add-edit.component';
import { SystemAddEditComponent } from './system-params/system-add-edit/system-add-edit.component';
import { TermsAndPoliciesComponent } from './terms-and-policies/terms-and-policies.component';

const routes: Routes = [
  {
    path: '',
    canActivate: [ClientGuard],
    children: [
      {
        path: 'countries-list',
        component: CountriesListComponent,
      },
      {
        path: 'countries-create',
        component: CreatecountryComponent,
      },
      {
        path: 'countries-edit/:id',
        component: CreatecountryComponent,
      },
      {
        path: 'governorates-list',
        component: GovernoratesListComponent,
      },
      {
        path: 'governorates-create',
        component: GovernoratesCreateComponent,
      },
      {
        path: 'governorates-edit/:id',
        component: GovernoratesCreateComponent,
      },
      {
        path: 'agesegments-list',
        component: AgeSegmentListComponent,
      },
      {
        path: 'agesegments-create',
        component: AgeSegmentAddEditComponent,
      },
      {
        path: 'agesegments-edit/:id',
        component: AgeSegmentAddEditComponent,
      },
      {
        path: 'system-create',
        component: SystemAddEditComponent,
      },
      {
        path: 'system-edit/:id',
        component: SystemAddEditComponent,
      },

    ],
  },
  {
    path: 'terms-and-policies',
    component: TermsAndPoliciesComponent,
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class DataRoutingModule {}
