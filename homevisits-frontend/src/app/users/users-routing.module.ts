import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ClientGuard } from '../core/auth/guards/client.guard';
import { RolesCreateComponent } from './roles-create/roles-create.component';
import { RoleListComponent } from './role-list/role-list.component';
import { UsersPatientsComponent } from './users-patients/users-patients.component';
import { RoleEditComponent } from './role-edit/role-edit.component';
import { UsersPermissionsComponent } from './users-permissions/users-permissions.component';
import { LogoutPageComponent } from './logout-page/logout-page.component';
import { UserPatientCreateComponent } from './user-patient-create/user-patient-create.component';
import { UserPatientDataComponent } from './user-patient-data/user-patient-data.component';
import { UserPatientAddressCreateComponent } from './user-patient-address-create/user-patient-address-create.component';
import { UserPatientPhoneCreateComponent } from './user-patient-phone-create/user-patient-phone-create.component';
const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: 'users-create',
        component: RolesCreateComponent,
        canActivate: [ClientGuard],
      },
      {
        path: 'users-edit/:userId',
        component: RolesCreateComponent,
        canActivate: [ClientGuard],
      },
      {
        path: 'users-list',
        component: RoleListComponent,
        canActivate: [ClientGuard],
      },
      {
        path: 'patients-list',
        component: UsersPatientsComponent,
        canActivate: [ClientGuard],
      },
      { path: 'patients-create', component: UserPatientCreateComponent },
      { path: 'patients-create/:phoneNumber', component: UserPatientCreateComponent },
      { path: 'patients-data/:patientId/:phoneNumber', component: UserPatientDataComponent },
      { path: 'patients-address/:patientId/:relativeType', component: UserPatientAddressCreateComponent },
      { path: 'patients-phones/:patientId', component: UserPatientPhoneCreateComponent },
      {
        path: 'user-permissions/:id',
        component: UsersPermissionsComponent,
        canActivate: [ClientGuard],
      },
      {
        path: 'logout/:role',
        component: LogoutPageComponent,
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class UsersRoutingModule {}
