import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../core/auth/guards/auth.guard';
import { ClientGuard } from '../core/auth/guards/client.guard';
import { AreaListResponse } from '../core/models/models';
import { AreaCreateComponent } from './area-create/area-create.component';
import { AreaEditComponent } from './area-edit/area-edit.component';
import { AreaListComponent } from './area-list/area-list.component';
import { ClientDashboardComponent } from './client-dashboard/client-dashboard.component';
import { RoleEditComponent } from './role-edit/role-edit.component';
import { RoleListComponent } from './role-list/role-list.component';
import { RolesCreateComponent } from './roles-create/roles-create.component';


const routes: Routes = [{
  path: 'client', canActivate: [ClientGuard], children: [
    { path: 'role-create', component: RolesCreateComponent },
    { path: 'role-list', component: RoleListComponent },
    { path: 'role-edit/:roleId', component: RoleEditComponent },
    { path: 'area-create', component: AreaCreateComponent },
    { path: 'area-list', component: AreaListComponent },
    { path: 'area-edit/:gezoneId', component: AreaEditComponent },
    { path: 'dashboard', component: ClientDashboardComponent },
  ]
}];


@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ClientRoutingModule { }
