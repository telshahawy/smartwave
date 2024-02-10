import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ClientDashboardComponent } from './client/client-dashboard/client-dashboard.component';
import { AuthGuard } from './core/auth/guards/auth.guard';
import { AutoLoginComponent } from './core/auth/auto-login/auto-login.component';
import { CallbackComponent } from './core/auth/callback/callback.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { HomeComponent } from './home/home.component';
import { AdminDashboardComponent } from './super-administration/admin-dashboard.component';
import { ClientGuard } from './core/auth/guards/client.guard';
import { ReasonsComponent } from './reasons/reasons-list/reasons.component';
import { SystemConfigComponent } from './system-config/system-config.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';

const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'alfa/autologin' },
  { path: 'home', component: HomeComponent },
  {
    path: 'admin',
    canActivate: [AuthGuard],
    loadChildren: () =>
      import('./super-administration/super-administration.module').then(
        (mod) => mod.SuperAdministrationModule
      ),
  },
  { path: 'autologin', component: AutoLoginComponent },
  { path: 'callback', component: CallbackComponent },
  {
    path: 'reasons',
    loadChildren: () =>
      import('../app/reasons/reasons.module').then((mod) => mod.ReasonsModule),
  },
  {
    path: 'system-configuration',
    canActivate: [ClientGuard],
    component: SystemConfigComponent,
  },
  {
    path: ':Client',
    children: [
      { path: '', pathMatch: 'full', redirectTo: 'client' },
      {
        path: 'client',
        component: ClientDashboardComponent,
        // component: SystemConfigComponent,
        canActivate: [ClientGuard],
      },
      {
        path: 'home',
        component: HomeComponent,
        canActivate: [ClientGuard],
      },
      {
        path: 'reports',
        canActivate: [ClientGuard],
        loadChildren: () =>
          import('./reports/reports.module').then((mod) => mod.ReportsModule),
      },
      { path: 'autologin', component: AutoLoginComponent },
    ],
  },
  // {
  //   path: 'dashboard',
  //   canActivate: [ClientGuard],
  //   component: ClientDashboardComponent,
  // },
  {
    path: 'users',
    loadChildren: () =>
      import('../app/users/users.module').then((mod) => mod.UsersModule),
  },

  {
    path: 'data',
    loadChildren: () =>
      import('../app/app/data/data.module').then((mod) => mod.DataModule),
  },
  // { path: '**', component: PageNotFoundComponent }, // Wildcard route for a 404 page
  // { path: '', pathMatch: 'full', redirectTo: 'home' },
  // { path:' callback',component:CallbackComponent},
  // { path: 'forbidden', component: ForbiddenComponent, canActivate: [AuthorizationGuard] },
  // { path: 'unauthorized', component: UnauthorizedComponent },
  // { path: 'protected', component: ProtectedComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
