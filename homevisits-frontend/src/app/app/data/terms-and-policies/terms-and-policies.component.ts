import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NotifyService } from 'src/app/core/data-services/notify.service';
import { BaseComponent } from 'src/app/core/permissions/base.component';
import { ActionsEnum } from 'src/app/core/permissions/models/actions';
import { PagesEnum } from 'src/app/core/permissions/models/pages';

@Component({
  selector: 'app-terms-and-policies',
  templateUrl: './terms-and-policies.component.html',
  styleUrls: ['./terms-and-policies.component.css'],
})
export class TermsAndPoliciesComponent extends BaseComponent implements OnInit {
  constructor(public router: Router, public notify: NotifyService) {
    super(PagesEnum.TermsAndPolicies, ActionsEnum.View, router, notify);
  }
  isPermissionsShow: boolean = false;
  iscontentShow: boolean = false;
  isDefinitionshow: boolean = false;
  ngOnInit(): void {}
}
