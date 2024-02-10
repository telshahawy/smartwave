import { CUSTOM_ELEMENTS_SCHEMA, NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ClientRoutingModule } from './client-routing.module';


import { ClientDashboardComponent } from './client-dashboard/client-dashboard.component';
import { RolesCreateComponent } from './roles-create/roles-create.component';
import { AssignPermissionComponent } from './assign-permission/assign-permission.component';

import { SharedModule } from '../shared/shared.module';
import { ClientService } from '../core/data-services/client.service';
import { TreeViewModule } from '@syncfusion/ej2-angular-navigations';
import { RoleListComponent } from './role-list/role-list.component';
import { RoleEditComponent } from './role-edit/role-edit.component';
import { AreaListComponent } from './area-list/area-list.component';
import { AreaCreateComponent } from './area-create/area-create.component';
import { AreaEditComponent } from './area-edit/area-edit.component';


@NgModule({
  declarations: [ ClientDashboardComponent, RolesCreateComponent,AssignPermissionComponent, RoleListComponent, RoleEditComponent, AreaListComponent, AreaCreateComponent, AreaEditComponent],
  imports: [
    CommonModule,
    SharedModule,
    ClientRoutingModule,
    TreeViewModule
  ],
  providers:[
    ClientService
  ],
  entryComponents: [ AssignPermissionComponent],
  schemas: [
    CUSTOM_ELEMENTS_SCHEMA,
    NO_ERRORS_SCHEMA
  ]
})
export class ClientModule { }
