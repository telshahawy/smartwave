import { BrowserModule } from '@angular/platform-browser';
import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { RouterModule } from '@angular/router';

//import { ShellComponent } from './shell/shell.component';

import { HomeComponent } from './home/home.component';
import { OAuthModule } from 'angular-oauth2-oidc';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { LocalStorageService } from 'ngx-webstorage';
import { DashboardComponent } from './dashboard/dashboard.component';
import { AuthGuard } from './core/auth/guards/auth.guard';
import { AutoLoginComponent } from './core/auth/auto-login/auto-login.component';
import { CallbackComponent } from './core/auth/callback/callback.component';
import { AccountService } from './core/data-services/account.service';
import { FooterComponent } from './shared/component/footer/footer.component';
import { HeaderComponent } from './shared/component/header/header.component';
import { SideMenuComponent } from './shared/component/side-menu/side-menu.component';
import { ChemistsModule } from './chemists/chemists.module';
import { HomeVisitsModule } from './home-visits/home-visits.module';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { TokenInterceptor } from './core/auth/token.interceptor';
import { SharedModule } from './shared/shared.module';
import { ClientModule } from './client/client.module';
import { CoreModule } from './core/core.module';
import { MatButtonModule } from '@angular/material/button';
import { ToastrModule } from 'ngx-toastr';
import { AgmCoreModule } from '@agm/core';
import { ReasonsComponent } from './reasons/reasons-list/reasons.component';
import { AddEditReasonsComponent } from './reasons/add-edit-reasons/add-edit-reasons.component';
import { SystemConfigComponent } from './system-config/system-config.component';
import { MatGoogleMapsAutocompleteModule } from '@angular-material-extensions/google-maps-autocomplete';
import { IsGrantedDirective } from './shared/directives/is-granted.directive';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
// import { AgmCoreModule } from '@agm/core/lib/core.module';
const materialModules = [
  MatTableModule,
  MatPaginatorModule,
  MatSortModule,
  MatButtonModule,
];
@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    // ShellComponent,
    SideMenuComponent,
    FooterComponent,
    AutoLoginComponent,
    HomeComponent,
    CallbackComponent,
    DashboardComponent,
    ReasonsComponent,
    AddEditReasonsComponent,
    SystemConfigComponent,
    PageNotFoundComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    AppRoutingModule,
    RouterModule,
    HttpClientModule,
    ChemistsModule,
    ClientModule,
    HomeVisitsModule,
    SharedModule,
    CoreModule,
    MatGoogleMapsAutocompleteModule,
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyBiDbUBM9XVnkqDhwZcqySFwaPXveTmRxw',
      libraries: ['places', 'geometry', 'drawing'],
    }),
    materialModules,
    OAuthModule.forRoot(),
    ToastrModule.forRoot({
      closeButton: true,
      positionClass: 'toast-top-left',
      progressBar: true,
    }),
  ],

  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  providers: [
    AuthGuard,
    AccountService,
    LocalStorageService,
    DatePipe,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
