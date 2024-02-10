import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ClientService } from 'src/app/core/data-services/client.service';
import { NotifyService } from 'src/app/core/data-services/notify.service';
import {
  IpagedList,
  ReasonsPage,
  ReasonsType,
  RoleStatus,
} from 'src/app/core/models/models';
import { BaseComponent } from 'src/app/core/permissions/base.component';
import { ActionsEnum } from 'src/app/core/permissions/models/actions';
import { PagesEnum } from 'src/app/core/permissions/models/pages';
import { dialogConfirmComponent } from 'src/app/shared/component/dialog-confirm/dialog-confirm.component';
import { RoleListComponent } from 'src/app/users/role-list/role-list.component';

@Component({
  selector: 'app-age-segment-list',
  templateUrl: './age-segment-list.component.html',
  styleUrls: ['./age-segment-list.component.css'],
})
export class AgeSegmentListComponent extends BaseComponent implements OnInit {
  data: IpagedList<any>;
  criteria: any;
  showForm = false;
  displayedColumns: string[] = [
    'Status',
    'SegmentId',
    'SegmentName',
    'NeedExperts',
    'AgeFrom',
    'AgeTo',
    'Actions',
  ];
  dataSource: MatTableDataSource<any>;
  sortKey: String;
  sortDir: String;
  loading;
  isShowEmpty: boolean;
  name: string;
  code: number;
  isActive: boolean = null;
  needExpertType: boolean = null;
  roleStatus = RoleStatus;
  ageSegmentPage : PagesEnum = PagesEnum.AgeSegments;
  roleSatuskeys = Object.keys;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(
    private service: ClientService,
    private dialog: MatDialog,
    public router: Router,
    public notify: NotifyService
  ) {
    super(PagesEnum.AgeSegments,ActionsEnum.View,router,notify);
  }
  ngOnInit(): void {
    // this.criteria = new RolesSearchCriteria();
    this.loading = true;
    this.search();
  }
  search(page?: PageEvent) {
    this.loading = true;
    this.data = undefined;
    this.dataSource = new MatTableDataSource<any>(null);
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    this.isShowEmpty = false;

    var searchData = {};
    if (this.code) {
      searchData['Code'] = this.code;
    }
    if (this.name) {
      searchData['Name'] = this.name;
    }
    if (this.isActive) {
      searchData['IsActive'] = this.isActive;
    }
    if (this.needExpertType) {
      searchData['NeedExpert'] = this.needExpertType;
    }
    searchData['PageSize'] = 5;
    searchData['CurrentPageIndex'] = page ? page : 1;

    return this.service.ageSegmentsService
      .search(searchData)
      .subscribe((items) => {
        if (items.response.ageSegments?.length > 0) {
          this.loading = false;
          this.data = items;
          this.dataSource = new MatTableDataSource<any>(
            this.data.response.ageSegments
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

  gotoEdit() {
    this.router.navigate(['data/agesegments-create']);
  }
  clear(roleForm: NgForm) {
    // this.criteria = new RolesSearchCriteria();
    this.code = null;
    this.name = '';
    this.isActive = null;
    this.needExpertType = null;

    roleForm.reset(this.search());
  }

  navigate(role) {
    this.router.navigate(['data/agesegments-edit', role]); //we can send product object as route param
  }
  openDeleteDialog(id: string): void {
    const dialogRef = this.dialog.open(dialogConfirmComponent, {
      data: 'Are You Sure Delete Country',
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.service.ageSegmentsService.delete(id).subscribe((res) => {
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
