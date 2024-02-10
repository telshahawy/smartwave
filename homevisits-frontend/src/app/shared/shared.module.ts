import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AgGridModule } from 'ag-grid-angular';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DialogOverviewComponent } from './component/dialog-overview/dialog-overview.component';
import { MatDialogModule } from '@angular/material/dialog';
import { dialogConfirmComponent } from './component/dialog-confirm/dialog-confirm.component';
import { TreeViewModule } from '@syncfusion/ej2-angular-navigations';
import { MaskedTextBoxModule } from '@syncfusion/ej2-angular-inputs';
// import { BrowserModule } from '@angular/platform-browser';
// import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DialogComponent } from './component/dialog/dialog.component';
import { MatButton, MatButtonModule } from '@angular/material/button';
import { MatIcon, MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { NgSelectModule } from '@ng-select/ng-select';
import { KeysPipe } from './Pipes/keys.pipe';
import { SecondVisitCreateComponent } from '../home-visits/second-visit-create/second-visit-create.component';
import { MatGoogleMapsAutocompleteModule } from '@angular-material-extensions/google-maps-autocomplete';
import { AgmCoreModule } from '@agm/core';
import { FormSubmitBtnComponent } from './form-submit-btn/form-submit-btn.component';
import { ShortNamePipe } from './Pipes/short-name.pipe';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { FilterPipe } from './Pipes/search.pipe';
import { IsGrantedDirective } from './directives/is-granted.directive';
import { PermissionService } from '../core/permissions/PermissionManager.service';

//import { DialogConfirmComponent } from './component/dialog-confirm/dialog-confirm.component';

@NgModule({
  //DialogConfirmComponent
  declarations: [
    dialogConfirmComponent,
    DialogOverviewComponent,
    DialogComponent,
    KeysPipe,
    ShortNamePipe,
    FilterPipe,
    FormSubmitBtnComponent,
    IsGrantedDirective
  ],
  imports: [
    CommonModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    FormsModule,
    ReactiveFormsModule,
    MatDatepickerModule,
    MatNativeDateModule,
    // BrowserAnimationsModule,
    // BrowserModule,
    TreeViewModule,
    MaskedTextBoxModule,
    MatButtonModule,
    NgSelectModule,
    MatIconModule,
    MatGoogleMapsAutocompleteModule,
    MatProgressSpinnerModule,
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyBiDbUBM9XVnkqDhwZcqySFwaPXveTmRxw',
      libraries: ['places', 'geometry', 'drawing'],
    }),
  ],
  entryComponents: [
    DialogOverviewComponent,
    dialogConfirmComponent,
     DialogComponent,
      SecondVisitCreateComponent
  ]
  ,

  exports: [
    MatPaginatorModule,
    MatSortModule,
    MatTableModule,
    FormsModule,
    ReactiveFormsModule,
    FormSubmitBtnComponent,
    MatProgressSpinnerModule,
    DialogOverviewComponent,
    MatDialogModule,
    TreeViewModule,
    MaskedTextBoxModule,
    MatButtonModule,
    NgSelectModule,
    MatIconModule,
    MatDatepickerModule,
    MatNativeDateModule,
    KeysPipe,
    ShortNamePipe,
    FilterPipe,
    MatGoogleMapsAutocompleteModule,
    IsGrantedDirective
  ],
  providers: [ PermissionService ]
})
export class SharedModule { }
