import {
  CUSTOM_ELEMENTS_SCHEMA,
  NgModule,
  NO_ERRORS_SCHEMA,
} from '@angular/core';
import { CommonModule } from '@angular/common';

import { UsersRoutingModule } from './users-routing.module';
import { SharedModule } from '../shared/shared.module';

// import { ClientDashboardComponent } from './client-dashboard/client-dashboard.component';
import { RolesCreateComponent } from './roles-create/roles-create.component';
import { AssignPermissionComponent } from './assign-permission/assign-permission.component';

import { ClientService } from '../core/data-services/client.service';
import { TreeViewModule } from '@syncfusion/ej2-angular-navigations';
import { RoleListComponent } from './role-list/role-list.component';
import { RoleEditComponent } from './role-edit/role-edit.component';
import { UsersPermissionsComponent } from './users-permissions/users-permissions.component';
import { LogoutPageComponent } from './logout-page/logout-page.component';
import { UsersPatientsComponent } from './users-patients/users-patients.component';
import { SearchUsersPatientsComponent } from './search-users-patients/search-users-patients.component';
import { UserPatientCreateComponent } from './user-patient-create/user-patient-create.component';
import { UserPatientDataComponent } from './user-patient-data/user-patient-data.component';
import { UserPatientAddressCreateComponent } from './user-patient-address-create/user-patient-address-create.component';
import { UserPatientPhoneCreateComponent } from './user-patient-phone-create/user-patient-phone-create.component';

@NgModule({
  declarations: [
    RolesCreateComponent,
    AssignPermissionComponent,
    RoleListComponent,
    RoleEditComponent,
    UsersPermissionsComponent,
    LogoutPageComponent,
    UsersPatientsComponent,
    SearchUsersPatientsComponent,
    UserPatientCreateComponent,
    UserPatientDataComponent,
    UserPatientAddressCreateComponent,
    UserPatientPhoneCreateComponent,
  ],
  imports: [CommonModule, UsersRoutingModule, SharedModule, TreeViewModule],
  providers: [ClientService],
  entryComponents: [AssignPermissionComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA],
})
export class UsersModule {}
