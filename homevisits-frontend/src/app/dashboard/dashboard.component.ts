import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { NotifyService } from '../core/data-services/notify.service';
import { BaseComponent } from '../core/permissions/base.component';
import { ActionsEnum } from '../core/permissions/models/actions';
import { PagesEnum } from '../core/permissions/models/pages';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
})
export class DashboardComponent extends BaseComponent implements OnInit {
  constructor(public router: Router, public notify: NotifyService) {
    super(PagesEnum.Dashboard, ActionsEnum.View, router, notify);
  }

  ngOnInit(): void {}
}
