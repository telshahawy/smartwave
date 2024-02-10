import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DataRoutingModule } from './data-routing.module';
import { SharedModule } from '../../shared/shared.module';
import { CountriesListComponent } from './countries-list/countries-list.component';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { CreatecountryComponent } from './createcountry/createcountry.component';
import { GovernoratesListComponent } from './governorates-list/governorates-list.component';
import { GovernoratesCreateComponent } from './governorates-create/governorates-create.component';
import { AgeSegmentAddEditComponent } from './age-segment/age-segment-add-edit/age-segment-add-edit.component';
import { AgeSegmentListComponent } from './age-segment/age-segment-list/age-segment-list.component';
import { SystemAddEditComponent } from './system-params/system-add-edit/system-add-edit.component';
import { TermsAndPoliciesComponent } from './terms-and-policies/terms-and-policies.component';

const COMPONENTS=[AgeSegmentAddEditComponent,AgeSegmentListComponent ,SystemAddEditComponent]
@NgModule({
  declarations: [CountriesListComponent, CreatecountryComponent, ...COMPONENTS,
    GovernoratesListComponent, GovernoratesCreateComponent, TermsAndPoliciesComponent],
  imports: [CommonModule, DataRoutingModule, SharedModule],
  schemas: [CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA],
})
export class DataModule {}
