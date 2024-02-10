import { Directive, Input, TemplateRef, ViewContainerRef } from '@angular/core';
import { PageAction } from 'src/app/core/permissions/models/pageAction';
import { PermissionService } from 'src/app/core/permissions/PermissionManager.service';

@Directive({
  selector: '[appIsGranted]'
})
export class IsGrantedDirective {
  constructor(
    private templateRef: TemplateRef<any>,
    private viewContainer: ViewContainerRef,
    private permissionService: PermissionService
  ) { }
  @Input() set appIsGranted(pageActionEnums: Array<number>) {
    let pageAction : PageAction = {
        page : pageActionEnums[0],
        action : pageActionEnums[1]
    }
    this.isGranted(pageAction);
  }
  private isGranted(pageAction : PageAction) {
    if (this.permissionService.isGranted(pageAction.page,pageAction.action)) {
      this.viewContainer.createEmbeddedView(this.templateRef);
    } else {
      this.viewContainer.clear();
      //this.viewContainer.createEmbeddedView(this.templateRef);
    }
  }
}