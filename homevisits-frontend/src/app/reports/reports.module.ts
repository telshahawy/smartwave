import {
  CUSTOM_ELEMENTS_SCHEMA,
  NgModule,
  NO_ERRORS_SCHEMA,
} from '@angular/core';
import { CommonModule } from '@angular/common';

import { ReportsRoutingModule } from './reports-routing.module';
import { VisitReportsComponent } from './visit-reports/visit-reports.component';
import { TatTrackingComponent } from './tat-tracking/tat-tracking.component';
import { ReassignedReportsComponent } from './reassigned-reports/reassigned-reports.component';
import { RejectedReportsComponent } from './rejected-reports/rejected-reports.component';
import { LostBussinessReportsComponent } from './lost-bussiness-reports/lost-bussiness-reports.component';
import { CancelledVisitReportsComponent } from './cancelled-visit-reports/cancelled-visit-reports.component';
import { SharedModule } from '../shared/shared.module';
import { DetailsReportsComponent } from './visit-reports/details-reports/details-reports.component';
import { TotalReportsComponent } from './visit-reports/total-reports/total-reports.component';
import { ShowReportsListComponent } from './cancelled-visit-reports/show-reports-list/show-reports-list.component';
import { ShowReportsRejectedListComponent } from './rejected-reports/show-reports-list/show-reports-list.component';
import { TatTrackingTotalComponent } from './tat-tracking/tat-tracking-total/tat-tracking-total.component';
import { TatTrackingDetailsComponent } from './tat-tracking/tat-tracking-details/tat-tracking-details.component';
import { LostBuissinessTotalReportComponent } from './lost-bussiness-reports/lost-buissiness-total-report/lost-buissiness-total-report.component';
import { ReassignedTotalReportComponent } from './reassigned-reports/reassigned-total-report/reassigned-total-report.component';

@NgModule({
  declarations: [
    VisitReportsComponent,
    TatTrackingComponent,
    ReassignedReportsComponent,
    RejectedReportsComponent,
    LostBussinessReportsComponent,
    CancelledVisitReportsComponent,
    DetailsReportsComponent,
    TotalReportsComponent,
    ShowReportsListComponent,
    ShowReportsRejectedListComponent,
    TatTrackingTotalComponent,
    TatTrackingDetailsComponent,
    LostBuissinessTotalReportComponent,
    ReassignedTotalReportComponent,
  ],
  imports: [CommonModule, ReportsRoutingModule, SharedModule],
  schemas: [CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA],
})
export class ReportsModule {}
