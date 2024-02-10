import { Injectable, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { NotifyService } from '../data-services/notify.service';
import { Assets } from './menu/assests';
import { ActionsEnum } from './models/actions';
import { PagesEnum } from './models/pages';
import { UserPermission } from './models/userPermisions';

@Injectable()
export abstract class BaseComponent {
  constructor(
    public page: PagesEnum,
    public action: ActionsEnum,
    public router: Router,
    public notify: NotifyService
  ) {
    this.userPermissions = JSON.parse(sessionStorage.getItem('permissions')
    );
    this.hasAccess();
  }
  userPermissions: UserPermission[] = [];
  createAction: ActionsEnum = ActionsEnum.Create;
  updateAction: ActionsEnum = ActionsEnum.Update;
  viewAction: ActionsEnum = ActionsEnum.View;
  DeleteAction: ActionsEnum = ActionsEnum.Delete;
  ApproveAction: ActionsEnum = ActionsEnum.Approve;
  CancelAction: ActionsEnum = ActionsEnum.Cancel;
  RequestSecondVisitAction: ActionsEnum = ActionsEnum.RequestSecondVisit;
  ReassignAction: ActionsEnum = ActionsEnum.Reassign;

  private hasAccess() {
    let hasPermission = this.userPermissions != null && this.userPermissions.find((x) => x.pageCode == this.page && x.actionCode == this.action) != null;
    if (!hasPermission) {
        // this.notify.error('You have No Access to this page', 'Unable to Navigate');
      let url = Assets.getUrlByCode(this.userPermissions[0].actionCode);
      if (url == null)
        url = "/client/home"
      this.router.navigate([url]);
    }
  }
}