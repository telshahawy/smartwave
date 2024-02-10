import { Component, Inject, OnInit } from '@angular/core';
import { MatDialog, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { ClientService } from 'src/app/core/data-services/client.service';
import { NotifyService } from 'src/app/core/data-services/notify.service';
import { GetVisitDetails } from 'src/app/core/models/models';
import { BaseComponent } from 'src/app/core/permissions/base.component';
import { ActionsEnum } from 'src/app/core/permissions/models/actions';
import { PagesEnum } from 'src/app/core/permissions/models/pages';

@Component({
  selector: 'app-visit-details',
  templateUrl: './visit-details.component.html',
  styleUrls: ['./visit-details.component.css'],
})
export class VisitDetailsComponent extends BaseComponent implements OnInit {
  visitId: any;
  loading;
  details: GetVisitDetails;
  constructor(
    private service: ClientService,
    public dialog: MatDialog,
    @Inject(MAT_DIALOG_DATA) data,
    public router: Router,
    public notify: NotifyService
  ) {
    super(PagesEnum.ViewVisit, ActionsEnum.View, router, notify);
    this.visitId = data.visitId;
  }

  ngOnInit(): void {
    this.getVisitDetails();
  }
  getVisitDetails() {
    this.loading = true;
    if (this.visitId) {
      return this.service.GetVisitDetailsById(this.visitId).subscribe((res) => {
        this.details = res.response;
        this.loading = false;
      });
    }
  }
  close() {
    this.dialog.closeAll();
  }
  print() {
    //     const printContent = document.getElementById("content");
    // const WindowPrt = window.open('', '', 'left=0,top=0,width=900height=900,toolbar=0,scrollbars=0,status=0');
    // WindowPrt.document.write(printContent.innerHTML);
    // WindowPrt.document.close();
    // WindowPrt.focus();
    // WindowPrt.print();
    // WindowPrt.close();
    window.print();
  }
}
