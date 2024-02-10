import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';

@Injectable({
  providedIn: 'root'
})
export class NotifyService {
 // , private translate: TranslateService
  constructor(private toastr: ToastrService) { }
  saved() {
    this.toastr.success('SAVED SUCCESSFUL', 'SUCCESS OPERATION');
  }

  delete() {
    this.toastr.success('DELETED SUCCESSFUL', 'SUCCESS OPERATION');
  }

  update() {
    this.toastr.success('EDITED SUCCESSFUL', 'SUCCESS OPERATION');
  }

  error(message: any, title: any) {
    this.toastr.error(message, title);
  }

  warning(message: any, title: any) {
    this.toastr.warning(message, title);
  }
  success(message: any, title: any) {
    this.toastr.success(message, title);
  }
}
