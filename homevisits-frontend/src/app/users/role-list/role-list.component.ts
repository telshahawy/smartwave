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

@Component({
  selector: 'app-role-list',
  templateUrl: './role-list.component.html',
  styleUrls: ['./role-list.component.css'],
})
export class RoleListComponent extends BaseComponent implements OnInit {
  data: any;
  criteria: RolesSearchCriteria;
  showForm = false;
  displayedColumns: string[] = ['status','userId', 'name', 'mobile', 'edit'];
  mobNumberPattern = "([+]|0)[0-9]{1,}";
  submitted:boolean=false

  dataSource: MatTableDataSource<RoleListComponent>;
  sortKey: String;
  sortDir: String;
  loading;
  isShowEmpty: boolean;
  name: string;
  code: number;
  PhoneNumber:string
  selectedActionId = [];
  actions = [
    {
      id: 1,
      name: 'Edit',
    },
    // {
    //   id: 3,
    //   name: 'Send Credentials',
    // },
    // {
    //   id: 4,
    //   name: 'Privileges',
    // },
    {
      id: 5,
      name: 'Delete',
    },
  ];
  roleStatusType: boolean;
  roleStatus = RoleStatus;
  usersPage : PagesEnum = PagesEnum.Users;
  roleSatuskeys = Object.keys;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(
    private service: ClientService,
    public router: Router,
    private dialog: MatDialog,
    public notify: NotifyService
  ) {
    super(PagesEnum.Users, ActionsEnum.View, router, notify)

  }
  ngOnInit(): void {
    this.criteria = new RolesSearchCriteria();
    this.loading = true;
    this.search();
  }
  search(roleForm?:NgForm,page?: PageEvent) {
    
    console.log(roleForm);
    let form = roleForm?.form?.value
    this.submitted=true;

    var searchData = {};
    if (form?.code) {
      searchData['Code'] = form?.code;
    }
    if (form?.name) {
      searchData['Name'] = form?.name;
    }
    if (form?.isActive) {
      searchData['IsActive'] = form?.isActive;
    }
    if (form?.PhoneNumber) {
      searchData['PhoneNumber'] = form?.PhoneNumber;
    }
    searchData['PageSize'] = page?.pageSize || 5;
    searchData['CurrentPageIndex'] = (page?.pageSize && page?.pageIndex + 1) || 1;
    
    this.loading = true;
    this.data = undefined;
    return this.service.searchUser(searchData).subscribe((items) => {
      console.log(items);
      if (items.response.totalCount > 0) {
        this.loading = false;
        this.data = items;
        this.selectedActionId.length = this.data.response.users.length;
        for (let i = 0; i < this.selectedActionId.length; i++) {
          this.selectedActionId[i] = '';
          this.data.response.users[i]['index'] = i;
        }
        this.dataSource = new MatTableDataSource<any>(this.data.response.users);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
        this.isShowEmpty = false;
      } else {
        this.loading = false;
        this.isShowEmpty = true;
      }
    });
  }
  changeAction(action, currentUser) {
    if (action == 1) {
      this.router.navigate(['users/users-edit', currentUser]);
    }
    if (action == 4) {
      this.router.navigate(['users/user-permissions/', currentUser]);
    }
    if (action == 5) {
      this.openDeleteDialog(currentUser);
    }
  }
  gotoEdit() {
    this.router.navigate(['users/users-create']);
  }
  clear(roleForm: NgForm) {

    roleForm.reset(this.search())
    
    
  }

  private navigate(role) {
    this.router.navigate(['users/users-edit', role]); //we can send product object as route param
  }
  openDeleteDialog(id: string): void {
    const dialogRef = this.dialog.open(dialogConfirmComponent, {
      data: 'Are You Sure Delete User',
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.service.deleteUser(id).subscribe((res) => {
          if (res ) {
            this.search();
          }
        });
      }
    });
  }
  //------Tree-----

  //-----------
}
