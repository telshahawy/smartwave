import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { Router, ActivatedRoute } from '@angular/router';
import { MaskedTextBoxComponent } from '@syncfusion/ej2-angular-inputs';
import { TreeViewComponent } from '@syncfusion/ej2-angular-navigations';
import { ClientService } from 'src/app/core/data-services/client.service';
import {
  AreaLookup,
  GetPermissionsChild,
  RoleStatus,
  systemPagesList,
  systemPagesResponse,
} from 'src/app/core/models/models';
import { DataManager, Query, Predicate } from '@syncfusion/ej2-data';
import { AssignPermissionComponent } from '../assign-permission/assign-permission.component';
import { NotifyService } from 'src/app/core/data-services/notify.service';
import { BaseComponent } from 'src/app/core/permissions/base.component';
import { ActionsEnum } from 'src/app/core/permissions/models/actions';
import { PagesEnum } from 'src/app/core/permissions/models/pages';

@Component({
  selector: 'app-roles-create',
  templateUrl: './roles-create.component.html',
  styleUrls: ['./roles-create.component.css'],
})
export class RolesCreateComponent extends BaseComponent implements OnInit {
  areas: AreaLookup[];
  systemPages: systemPagesList[];
  showPass = false;
  userData: any;
  geoZones;
  mobNumberPattern = "([+]|0)[0-9]{1,}";
  //tree -----
  @ViewChild('treeview', { static: false })
  public tree: TreeViewComponent;
  @ViewChild('maskObj', { static: false })
  public maskObj: MaskedTextBoxComponent;
  // @ViewChild("treeviewObj",{static:false})
  //  tree:TreeViewComponent;
  @ViewChild('roleForm') roleForm: NgForm;
  @ViewChild('samples')
  public checkNodes = [];
  //data: RoleAllPermissionsResponse[];
  checkedPermission: Array<number> = [];
  convert: any[];
  treeData: any;
  roleId: number = null;
  UserId;
  name = '';
  phoneNumber;
  userName;
  defaultPageId: number = null;
  isActive: boolean = false;
  roles = [];
  public vm: any = { field: undefined };
  // set the CheckBox to the TreeView
  public showCheckBox: boolean = true;
  //--------
  constructor(
    private service: ClientService,
    public notify: NotifyService,
    private dialog: MatDialog,
    public router: Router,
    private route: ActivatedRoute
  ) {
    super(PagesEnum.Users, ActionsEnum.Create, router, notify)
  }

  ngOnInit(): void {
    // this.getSystemPages();
    this.getAreas();
    this.service.searchRole(null).subscribe((items) => {
      this.roles = items.response.roles;
      // console.log('roles : ', items.response.roles);
    });
    this.UserId = this.route.snapshot.paramMap.get("userId");
    if (this.UserId) {
      this.service.getUser(this.UserId).subscribe((res) => {

        this.userData = res.response.user;
        this.roleId = this.userData.roleId;
        this.geoZones = this.userData.geoZonesIds;
        this.name = this.userData?.name;
        this.phoneNumber = this.userData?.phoneNumber;
        this.userName = this.userData?.userName;
        this.isActive = this.userData.isActive;

        this.roleForm.controls['name'].setValue(this.name);
        this.roleForm.controls['roleId'].setValue(this.roleId);
        this.roleForm.controls['geoZones'].setValue(this.geoZones);
        this.roleForm.controls['phoneNumber'].setValue(this.phoneNumber);
        this.roleForm.controls['userName'].setValue(this.userName);
        this.roleForm.controls['isActive'].setValue(this.isActive);
        this.getPermissions(); // Load permissions after user info is retrieved
        // if(res.response.role.permissions.length>0)
      });
    } else {
      this.getPermissions();

    }
  }
  onlyNumberKey(event) {
    return event.charCode == 8 || event.charCode == 0
      ? null
      : event.charCode >= 48 && event.charCode <= 57;
  }
  getAreas() {
    return this.service.getAreas().subscribe((items) => {
      this.areas = items.response.geoZones;
      // this.geoZones = this.areas;
      // console.log('areas : ', this.areas);
    });
  }
  getSystemPages() {
    return this.service.getSystemPages().subscribe((items) => {
      this.systemPages = items.response.systemPages;
      console.log(this.systemPages);
    });
  }

