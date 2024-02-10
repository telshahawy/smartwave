import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SuperAdministrationRoutingModule } from './super-administration-routing.module';
import { AdminDashboardComponent } from './admin-dashboard.component';
import { ClientListComponent } from './clients/client-list/client-list.component';
import { ClientAddEditComponent } from './clients/client-add-edit/client-add-edit.component';
import { HeaderComponent } from './shared/components/sidebar/header.component';
import { NavTopComponent } from './shared/components/nav-top/nav-top.component';
import { HomeComponent } from './home/home.component';


@NgModule({
  declarations: [AdminDashboardComponent, ClientListComponent,
     ClientAddEditComponent, HeaderComponent, NavTopComponent, HomeComponent],
  imports: [
    CommonModule,
    SuperAdministrationRoutingModule
  ]
})
export class SuperAdministrationModule { }
