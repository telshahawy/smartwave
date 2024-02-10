import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AdminDashboardComponent } from './admin-dashboard.component';
import { ClientAddEditComponent } from './clients/client-add-edit/client-add-edit.component';
import { ClientListComponent } from './clients/client-list/client-list.component';
import { HomeComponent } from './home/home.component';

const routes: Routes = [
  {
    path:'',
    component:AdminDashboardComponent,
    children:[
      { path: '', pathMatch: 'full', redirectTo: 'admin' },
      { path: '', component:HomeComponent },
      {
        path:'client-list',
        component:ClientListComponent
      },
      {
        path:'client-create',
        component:ClientAddEditComponent
      },
      {
        path:'client-edit/:id',
        component:ClientAddEditComponent
      },
      
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SuperAdministrationRoutingModule { }
