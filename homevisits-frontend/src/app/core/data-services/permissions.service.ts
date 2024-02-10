import { Injectable } from '@angular/core';
import { EndPoints } from './endpoints';
import { HttpClient } from '@angular/common/http';
import { DatePipe } from '@angular/common';
import { MenuPage } from '../permissions/models/menuPage';
import { Assets } from '../permissions/menu/assests';
import { from, Observable, of } from 'rxjs';
import { ClientService } from './client.service';
import { GetPermissionsChild, GetPermissionsDTO } from '../models/models';
import { map } from 'rxjs/operators';
import { UserPermission } from '../permissions/models/userPermisions';
import { ActionsEnum } from '../permissions/models/actions';

@Injectable({
  providedIn: 'root',
})
export class PermissionsService {
  // , private translate: TranslateService

  constructor(private client: ClientService, private datepipe: DatePipe) { }

  MapPermissionsWithMenu(result: GetPermissionsDTO) {

    var arrayToTree = require('array-to-tree');
    let pages: MenuPage[] = [];
    let data = result.response.systemPages.filter(x => x.isDisplayInMenue)
      .sort((a, b) => (a.position > b.position) ? 1 : -1);
    let tree: GetPermissionsChild[] = arrayToTree(data, {
      parentProperty: 'parentId',
      childrenProperty: 'children',
      customID: 'id',
    });

    tree.forEach(element => {
      if (!element.hasURL || element.permissions.some(x => x.permissionCode == ActionsEnum.View))
        pages.push(this.mapPage(element));
    });
    this.mapPermissions(result.response.systemPages);
    sessionStorage.setItem('menu', JSON.stringify(pages))
    return pages;
  }
  mapPermissions(systemPages: GetPermissionsChild[]) {
    let result: UserPermission[] = [];
    systemPages.forEach(x => {
      x.permissions.forEach(z => {
        result.push({
          pageActionId: z.systemPagePermissionId,
          actionCode: z.permissionCode,
          pageCode: +x.code
        })
      })
    });
    sessionStorage.setItem('permissions', JSON.stringify(result))
  }
  mapPage(element: GetPermissionsChild) {
    let menuPage: MenuPage = {
      systemPageId: element.id,
      parentId: element.parentId,
      code: +element.code,
      hasUrl: element.hasURL,
      name: element.name,
      position: 0,
      menuElements: {
        collapseId: 'collapse' + element.id,
        imgSrc: Assets.getImageByCode(+element.code),
        routerLink: Assets.getUrlByCode(+element.code),
      },
      children: []
    }
    if (element.children != null && element.children.length > 0) {
      element.children.forEach(x => {
        menuPage.children.push(this.mapPage(x))
      })
    }
    return menuPage;
  }
}

