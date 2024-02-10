import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ClientGuard } from '../core/auth/guards/client.guard';
import { ChemistsCreateComponent } from './chemists-create/chemists-create.component';
import { ChemistsEditComponent } from './chemists-edit/chemists-edit.component';
import { ChemistsListComponent } from './chemists-list/chemists-list.component';
import { ChemistsPermitsComponent } from './chemists-permits/chemists-permits.component';
import { ChemistschedulesComponent } from './chemistschedules/chemistschedules.component';
import { CreateComponent } from './chemistschedules/create/create.component';
import { TrackChemistComponent } from './track-chemist/track-chemist.component';
import { CreateChemistsPermitsComponent } from './create-chemists-permits/create-chemists-permits.component';
import { EditChemistsPermitsComponent } from './edit-chemists-permits/edit-chemists-permits.component';

const routes: Routes = [
  {
    path: 'chemists',
    canActivate: [ClientGuard],
    children: [
      { path: 'chemists-list', component: ChemistsListComponent },
      { path: 'chemists-edit/:chemistId', component: ChemistsEditComponent },
      { path: 'chemists-create', component: ChemistsCreateComponent },
      { path: 'chemists-permits/:chemistId', component: ChemistsPermitsComponent },
      { path: 'create-chemists-permits/:chemistId', component: CreateChemistsPermitsComponent },
      { path: 'update-chemists-permits/:chemistPermitId', component: EditChemistsPermitsComponent },

      {
        path: 'chemist-schedules/:chemistId/:userName',
        component: ChemistschedulesComponent,
      },
      {
        path: 'chemist-schedules/:chemistId/create/:userName',
        component: CreateComponent,
      },
      {
        path: 'chemist-schedules/:chemistId/edit/:scheduleId/:userName',
        component: CreateComponent,
      },
      {
        path: 'track',
        component: TrackChemistComponent,
      },
      
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes), CommonModule],
  exports: [RouterModule],
})
export class ChemistsRoutingModule {}
