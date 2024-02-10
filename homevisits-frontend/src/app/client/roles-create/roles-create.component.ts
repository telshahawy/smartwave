import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { MaskedTextBoxComponent } from '@syncfusion/ej2-angular-inputs';
import { TreeViewComponent } from '@syncfusion/ej2-angular-navigations';
import { ClientService } from 'src/app/core/data-services/client.service';
import {
  AreaLookup,
  GetPermissionsChild,
  nodeList,
  RoleStatus,
  systemPagesList,
  systemPagesResponse,
} from 'src/app/core/models/models';
import { DataManager, Query, Predicate } from '@syncfusion/ej2-data';
import { AssignPermissionComponent } from '../assign-permission/assign-permission.component';
import { NotifyService } from 'src/app/core/data-services/notify.service';
import { BaseComponent } from 'src/app/core/permissions/base.component';
import { PagesEnum } from 'src/app/core/permissions/models/pages';
import { ActionsEnum } from 'src/app/core/permissions/models/actions';

@Component({
  selector: 'app-roles-create',
  templateUrl: './roles-create.component.html',
  styleUrls: ['./roles-create.component.css'],
})
export class RolesCreateComponent extends BaseComponent implements OnInit {
  areas: AreaLookup[];
  systemPages: systemPagesList[];
  child: nodeList[];
  //tree -----
  @ViewChild('treeview', { static: false })
  public tree: TreeViewComponent;
  @ViewChild('maskObj', { static: false })
  public maskObj: MaskedTextBoxComponent;
  // @ViewChild("treeviewObj",{static:false})
  //  tree:TreeViewComponent;
  @ViewChild('samples')
  public checkNodes = [];
  //data: RoleAllPermissionsResponse[];
  checkedPermission: Array<number> = [];
  convert: any[];
  treeData: any;
  roleId: number;
  submitted: boolean = false;
  defaultPageId: number = null;
  isActive: boolean = false;
  permissionList: GetPermissionsChild[];
  public vm: any = { field: undefined };
  // set the CheckBox to the TreeView
  public showCheckBox: boolean = true;
  //--------
  constructor(
    private service: ClientService,
    public notify: NotifyService,
    private dialog: MatDialog,
    public router: Router
  ) {
    super(PagesEnum.Roles, ActionsEnum.Create, router, notify);
  }

  ngOnInit(): void {
    this.getSystemPages();
    this.getPermissions();
    this.getAreas();
  }

  getAreas() {
    return this.service.getAreas().subscribe((items) => {
      this.areas = items.response.geoZones;
    });
  }
  getSystemPages() {
    return this.service.getSystemPages().subscribe((items) => {
      this.systemPages = items.response.systemPages;
      console.log(this.systemPages);
    });
  }

  onSubmit(form: NgForm) {
    this.submitted = true;
    const dto = form.value;
      dto.permissions = this.tree.getAllCheckedNodes().filter(x=>+x < 1000).map(x=>+x);
      dto.defaultPageId = parseInt(dto.defaultPageId);
      dto.description = dto.description.toString();
      if (dto.geoZones == '' || dto.geoZones == 'undefined') {
        dto.geoZones = [];
      }

      this.service.createRole(dto).subscribe(
        (res) => {
          console.log(res);
          this.submitted = false;
          //
          this.notify.success(
            'ٌRole has been created successfully',
            'SUCCESS OPERATION'
          );
          this.router.navigate(['client/role-list']);
        },
        (error) => {
          this.notify.error(error.message, 'FAILED OPERATION');
          this.submitted = false;
          console.log(error);
        }
      );
    }

  backToList() {
    this.router.navigate(['client/role-list']);
  }

  //----Treee--

  public permissions: Object[];
  public cssClass: string = 'custom';
  getPermissions() {
    
    var arrayToTree = require('array-to-tree');
    this.service.GetPermissionsForUpdate().subscribe((res) => {
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
              tree[i].subChild[j].id = tree[i].subChild[j].id+ 1000
            }
            tree[i].id =  tree[i].id +1000
          }
        }
      }
      this.permissionList = tree;
      this.vm.field = {
        dataSource: tree,
        id: 'id',
        text: 'name',
        child: 'subChild',
      };
    });
  }
  public nodeChecked(args): void {
    //this.setRole=this.treeData;
  }

  savePermission() {
    //   for (var i = 0, len = this.tree.checkedNodes.length; i < len; ++i) {
    //     this.checkedPermission.push(Number(this.tree.checkedNodes[i]));
    //   }
    //   this.service.setRoleAllPermissions(this.roleId, this.checkedPermission).subscribe(permission => {
    //     this.dialog.closeAll();
    //     this.notify.update();
    //   }, error => {
    //     this.notify.error(this.translate.instant(err.errorMessage), 'حدث خطأ');
    //   });
  }

  changeDataSource(data) {
    for (let i = 0; i < data.length; i++) {
      let dataId = String(data[i]['permissionId']);
      if (
        this.tree.checkedNodes.indexOf(dataId) !== -1 &&
        this.checkNodes.indexOf(dataId) === -1
      )
        this.checkNodes.push(dataId);
    }
    this.tree.fields = {
      dataSource: data,
      id: 'permissionId',
      text: 'permissionName',
    };
  }

  //Filtering the TreeNodes
  searchNodes(args?) {
    let _text = this.maskObj.element.value;
    let predicats = [],
      _array = [],
      _filter = [];
    if (_text == '') {
      this.changeDataSource(this.permissions);
    } else {
      let predicate = new Predicate('permissionName', 'contains', _text, true);
      let filteredList = new DataManager(this.permissions).executeLocal(
        new Query().where(predicate)
      );
      for (var j = 0; j < filteredList.length; j++) {
        _filter.push(filteredList[j]['permissionId']);
        var filters = this.getFilterItems(filteredList[j], this.permissions);
        for (var i = 0; i < filters.length; i++) {
          if (_array.indexOf(filters[i]) == -1 && filters[i] != null) {
            _array.push(filters[i]);
            predicats.push(
              new Predicate('permissionId', 'equal', filters[i], false)
            );
          }
        }
      }
      if (predicats.length == 0) {
        this.changeDataSource([]);
      } else {
        let query = new Query().where(Predicate.or(predicats));
        let newList = new DataManager(this.permissions).executeLocal(query);
        this.changeDataSource(newList);
        var proxy = this;
        setTimeout(function (this) {
          proxy.tree.expandAll();
        }, 200);
      }
    }
  }
  dataBounded(args) {
    this.tree.checkedNodes = this.checkNodes;
    console.log(this.checkNodes);
    console.log(this.tree.checkedNodes);
  }

  //Find the Parent Nodes for corresponding childs
  getFilterItems(fList, list) {
    let nodes = [];
    nodes.push(fList['permissionId']);
    let query2 = new Query().where(
      'permissionId',
      'equal',
      fList['pid'],
      false
    );
    let fList1 = new DataManager(list).executeLocal(query2);
    if (fList1.length != 0) {
      let pNode = this.getFilterItems(fList1[0], list);
      for (var i = 0; i < pNode.length; i++) {
        if (nodes.indexOf(pNode[i]) == -1 && pNode[i] != null)
          nodes.push(pNode[i]);
      }
      return nodes;
    }
    return nodes;
  }
}
