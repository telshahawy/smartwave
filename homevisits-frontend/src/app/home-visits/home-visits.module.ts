import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HomeVisitsRoutingModule } from './home-visits-routing.module';
import { HomeVisitsListComponent } from './home-visits-list/home-visits-list.component';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { SharedModule } from '../shared/shared.module';
import { ClientService } from '../core/data-services/client.service';
import { MAT_DIALOG_DEFAULT_OPTIONS } from '@angular/material/dialog';
import { HomeVisitsCreateComponent } from './home-visits-create/home-visits-create.component';
import { SearchPatientsComponent } from './search-patients/search-patients.component';
import { PatientDataComponent } from './patient-data/patient-data.component';
import { VisitDataComponent } from './visit-data/visit-data.component';
import { PatientsAddressCreateComponent } from './patients-address-create/patients-address-create.component';
import { PatientCreateComponent } from './patient-create/patient-create.component';
import { VisitLostTimeComponent } from './visit-lost-time/visit-lost-time.component';
import { SecondVisitCreateComponent } from './second-visit-create/second-visit-create.component';
import { CancelVisitComponent } from './cancel-visit/cancel-visit.component';
import { ReassignChemistVisitComponent } from './reassign-chemist-visit/reassign-chemist-visit.component';
import { VisitDetailsComponent } from './visit-details/visit-details.component';
import { QueryTimeComponent } from './query-time/query-time.component';
import { AgmCoreModule } from '@agm/core';
import { PendingDialogComponent } from './pending-dialog/pending-dialog.component';
import {MatDialogModule} from '@angular/material/dialog';

import { CountdownModule } from 'ngx-countdown';
import { PatientPhoneCreateComponent } from './patient-phone-create/patient-phone-create.component';


@NgModule({
  declarations: [HomeVisitsListComponent, HomeVisitsCreateComponent, SearchPatientsComponent, PatientDataComponent, VisitDataComponent, PatientsAddressCreateComponent, PatientCreateComponent, VisitLostTimeComponent, SecondVisitCreateComponent, CancelVisitComponent, ReassignChemistVisitComponent, VisitDetailsComponent, QueryTimeComponent, PendingDialogComponent, PatientPhoneCreateComponent],
  imports: [
    CommonModule,
    HomeVisitsRoutingModule,
    SharedModule,
    MatPaginatorModule,
    MatSortModule,
    MatTableModule,
    MatDialogModule,
    CountdownModule,
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyBiDbUBM9XVnkqDhwZcqySFwaPXveTmRxw',
      libraries: ['places', 'geometry', 'drawing'],
    }),
  ],
  providers:[
    ClientService,
    {provide: MAT_DIALOG_DEFAULT_OPTIONS, useValue: {hasBackdrop: false}}
  ]
})
export class HomeVisitsModule { }
