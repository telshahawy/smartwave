import { ViewChild } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { MaskedTextBoxComponent } from '@syncfusion/ej2-angular-inputs';
import { TreeViewComponent } from '@syncfusion/ej2-angular-navigations';
import { ClientService } from 'src/app/core/data-services/client.service';
import { NotifyService } from 'src/app/core/data-services/notify.service';
import {
  IpagedList,
  RoleListResponse,
  RolesSearchCriteria,
  RoleStatus,
} from 'src/app/core/models/models';
import { BaseComponent } from 'src/app/core/permissions/base.component';
import { ActionsEnum } from 'src/app/core/permissions/models/actions';
import { PagesEnum } from 'src/app/core/permissions/models/pages';
import { dialogConfirmComponent } from 'src/app/shared/component/dialog-confirm/dialog-confirm.component';

@Component({
  selector: 'app-role-list',
  templateUrl: './role-list.component.html',
  styleUrls: ['./role-list.component.css'],
})
export class RoleListComponent extends BaseComponent implements OnInit {
  data: IpagedList<RoleListResponse>;
  criteria: RolesSearchCriteria;
  showForm = false;
  displayedColumns: string[] = ['status', 'roleId', 'name', 'edit'];
  dataSource: MatTableDataSource<RoleListComponent>;
  sortKey: String;
  sortDir: String;
  loading;
  isShowEmpty: boolean;
  name: string;
  code: number;
  rolesPage: PagesEnum = PagesEnum.Roles;
  roleStatusType: boolean = null;
  roleStatus = RoleStatus;
  roleSatuskeys = Object.keys;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(
    private service: ClientService,
    public router: Router,
    private dialog: MatDialog,
    public notify: NotifyService
  ) {
    super(PagesEnum.Roles, ActionsEnum.View, router, notify);
  }
  ngOnInit(): void {
    this.criteria = new RolesSearchCriteria();
    this.loading = true;
    this.search();
  }
  search(page?: PageEvent) {
    this.showForm = false;
    this.criteria.currentPageIndex = (page && page.pageIndex + 1) || 1;
    this.criteria.pageSize = (page && page.pageSize) || 5;
    if (this.code != undefined) {
      this.criteria.code = this.code;
    }
    if (this.name != undefined) {
      this.criteria.name = this.name;
    }

    if (this.roleStatusType != undefined) {
      this.criteria.isActive = this.roleStatusType;
    } else {
      this.criteria.isActive = null;
    }

    this.loading = true;
    this.data = undefined;
    return this.service.searchRole(this.criteria).subscribe((items) => {
      console.log(items);
      if (items.response.totalCount > 0) {
        this.loading = false;
        this.data = items;
        this.dataSource = new MatTableDataSource<any>(this.data.response.roles);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
        this.isShowEmpty = false;
      } else {
        this.loading = false;
        this.isShowEmpty = true;
      }
    });
  }

  gotoEdit() {
    this.router.navigate(['client/role-create']);
  }
  clear(roleForm: NgForm) {
    this.criteria = new RolesSearchCriteria();
    this.code = null;
    this.name = '';
    this.roleStatusType = null;

    roleForm.reset(this.search());
  }

  navigate(role) {
    this.router.navigate(['client/role-edit', role]); //we can send product object as route param
  }
  openDeleteDialog(id: string): void {
    const dialogRef = this.dialog.open(dialogConfirmComponent, {
      data: 'Are You Sure Delete Role',
      disableClose: true,
      hasBackdrop: true,
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.service.deleteRole(id).subscribe((res) => {
          if (res != undefined) {
            this.search();
          }
        });
      }
    });
  }
  //------Tree-----

  //-----------
}
