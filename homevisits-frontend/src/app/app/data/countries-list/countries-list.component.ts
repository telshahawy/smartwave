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
  selector: 'app-countries-list',
  templateUrl: './countries-list.component.html',
  styleUrls: ['./countries-list.component.css'],
})
export class CountriesListComponent extends BaseComponent implements OnInit {
  data: IpagedList<any>;
  criteria: any;
  showForm = false;
  displayedColumns: string[] = [
    'status',
    'countryId',
    'name',
    'mobileLength',
    'edit',
  ];
  dataSource: MatTableDataSource<any>;
  sortKey: String;
  sortDir: String;
  loading;
  isShowEmpty: boolean;
  name: string;
  code: number;
  roleStatusType: boolean = null;
  roleStatus = RoleStatus;
  countryPage: PagesEnum = PagesEnum.Countries;
  roleSatuskeys = Object.keys;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(
    private service: ClientService,
    public router: Router,
    private dialog: MatDialog,
    public notify : NotifyService
  ) { 
    super(PagesEnum.Countries,ActionsEnum.View,router,notify);
  }
  ngOnInit(): void {
    // this.criteria = new RolesSearchCriteria();
    this.loading = true;
    this.search();
  }
  search(page?: PageEvent) {
    console.log(page);

    this.showForm = false;
  
    var searchData = {};

    searchData['PageSize'] = (page && page.pageSize) || 5;
    searchData['CurrentPageIndex'] = (page && page.pageIndex + 1) || 1;
    if (this.code) {
      searchData['Code'] = this.code;
    }
    if (this.name) {
      searchData['Name'] = this.name;
    }
    if (this.roleStatusType) {
      searchData['IsActive'] = this.roleStatusType;
    }
    this.loading = true;
    this.data = undefined;
    this.dataSource = new MatTableDataSource<any>(null);
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    this.isShowEmpty = false;

    // console.log('size : ', this.data?.response.pageSize);

    // return this.service.getCountries().subscribe((items) => {
    return this.service.searchCountries(searchData).subscribe((items) => {
      // this.countries = items.response.countries;
      if (items?.response?.countries?.length > 0) {
        this.loading = false;
        this.data = items;
        this.dataSource = new MatTableDataSource<any>(
          this.data.response.countries
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
    this.router.navigate(['data/countries-create']);
  }
  clear(roleForm: NgForm) {
    // this.criteria = new RolesSearchCriteria();
    this.code = null;
    this.name = '';
    this.roleStatusType = null;

    roleForm.reset(this.search());
  }

  private navigate(role) {
    this.router.navigate(['data/countries-edit', role]); //we can send product object as route param
  }
  openDeleteDialog(id: string): void {
    const dialogRef = this.dialog.open(dialogConfirmComponent, {
      data: 'Are You Sure Delete Country',
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.service.deleteCountry(id).subscribe((res) => {
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
