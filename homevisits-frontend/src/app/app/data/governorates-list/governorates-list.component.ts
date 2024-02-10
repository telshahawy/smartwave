import { ViewChild } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
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
import { RoleListComponent } from '../../../client/role-list/role-list.component';

@Component({
  selector: 'app-governorates-list',
  templateUrl: './governorates-list.component.html',
  styleUrls: ['./governorates-list.component.css'],
})
export class GovernoratesListComponent extends BaseComponent implements OnInit {
  data: IpagedList<any>;
  criteria: any = {};
  showForm = false;
  govPage : PagesEnum = PagesEnum.Governorates;
  displayedColumns: string[] = [
    'status',
    'governorateId',
    'governorateName',
    'country',
    'customerServiceEmail',
    'edit',
  ];
  dataSource: MatTableDataSource<any>;
  sortKey: String;
  sortDir: String;
  loading;
  countries = [];
  governorates=[]
  isShowEmpty: boolean;
  name: string;
  code: number;
  selectedCountryId: string;
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
    super(PagesEnum.Governorates,ActionsEnum.View,router,notify)
  }
  ngOnInit(): void {
    // this.criteria = new RolesSearchCriteria();
    this.loading = true;
    this.search();
    this.getCountries();
  }
  getCountries() {
    return this.service.getCountries().subscribe((items) => {
      this.countries = items.response.countries;
    });
  }

  search(page?: PageEvent) {
    this.showForm=false;
    this.criteria.CurrentPageIndex = (page && page.pageIndex + 1) || 1;
    this.criteria.PageSize = (page && page.pageSize) || 5;
    if (this.code != undefined) {
      this.criteria.Code = this.code;
    }
    if (this.name != undefined) {
      this.criteria.Name = this.name;
    }
    if (this.selectedCountryId != undefined) {
      this.criteria.CountryId = this.selectedCountryId;
    }
    if (this.roleStatusType != undefined) {
      this.criteria.IsActive = this.roleStatusType;
    } else {
      this.criteria.IsActive = null;
    }

    this.loading = true;
    this.data = undefined;
    this.dataSource = new MatTableDataSource<any>(null);
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    this.isShowEmpty = false;
    return this.service.searchgovers(this.criteria).subscribe((items) => {
      // this.countries = items.response.countries;
      if (items.response.governats.length > 0) {
        this.loading = false;
        this.data = items;
        this.dataSource = new MatTableDataSource<any>(
          this.data.response.governats
        );
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
        this.isShowEmpty = false;
      } else {
        this.loading = false;
        this.isShowEmpty = true;
      }
    });
    // return this.service.searchRole(this.criteria).subscribe((items) => {
    //   console.log(items);
    //   if (items.response.totalCount > 0) {
    //     this.loading = false;
    //     this.data = items;
    //     this.dataSource = new MatTableDataSource<any>(this.data.response.roles);
    //     this.dataSource.paginator = this.paginator;
    //     this.dataSource.sort = this.sort;
    //     this.isShowEmpty = false;
    //   } else {
    //     this.loading = false;
    //     this.isShowEmpty = true;
    //   }
    // });
  }

  gotoEdit() {
    this.router.navigate(['data/governorates-create']);
  }
  clear(roleForm: NgForm) {
    this.criteria = new RolesSearchCriteria();
    this.code = null;
    this.name = '';
    this.roleStatusType = null;

    roleForm.reset(this.search());
  }

   navigate(role) {
    this.router.navigate(['data/governorates-edit', role]); //we can send product object as route param
  }
  openDeleteDialog(id: string): void {
    const dialogRef = this.dialog.open(dialogConfirmComponent, {
      data: 'Are You Sure Delete Governorates',
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.service.deleteGover(id).subscribe((res) => {
          if (res != undefined) {
            this.search();
          }
          console.log(res);
          
        },
        (err)=>{
          this.notify.error(err.message, 'FAILED OPERATION');
          console.log(err)
        });
      }
    });
  }
  //------Tree-----

  //-----------
}
