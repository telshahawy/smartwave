import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CancelledVisitReportsComponent } from './cancelled-visit-reports/cancelled-visit-reports.component';
import { ShowReportsListComponent } from './cancelled-visit-reports/show-reports-list/show-reports-list.component';
import { LostBuissinessTotalReportComponent } from './lost-bussiness-reports/lost-buissiness-total-report/lost-buissiness-total-report.component';
import { LostBussinessReportsComponent } from './lost-bussiness-reports/lost-bussiness-reports.component';
import { ReassignedReportsComponent } from './reassigned-reports/reassigned-reports.component';
import { ReassignedTotalReportComponent } from './reassigned-reports/reassigned-total-report/reassigned-total-report.component';
import { RejectedReportsComponent } from './rejected-reports/rejected-reports.component';
import { ShowReportsRejectedListComponent } from './rejected-reports/show-reports-list/show-reports-list.component';
import { TatTrackingDetailsComponent } from './tat-tracking/tat-tracking-details/tat-tracking-details.component';
import { TatTrackingTotalComponent } from './tat-tracking/tat-tracking-total/tat-tracking-total.component';
import { TatTrackingComponent } from './tat-tracking/tat-tracking.component';
import { DetailsReportsComponent } from './visit-reports/details-reports/details-reports.component';
import { TotalReportsComponent } from './visit-reports/total-reports/total-reports.component';
import { VisitReportsComponent } from './visit-reports/visit-reports.component';

const routes: Routes = [
  {
    path: 'visit-reports/create',
    component: VisitReportsComponent
  },
  {
    path: 'visit-reports/details',
    component: DetailsReportsComponent
  },
  {
    path: 'visit-reports/total',
    component: TotalReportsComponent
  },
  {
    path: 'tat-tracking/create',
    component: TatTrackingComponent
  },
  {
    path: 'tat-tracking/total',
    component: TatTrackingTotalComponent
  },
  {
    path: 'tat-tracking/details',
    component: TatTrackingDetailsComponent
  },
  {
    path: 'rejected-reports/create',
    component: RejectedReportsComponent
  },
  {
    path: 'rejected-reports/details',
    component: ShowReportsRejectedListComponent
  },
  {
    path: 'reassigned-reports/create',
    component: ReassignedReportsComponent
  },
   {
    path: 'reassigned-reports/total',
    component: ReassignedTotalReportComponent
  },
  {
    path: 'lost-bussiness-reports/create',
    component: LostBussinessReportsComponent
  },
  {
    path: 'lost-bussiness-reports/total',
    component: LostBuissinessTotalReportComponent
  },
  {
    path: 'cancelled-visit-reports/create',
    component: CancelledVisitReportsComponent
  },
  {
    path: 'cancelled-visit-reports/details',
    component: ShowReportsListComponent
  },


];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ReportsRoutingModule { }
