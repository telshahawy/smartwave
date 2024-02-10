import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ClientGuard } from '../core/auth/guards/client.guard';
import { AddEditReasonsComponent } from './add-edit-reasons/add-edit-reasons.component';
import { ReasonsComponent } from './reasons-list/reasons.component';


const routes: Routes = [
  // 'Cancel Visit Type Action' = '4',
  // 'Reject visit Type Action' = '8',
  // 'request second visit Type Action' = '9',
  // 'Reassign visit Type Action' = '10',
  {
    path: '',
    canActivate: [ClientGuard],
    children: [
      {
        path:'list/:visitId',
        component:ReasonsComponent
      },
      {
        path:'edit/:visitId/:reasonId',
        component:AddEditReasonsComponent
      },
      {
        path:'create/:visitId',
        component:AddEditReasonsComponent
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ReasonsRoutingModule {}
