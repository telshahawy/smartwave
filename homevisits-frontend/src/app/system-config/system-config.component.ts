import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NotifyService } from '../core/data-services/notify.service';
import { BaseComponent } from '../core/permissions/base.component';
import { ActionsEnum } from '../core/permissions/models/actions';
import { PagesEnum } from '../core/permissions/models/pages';

@Component({
  selector: 'app-system-config',
  templateUrl: './system-config.component.html',
  styleUrls: ['./system-config.component.css']
})
export class SystemConfigComponent extends BaseComponent  implements OnInit {

  systemParametersPage: PagesEnum = PagesEnum.SystemParameters;
  countriesPage: PagesEnum = PagesEnum.Countries;
  governoratesPage: PagesEnum = PagesEnum.Governorates;
  areasPage: PagesEnum = PagesEnum.Areas;
  requestSecondReasonPage: PagesEnum = PagesEnum.RequestSecondVisitReasons;
  ReassignReasonPage: PagesEnum = PagesEnum.ReAssignReasons;
  requestSecondPage: PagesEnum = PagesEnum.RequestSecondVisitReasons;
  cancellationReasonPage: PagesEnum = PagesEnum.CancellationReasons;
  rejectionReasonPage: PagesEnum = PagesEnum.RejectReasons;
  ageSegment: PagesEnum = PagesEnum.AgeSegments;
  policiesPage: PagesEnum = PagesEnum.TermsAndPolicies;
  
  constructor(public router: Router,public notify: NotifyService) { 
    super(PagesEnum.SystemConfiguration, ActionsEnum.View, router, notify);
  }

  ngOnInit(): void {
  }

}
