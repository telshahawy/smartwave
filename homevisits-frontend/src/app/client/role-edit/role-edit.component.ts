import { DatePipe } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { MaskedTextBoxComponent } from '@syncfusion/ej2-angular-inputs';
import { NodeCheckEventArgs, TreeViewComponent } from '@syncfusion/ej2-angular-navigations';
import { ClientService } from 'src/app/core/data-services/client.service';
import { DataManager, Query, Predicate } from '@syncfusion/ej2-data';
import { EmitType } from "@syncfusion/ej2-base"
import {
  AreaLookup,
  GetPermissionsChild,
  RoleStatus,
  SystemPage,
  systemPagesList,
} from 'src/app/core/models/models';
import { AssignPermissionComponent } from '../assign-permission/assign-permission.component';
import { NotifyService } from 'src/app/core/data-services/notify.service';
import { element } from 'protractor';
import { BaseComponent } from 'src/app/core/permissions/base.component';
import { ActionsEnum } from 'src/app/core/permissions/models/actions';
import { PagesEnum } from 'src/app/core/permissions/models/pages';
import { emit } from 'process';
import { EventEmitter } from 'events';

@Component({
  selector: 'app-role-edit',
  templateUrl: './role-edit.component.html',
  styleUrls: ['./role-edit.component.css'],
})
export class RoleEditComponent extends BaseComponent implements OnInit {
  areas: AreaLookup[];
  isActive: boolean = false;
  roleId: string;
  systemPages: systemPagesList[];
  permissionsList: number[];
  systemPagesModel: SystemPage[];
  submitted: boolean = false;
  permissionList: GetPermissionsChild[];
  @ViewChild('roleForm') roleForm: NgForm;
  //tree -----
  @ViewChild('treeview', { static: false })
  public tree: TreeViewComponent;
  @ViewChild('maskObj', { static: false })
  public maskObj: MaskedTextBoxComponent;
  // @ViewChild("treeviewObj",{static:false})
  //  tree:TreeViewComponent;
  public checkedNodes: string[] = [];
  public checkNodes = [];
  //data: RoleAllPermissionsResponse[];
  checkedPermission: Array<number> = [];
  convert: any[];
  treeData: any;
  areasArray: any[] = [];
  filterArray: number[] = [];
  public vm: any = { field: undefined };
  // set the CheckBox to the TreeView
  public showCheckBox: boolean = true;
  //--------
  constructor(
    private service: ClientService,
    public notify: NotifyService,
    private route: ActivatedRoute,
    public router: Router,
    public datepipe: DatePipe,
    private dialog: MatDialog
  ) {
    super(PagesEnum.Roles, ActionsEnum.Update, router, notify);
    this.route.params.subscribe((paramsId) => {
      this.roleId = paramsId.roleId;
    });
    this.getSystemPages();
    this.getAreas();

    this.getRole(this.roleId);
  }

  ngOnInit(): void {
    // this.getPermissions();
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

    console.log('Your form data : ', form.value);
    const dto = form.value;
    dto.roleId = this.roleId;
    dto.permissions = this.tree.getAllCheckedNodes().filter(x=>+x < 1000).map(x=>+x);
    dto.description = dto.description.toString();
    dto.defaultPageId = parseInt(dto.defaultPageId);
    ////dto.isActive=true;
    if (dto.geoZones == '' || dto.geoZones == 'undefined') {
      dto.geoZones = [];
    }
    this.service.updateRole(dto).subscribe(
      (res) => {
        console.log(res);

        this.notify.update();
        this.submitted = false;
        this.router.navigate(['client/role-list']);
      },
      (error) => {
        this.notify.error(error.message, 'FAILED OPERATION');
        this.submitted = false;
      }
    );
  }

  getRole(id: string) {
    this.service.getRoleById(id).subscribe((res) => {
      this.roleForm.controls['name'].setValue(res.response.role.name);
      this.roleForm.controls['description'].setValue(
        res.response.role.description
      );
      this.roleForm.controls['roleId'].setValue(res.response.role.roleId);
      this.roleForm.controls['defaultPageId'].setValue(
        res.response.role.defaultPageId
      );
      this.roleForm.controls['isActive'].setValue(res.response.role.isActive);
      if (res.response.role.geoZones.length > 0) {
        // this.areasArray = res.response.role.geoZones.map(o => {
        //   return o.;
        // });
        this.roleForm.controls['geoZones'].setValue(res.response.role.geoZones);
      }
      // if(res.response.role.permissions.length>0)
      // {
      this.getPermissions(res.response.role.permissions);
      // }
    });
  }
  backToList() {
    this.router.navigate(['client/role-list']);
  }

  //----Treee--
  // public permissions: Object[] = [
  //   { permissionId: 1, children: [],permissionName: 'Australia', expanded: true },
  //   { permissionId: 2, children: [],permissionName: 'New South Wales' },
  //   { permissionId: 3, children: [], permissionName: 'Victoria' },
  //   { permissionId: 4, children: [],permissionName: 'South Australia' },

  // ];
  public permissions: Object[];

  getPermissions(rolePermissions: number[]) {
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
      this.vm.checkedNodes = rolePermissions.map(x=>x.toString());
    });
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
  // unCheckParents(args: NodeCheckEventArgs) {
  //   var treeItem = this.permissionList.find(x => x.id == args.data[0].id);
  //   if (treeItem && treeItem.subChild && treeItem.subChild.length > 0) {
  //     args.cancel = true
  //   }
  // }
}

