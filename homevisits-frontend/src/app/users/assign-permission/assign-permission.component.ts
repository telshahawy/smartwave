import { MaskedTextBoxComponent } from '@syncfusion/ej2-angular-inputs';

import { Component, Inject, Input, ViewChild } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialog } from '@angular/material/dialog';

import { TreeViewComponent } from '@syncfusion/ej2-angular-navigations';
import { DataManager, Query, Predicate } from '@syncfusion/ej2-data';
import { NotifyService } from 'src/app/core/data-services/notify.service';
import { TranslateService } from '@ngx-translate/core';
import { ClientService } from 'src/app/core/data-services/client.service';
import { BaseComponent } from 'src/app/core/permissions/base.component';
import { PagesEnum } from 'src/app/core/permissions/models/pages';
import { ActionsEnum } from 'src/app/core/permissions/models/actions';
import { Router } from '@angular/router';

@Component({
  selector: 'app-assign-permission',
  templateUrl: './assign-permission.component.html',
  styleUrls: ['./assign-permission.component.sass'],
})
export class AssignPermissionComponent extends BaseComponent {
  @ViewChild('treeview', { static: false })
  public tree: TreeViewComponent;
  @ViewChild('maskObj', { static: false })
  public maskObj: MaskedTextBoxComponent;
  // @ViewChild("treeviewObj",{static:false})
  //  tree:TreeViewComponent;

  public checkNodes = [];
  //data: RoleAllPermissionsResponse[];
  checkedPermission: Array<number> = [];
  convert: any[];
  treeData: any;
  roleId: number;
  constructor(
    private service: ClientService,
    @Inject(MAT_DIALOG_DATA) roleData,
    private dialog: MatDialog,
    public router: Router,
    public notify: NotifyService
  ) {
    super(PagesEnum.Roles, ActionsEnum.Create, router, notify);
    this.roleId = roleData;
  }
  // defined the array of data
  ngOnInit() {
    this.getPermissions();
  }

  //public countries: Object[];
  public countries: Object[] = [
    { permissionId: 1, permissionName: 'Australia', expanded: true },
    { permissionId: 2, permissionName: 'New South Wales' },
    { permissionId: 3, children: [], permissionName: 'Victoria' },
    { permissionId: 4, permissionName: 'South Australia' },
  ];
  // maps the appropriate column to

  public vm: any = { field: undefined };
  // set the CheckBox to the TreeView
  public showCheckBox: boolean = true;
  getPermissions() {
    // this.service.getRoleAllPermissions(this.roleId).subscribe(permission => {
    this.vm.field = {
      dataSource: this.countries,
      id: 'permissionId',
      text: 'permissionName',
    };
    //   this.countries = permission;
    //  this.vm.field = { dataSource: this.countries, id: 'permissionId', text: 'permissionName', child: 'children', isChecked: 'granted' };

    // })
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
      this.changeDataSource(this.countries);
    } else {
      let predicate = new Predicate('permissionName', 'contains', _text, true);
      let filteredList = new DataManager(this.countries).executeLocal(
        new Query().where(predicate)
      );
      for (var j = 0; j < filteredList.length; j++) {
        _filter.push(filteredList[j]['permissionId']);
        var filters = this.getFilterItems(filteredList[j], this.countries);
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
        let newList = new DataManager(this.countries).executeLocal(query);
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
