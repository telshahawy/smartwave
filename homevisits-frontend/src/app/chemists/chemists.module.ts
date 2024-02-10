import {
  CUSTOM_ELEMENTS_SCHEMA,
  NgModule,
  NO_ERRORS_SCHEMA,
} from '@angular/core';
import { CommonModule } from '@angular/common';

import { ChemistsRoutingModule } from './chemists-routing.module';
import { SharedModule } from '../shared/shared.module';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { ChemistsEditComponent } from './chemists-edit/chemists-edit.component';
import { ChemistsListComponent } from './chemists-list/chemists-list.component';
import { ClientService } from '../core/data-services/client.service';
import { ChemistsCreateComponent } from './chemists-create/chemists-create.component';
import { MAT_DIALOG_DEFAULT_OPTIONS } from '@angular/material/dialog';
import { NgSelectModule } from '@ng-select/ng-select';
import { ChemistschedulesComponent } from './chemistschedules/chemistschedules.component';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatButtonModule } from '@angular/material/button';
// import { MatNativeDateModule } from '@angular/material/core/datetime';
import { MatNativeDateModule } from '@angular/material/core';
import { CreateComponent } from './chemistschedules/create/create.component';
import { ReactiveFormsModule } from '@angular/forms';
import { AgmCoreModule } from '@agm/core';
import { MyVisitsComponent } from './my-visits/my-visits.component';
import { TrackChemistComponent } from './track-chemist/track-chemist.component';
import { ShowChemistScheduleComponent } from './track-chemist/show-chemist-schedule/show-chemist-schedule.component';
import { ShowChemistCardComponent } from './track-chemist/show-chemist-card/show-chemist-card.component';
import { ChemistsPermitsComponent } from './chemists-permits/chemists-permits.component';
import { CreateChemistsPermitsComponent } from './create-chemists-permits/create-chemists-permits.component';
import { EditChemistsPermitsComponent } from './edit-chemists-permits/edit-chemists-permits.component';

@NgModule({
  declarations: [
    ChemistsEditComponent,
    ChemistsListComponent,
    ChemistsCreateComponent,
    ChemistschedulesComponent,
    CreateComponent,
    MyVisitsComponent,
    TrackChemistComponent,
    ShowChemistScheduleComponent,
    ShowChemistCardComponent,
    ChemistsPermitsComponent,
    CreateChemistsPermitsComponent,
    EditChemistsPermitsComponent,
  ],
  imports: [
    CommonModule,
    ChemistsRoutingModule,
    SharedModule,
    MatPaginatorModule,
    NgSelectModule,
    MatDatepickerModule,
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyBiDbUBM9XVnkqDhwZcqySFwaPXveTmRxw',
      libraries: ['places', 'geometry', 'drawing'],
    }),
    ReactiveFormsModule,
    MatButtonModule,
    MatNativeDateModule,
    MatSortModule,
    MatTableModule,
  ],

  providers: [
    ClientService,
    { provide: MAT_DIALOG_DEFAULT_OPTIONS, useValue: { hasBackdrop: false } },
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA],
})
export class ChemistsModule {}
