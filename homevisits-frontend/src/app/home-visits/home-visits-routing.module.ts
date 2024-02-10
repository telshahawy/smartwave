import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ClientGuard } from '../core/auth/guards/client.guard';
import { HomeVisitsCreateComponent } from './home-visits-create/home-visits-create.component';
import { HomeVisitsListComponent } from './home-visits-list/home-visits-list.component';
import { PatientCreateComponent } from './patient-create/patient-create.component';
import { PatientDataComponent } from './patient-data/patient-data.component';
import { PatientPhoneCreateComponent } from './patient-phone-create/patient-phone-create.component';
import { PatientsAddressCreateComponent } from './patients-address-create/patients-address-create.component';
import { QueryTimeComponent } from './query-time/query-time.component';
import { SearchPatientsComponent } from './search-patients/search-patients.component';
import { VisitDataComponent } from './visit-data/visit-data.component';

const routes: Routes = [{
  path: 'visits', canActivate: [ClientGuard], children: [
    { path: 'visits-list', component: HomeVisitsListComponent },
    { path: 'visits-list/:id', component: HomeVisitsListComponent },
    { path: 'search-patients', component: SearchPatientsComponent },
    { path: 'home-visits-create', component: HomeVisitsCreateComponent },
    { path: 'home-visits-create/:timeZoneFrameGeoZoneId/:visitDate/:geoZoneId', component: HomeVisitsCreateComponent },
    { path: 'home-visits-create/:timeZoneFrameGeoZoneId/:visitDate/:geoZoneId/:chemistId', component: HomeVisitsCreateComponent },
    { path: 'patients-address/:patientId/:relativeType', component: PatientsAddressCreateComponent },
    { path: 'patients-create', component: PatientCreateComponent },
    { path: 'patients-create/:phoneNumber', component: PatientCreateComponent },
    { path: 'patients-data/:patientId', component: PatientDataComponent },
    { path: 'visit-data', component: VisitDataComponent },
    { path: 'patients-phones/:patientId', component: PatientPhoneCreateComponent },
    { path: 'query-avliable-time', component: QueryTimeComponent },
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HomeVisitsRoutingModule { }
