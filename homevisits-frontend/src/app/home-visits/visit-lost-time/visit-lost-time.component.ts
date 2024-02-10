import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialog, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { ClientService } from 'src/app/core/data-services/client.service';
import { NotifyService } from 'src/app/core/data-services/notify.service';
import { BaseComponent } from 'src/app/core/permissions/base.component';
import { ActionsEnum } from 'src/app/core/permissions/models/actions';
import { PagesEnum } from 'src/app/core/permissions/models/pages';

@Component({
  selector: 'app-visit-lost-time',
  templateUrl: './visit-lost-time.component.html',
  styleUrls: ['./visit-lost-time.component.css'],
})
export class VisitLostTimeComponent extends BaseComponent implements OnInit {
  @ViewChild('visitLostTimeForm') visitLostTimeForm: NgForm;
  constructor(
    private service: ClientService,
    public notify: NotifyService,
    public router : Router,
    @Inject(MAT_DIALOG_DATA) data,
    private dialog: MatDialog
  ) {
    super(PagesEnum.AddNewVisit, ActionsEnum.Create, router, notify);
  }

  ngOnInit(): void {}

  onSubmit(form: NgForm) {
    const dto = form.value;
    if (dto) {
      this.service.createLostVisitTime(dto).subscribe(
        (res) => {
          this.notify.saved();
          this.dialog.closeAll();

          //  this.router.navigate(['chemists/chemists-list']);
          //  this.notify.update();
        },
        (error) => {
          this.notify.error(
            error.message,
            'There Is A problem Now Please Try Again Later'
          );
        }
      );
    }
  }
  close() {
    this.dialog.closeAll();
  }
}
