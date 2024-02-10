import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { NotifyService } from 'src/app/core/data-services/notify.service';
import { BaseComponent } from 'src/app/core/permissions/base.component';
import { ActionsEnum } from 'src/app/core/permissions/models/actions';
import { PagesEnum } from 'src/app/core/permissions/models/pages';
import { ClientService } from '../../core/data-services/client.service';
import {
  ReasonsPage,
  RolesSearchCriteria,
  RoleStatus,
  ReasonsType,
} from '../../core/models/models';
import { dialogConfirmComponent } from '../../shared/component/dialog-confirm/dialog-confirm.component';
import { RoleListComponent } from '../../users/role-list/role-list.component';

@Component({
  selector: 'app-reasons',
  templateUrl: './reasons.component.html',
  styleUrls: ['./reasons.component.css'],
})
export class ReasonsComponent extends BaseComponent implements OnInit {
  data: any;
  criteria: ReasonsPage;
  showForm = false;
  displayedColumns: string[] = ['status', 'roleId', 'name', 'edit'];
  dataSource: MatTableDataSource<RoleListComponent>;
  sortKey: String;
  sortDir: String;
  loading;
  isShowEmpty: boolean;
  ReasonName: string;
  reasonId: number;
  selectedActionId = [];
  reasonRouteId;

  roleStatusType: boolean;
  roleReasonsType: number;
  roleStatus = RoleStatus;
  reasons = ReasonsType;
  reasonsActions;
  roleSatuskeys = Object.keys;
  reasonskeys = Object.keys;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(
    private service: ClientService,
    public router: Router,
    private dialog: MatDialog,
    private route: ActivatedRoute,
    public notify : NotifyService
  ) {
    super(PagesEnum.SystemConfiguration,ActionsEnum.View,router,notify);
  }
  ngOnInit(): void {
    this.criteria = new ReasonsPage();
    this.loading = true;
    this.getReasonActions();
    this.route.params.subscribe((params) => {
      console.log(params);
      this.reasonRouteId = params.visitId;
      this.search();
      this.getReasonName();
    });
  }
  getReasonPage() : PagesEnum {
    if (this.reasonRouteId == 9) return PagesEnum.RequestSecondVisitReasons;
    if (this.reasonRouteId == 10) return PagesEnum.ReAssignReasons;
    if (this.reasonRouteId == 4) return PagesEnum.CancellationReasons;
    if (this.reasonRouteId == 8) return PagesEnum.RejectReasons;
    return PagesEnum.RequestSecondVisitReasons;
  }
  search(page?: PageEvent) {
    this.showForm = false;
    this.criteria.CurrentPageIndex = (page && page.pageIndex + 1) || 1;
    if (
      this.roleReasonsType != undefined ||
      this.route.snapshot.params.visitId
    ) {
      this.criteria.VisitTypeActionId =
        this.roleReasonsType || this.route.snapshot.params.visitId;
    } else {
      this.criteria.VisitTypeActionId = null;
    }
    this.criteria.PageSize = (page && page.pageSize) || 5;
    if (this.reasonId != undefined) {
      this.criteria.ReasonId = this.reasonId;
    }
    if (this.ReasonName != undefined) {
      this.criteria.ReasonName = this.ReasonName;
    }

    if (this.roleStatusType != undefined) {
      this.criteria.IsActive = this.roleStatusType;
    } else {
      this.criteria.IsActive = null;
    }

    this.loading = true;
    this.data = undefined;
    return this.service.reasonsService
      .search(this.criteria)
      .subscribe((items) => {
        console.log(items);
        if (items.response.totalCount > 0) {
          this.loading = false;
          this.data = items;
          this.selectedActionId.length = this.data.response.reasons.length;
          for (let i = 0; i < this.selectedActionId.length; i++) {
            this.selectedActionId[i] = '';
            this.data.response.reasons[i]['index'] = i;
          }
          this.dataSource = new MatTableDataSource<any>(
            this.data.response.reasons
          );
          this.dataSource.paginator = this.paginator;
          this.dataSource.sort = this.sort;
          this.isShowEmpty = false;
        } else {
          this.loading = false;
          this.isShowEmpty = true;
        }
      });
  }
  getReasonActions() {
    this.service.reasonsService.getActionReasons().subscribe((res) => {
      this.reasonsActions = res.response;
    });
  }

  getReasonName() {
    if (this.reasonRouteId == 4) {
      return 'Cancel Visit  ';
    } else if (this.reasonRouteId == 8) {
      return 'Reject Visit  ';
    } else if (this.reasonRouteId == 9) {
      return 'Request Visit  ';
    } else if (this.reasonRouteId == 10) {
      return 'Reassign Visit  ';
    }
  }

  gotoEdit() {
    this.router.navigate(['reasons/create', this.reasonRouteId]);
  }
  clear(roleForm: NgForm) {
    this.reasonId = null;
    this.ReasonName = '';
    this.roleStatusType = null;
    this.roleReasonsType = null;

    roleForm.reset(this.search());
  }

  public navigate(id) {
    this.router.navigate(['reasons/edit', this.reasonRouteId, id]); //we can send product object as route param
  }
  openDeleteDialog(id: string): void {
    console.log(id);
    const dialogRef = this.dialog.open(dialogConfirmComponent, {
      data: 'Are You Sure Delete User',
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.service.reasonsService.delete(id).subscribe((res) => {
          if (res != undefined) {
            this.search();
          }
        });
      }
    });
  }
  //------Tree-----
}