  onSubmit(form: NgForm) {
    const dto = form.value;
    console.log('dto : ', dto);

    if (dto) {
      dto.permissions = this.tree.getAllCheckedNodes().filter(x => +x < 1000).map(x => +x);
      if (!this.UserId) {
        this.service.createUser(dto).subscribe(
          (res) => {

            this.notify.success(
              'User has been created successfully',
              'SUCCESS OPERATION'
            );
            this.router.navigate(['users/users-list']);
          },
          (error) => {
            this.notify.error(error.message, 'FAILED OPERATION');

            console.log(error);
          }
        );
      } else {
        const edit = {
          name: dto.name,
          phoneNumber: dto.phoneNumber,
          isActive: dto.isActive,
          roleId: dto.roleId,
          geoZonesIds: dto.geoZones,
          permissions: this.tree.getAllCheckedNodes().filter(x => +x < 1000).map(x => +x)
        }
        this.service.UpdateUser(edit, this.UserId).subscribe(
          (res) => {

            this.notify.success(
              'User has been Edited successfully',
              'SUCCESS OPERATION'
            );
            this.router.navigate(['users/users-list']);
          },
          (error) => {
            this.notify.error(error.message, 'FAILED OPERATION');

            console.log(error);
          }
        );
      }
    }
  }

  backToList() {
    this.router.navigate(['users/users-list']);
  }
  roleChanged($event) {
    if ($event.roleId && $event.roleId != '') {
      this.service.getRoleById($event.roleId).subscribe((res) => {
        this.vm.checkedNodes = res.response.role.permissions.map(x => x.toString());
      });
    }
    else {
      this.vm.checkedNodes = [];
    }
  }
  //----Treee--

  public permissionList: GetPermissionsChild[];
  public cssClass: string = 'custom';
  getPermissions() {
    this.service.GetPermissions().subscribe((res) => {
      var arrayToTree = require('array-to-tree');
      let data = res.response.systemPages;
      let tree: GetPermissionsChild[] = arrayToTree(data, {
        parentProperty: 'parentId',
        childrenProperty: 'subChild',
        customID: 'id',
      });

      // PLEASE DON'T JUDGE US 
      // WE HAVE DEPLOYMENT AND THIS IS THE ONLY SOLUTION WE HAD 
      // WE NEED TO SEND UNIQUE SystemPagePermissionId AND THIS IS THE ONLY WAY FOUND
      // PLEASE INCREMENT THIS COUNTER FOR ANYONE WORKING ON THIS TO BE REFERENCE
      /// TIME SPENT ON THIS FUNCTION SO FAR = 4 hours

      for (var i = 0; i < tree.length; i++) {
        if (tree[i].subChild && tree[i].subChild.length > 0) {
          for (var j = 0; j < tree[i].subChild.length; j++) {
            if (tree[i].subChild[j].subChild && tree[i].subChild[j].subChild.length > 0) {
              tree[i].subChild[j].id = tree[i].subChild[j].id + 1000
            }
            tree[i].id = tree[i].id + 1000
          }
        }
      }
      this.permissionList = tree;
      if (this.UserId) {
        this.service.GetUserPermissionForEdit(this.UserId).subscribe(result => {
          this.vm.field = {
            dataSource: tree,
            id: 'id',
            text: 'name',
            child: 'subChild',
          };
          this.vm.checkedNodes = result.response.systemPagePermissionDtos.map(x => x.systemPagePermissionId.toString())
        });
      }
      else {
        this.vm.field = {
          dataSource: tree,
          id: 'id',
          text: 'name',
          child: 'subChild',
        };
      }
    });
  }
  public nodeChecked(args): void {
    //this.setRole=this.treeData;
  }
  TogglePassword() {
    var passwordInput = <HTMLInputElement>document.getElementById('loginPass');
    if (passwordInput.type === 'password') {
      passwordInput.type = 'text';
      this.showPass = true;
    } else {
      passwordInput.type = 'password';
      this.showPass = false;
    }
  }
}
