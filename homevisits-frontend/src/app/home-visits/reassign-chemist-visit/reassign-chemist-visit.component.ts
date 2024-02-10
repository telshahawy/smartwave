import { formatDate } from '@angular/common';
import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialog, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ClientService } from 'src/app/core/data-services/client.service';
import { NotifyService } from 'src/app/core/data-services/notify.service';
import { Chemists, GetAvailableVisitsInAreaList, ReasonsList, ReasonsObj } from 'src/app/core/models/models';
import { BaseComponent } from 'src/app/core/permissions/base.component';
import { ActionsEnum } from 'src/app/core/permissions/models/actions';
import { PagesEnum } from 'src/app/core/permissions/models/pages';

@Component({
  selector: 'app-reassign-chemist-visit',
  templateUrl: './reassign-chemist-visit.component.html',
  styleUrls: ['./reassign-chemist-visit.component.css']
})
export class ReassignChemistVisitComponent extends BaseComponent implements OnInit {

  allowedDates;
  startDate;
  endDate;
  reasons: any[] = [];
  ReasonObj: ReasonsObj;
  reasonId: number = null;
  chemistId: string = null;
  chemists: Chemists[];
  visitId: any;
  geZoneId: any;
  visitDate: any = formatDate(new Date(), 'yyyy-MM-dd', 'en_US');
  showChemistId: boolean = false;
  times: GetAvailableVisitsInAreaList[];
  loading;
  timeZoneGeoZoneId
  selectedType: boolean = true;


  @ViewChild('reassignChemistForm') reassignChemistForm: NgForm;
  constructor(private service: ClientService, public dialog: MatDialog, @Inject(MAT_DIALOG_DATA) data,
  public notify: NotifyService,public router: Router) {
    super(PagesEnum.HomeVisit,ActionsEnum.Reassign,router,notify)
    this.visitId = data.visitId;
    this.geZoneId = data.geZoneId;
  }

  ngOnInit(): void {
    this.getReasons();
    this.getChemistList(this.geZoneId);
    this.getAllowedDate()
  }
  openVisitLostTimeDialog() { }
  getAllowedDate() {
    this.service.systemParameters.dateVisit().subscribe(res => {

      this.allowedDates = res.response

      this.startDate = this.allowedDates.startDate
      this.startDate = formatDate(this.startDate, 'yyyy-MM-dd', 'en_US')

      this.endDate = this.allowedDates.endDate
      this.endDate = formatDate(this.endDate, 'yyyy-MM-dd', 'en_US')

      console.log(this.startDate);
      console.log(this.endDate);


    })
  }
  
  getReasons() {
    let ActionType = 10;
    return this.service.searchReasonByAction(ActionType).subscribe(items => {
      if (items.response.reasons.length > 0) {
        items.response.reasons.forEach(element => {
          this.ReasonObj = new ReasonsObj();
          this.ReasonObj.reasonId = element.reasonId;
          this.ReasonObj.reasonName = element.reasonName;
          this.reasons.push(this.ReasonObj);
        });
      }
    });
  }

  onSubmit(form: NgForm) {
    console.log('Your form data : ', form.value);
    const dto = form.value;
    if (dto) {
      if (dto.timeZoneGeoZoneId == undefined) {
        this.notify.error('Please Choose Time', 'FAILED OPERATION');
      }


      else {
        dto.reasonId = parseInt(dto.reasonId);
        dto.visitId = this.visitId;
        dto.visitActionTypeId = 10;
        dto.visitStatusTypeId = 10;
        this.service.sendChemistAction(dto).subscribe(res => {
          console.log(res);
          this.notify.saved();
          this.dialog.closeAll();

        }, error => {
          this.notify.error(error.message, 'FAILED OPERATION');
          console.log(error);
        });

      }
    }
  }
  selectAllChemistsChange(values: any) {
    if (values.currentTarget.checked) {
      let geZoneId = '';
      this.getChemistList(geZoneId);
    }
    else {
      this.getChemistList(this.geZoneId);
    }

  }
  getChemistList(geoZoneId: string) {
    return this.service.getChemistListItem(geoZoneId).subscribe(items => {
      this.chemists = items.response;
    });
  }
  close() {
    this.dialog.closeAll();
  }
  chemistChanged() {
    if (this.visitDate != undefined) {
      this.getChemistTimeSlot(this.chemistId, this.geZoneId, this.visitDate);
    }
  }
  getChemistTimeSlot(chemistId: string, id: string, date: any) {
    this.times = [];
    this.loading = true;
    var date1 = formatDate(new Date(), 'yyyy-MM-dd', 'en_US');
    var date2 = formatDate(date, 'yyyy-MM-dd', 'en_US');
    if (date2 < date1) {
      this.notify.error("You Cannot Choose Date Less Than Today", 'FAILED OPERATION');
      this.loading = false;
    }
    // else if (chemistId == null) {
    //   //this.notify.error("You Must Select Chemist", 'FAILED OPERATION');
    //   this.loading = false;
    // }
    else {
      return this.service.GetAvailableVisitsForChemist(chemistId, id, date).subscribe(items => {
        this.times = items.response;
        this.loading = false;
      }, error => {
        this.notify.error(error.message, 'FAILED OPERATION');
        console.log(error);
        this.loading = false;
      });
    }
  }

  visitDateChanged() {

    this.getChemistTimeSlot(this.chemistId, this.geZoneId, this.visitDate);

  }
}
