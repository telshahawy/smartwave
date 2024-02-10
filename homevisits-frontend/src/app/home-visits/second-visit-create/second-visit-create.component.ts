import { formatDate } from '@angular/common';
import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import {
  MatDialog,
  MatDialogConfig,
  MAT_DIALOG_DATA,
} from '@angular/material/dialog';
import { Router } from '@angular/router';
import { CountdownComponent } from 'ngx-countdown';
import { timer } from 'rxjs';
import { ClientService } from 'src/app/core/data-services/client.service';
import { NotifyService } from 'src/app/core/data-services/notify.service';
import {
  GetAvailableVisitsInAreaList,
  ReasonsList,
  ReasonsObj,
  ReasonsPage,
} from 'src/app/core/models/models';
import { BaseComponent } from 'src/app/core/permissions/base.component';
import { ActionsEnum } from 'src/app/core/permissions/models/actions';
import { PagesEnum } from 'src/app/core/permissions/models/pages';
import { VisitLostTimeComponent } from '../visit-lost-time/visit-lost-time.component';

@Component({
  selector: 'app-second-visit-create',
  templateUrl: './second-visit-create.component.html',
  styleUrls: ['./second-visit-create.component.css'],
})
export class SecondVisitCreateComponent
  extends BaseComponent
  implements OnInit {
  @ViewChild('secondVisitForm') secondVisitForm: NgForm;
  @ViewChild('timeRadioControl') timeRadioControl;
  @ViewChild('cd') countdown: CountdownComponent;
  reasons: any[] = [];
  ReasonObj: ReasonsObj;
  chemistId: string = null;
  secondVisitReason: string = null;
  chemists: any[];
  visitDate: any = formatDate(new Date(), 'yyyy-MM-dd', 'en_US');
  visitId: any;
  geZoneId: any;
  selectedType = '';
  times: GetAvailableVisitsInAreaList[];
  loading;
  setSpecificTime: boolean = false;
  inValidDate: boolean;
  allowedDates;
  startDate;
  endDate;
  timeZoneGeoZoneId;
  maxMinutes;
  minMinutes;
  timeLeft: number = 60;
  interval;
  subscribeTimer: any;
  isStartCount: boolean = false;
  isTimeSlotChecked: boolean = false;
  patientNo;
  afterVisitTime = '';
  isActiveTimeStart = '';

  constructor(
    private service: ClientService,
    public dialog: MatDialog,
    @Inject(MAT_DIALOG_DATA) data,
    public notify: NotifyService,
    public router: Router
  ) {
    super(PagesEnum.ViewVisit, ActionsEnum.RequestSecondVisit, router, notify);
    this.visitId = data.visitId;
    this.geZoneId = data.geZoneId;
    this.patientNo = data.patientNo;
    console.log(data);
  }

  ngOnInit(): void {
    this.getReasons();
    this.getAllowedDate();

    // this.checkType()
  }
  afterFirstVisitChange(e) {
    console.log(e.target.value);
    if (this.selectedType == 'afterfirstvisit') {
      this.afterVisitTime = e.target.value;
    } else {
      this.afterVisitTime = '';
    }
  }
  getSecondVisitTimeZoneAndDate() {
    if (this.maxMinutes && this.minMinutes) {
      const x = {
        OriginVisitId: this.visitId,
        MaxMinutes: this.maxMinutes,
        MinMinutes: this.minMinutes,
      };
      this.service.getSecondVisitTimeZoneAndDate(x).subscribe(
        (res) => {
          this.visitDate = formatDate(
            res.response.secondVisitDate,
            'yyyy-MM-dd',
            'en_US'
          );
          this.timeZoneGeoZoneId = res.response.secondVisitTimeFrameId;
          this.notify.success(res.message, 'SUCCESS OPERATION');
        },
        (err) => {
          this.notify.error(err.message, 'FAILED OPERATION');
        }
      );
    }
  }

  startCount(e) {
    console.log(e);
    this.isActiveTimeStart = e;
    this.countdown.restart();
    this.isStartCount = false;
    this.isTimeSlotChecked = false;
    this.timeZoneGeoZoneId = e;

    const x = {
      timeZoneFrameGeoZoneId: e,
      noOfPatients: eval(this.patientNo),
      deviceSerialNo: '0',
    };
    this.service.createholdVisit(x).subscribe(
      (res) => {
        console.log(res);

        this.countdown.begin();

        this.isStartCount = true;
        this.isTimeSlotChecked = true;
        this.notify.success('Counter Start Successfuly', 'SUCCESS OPERATION');
      },
      (err) => {
        this.isStartCount = false;
        this.isTimeSlotChecked = false;
        this.countdown.restart();
        this.notify.error(err.message, 'FAILED OPERATION');
      }
    );
  }

  pauseTimer() {
    clearInterval(this.interval);
  }
  openVisitLostTimeDialog() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.autoFocus = true;
    dialogConfig.width = '100%';

    this.dialog.open(VisitLostTimeComponent, dialogConfig);

    this.dialog.afterAllClosed.subscribe((res) => {});
  }
  getAllowedDate() {
    this.service.systemParameters.dateVisit().subscribe((res) => {
      this.allowedDates = res.response;

      this.startDate = this.allowedDates.startDate;
      this.startDate = formatDate(this.startDate, 'yyyy-MM-dd', 'en_US');

      this.endDate = this.allowedDates.endDate;
      this.endDate = formatDate(this.endDate, 'yyyy-MM-dd', 'en_US');

      console.log(this.startDate);
      console.log(this.endDate);
    });
  }
  getReasons() {
    let ActionType = 9;
    return this.service.searchReasonByAction(ActionType).subscribe((items) => {
      if (items.response.reasons.length > 0) {
        items.response.reasons.forEach((element) => {
          this.ReasonObj = new ReasonsObj();
          this.ReasonObj.reasonId = element.reasonId;
          this.ReasonObj.reasonName = element.reasonName;
          this.reasons.push(this.ReasonObj);
        });
      }
    });
  }
  onSubmit(form: NgForm) {
    const dto = form.value;
    if (this.visitDate) {
      dto.visitDate = this.visitDate;
    }
    console.log('Your form data : ', form.value);

    if (dto) {
      if (this.timeZoneGeoZoneId == undefined) {
        this.notify.error('Please Choose Time Slot', 'FAILED OPERATION');
      }
      // if (
      //   dto.timeZoneGeoZoneId == undefined &&
      //   (dto.visitTime == undefined || dto.visitTime == '')
      // ) {
      //   this.notify.error('Please Choose Time', 'FAILED OPERATION');
      // }
      //  else if (!this.selectedType && dto.chemistId == null) {
      //   this.notify.error('Please Choose Chemist And Time', 'FAILED OPERATION');
      // }
      else {
        dto.originVisitId = this.visitId;
        if (this.timeZoneGeoZoneId) {
          dto.timeZoneGeoZoneId = this.timeZoneGeoZoneId;
        }
        dto.secondVisitReason = parseInt(dto.secondVisitReason);
        if (this.chemistId == null || this.chemistId == undefined) {
          dto.chemistId = null;
        }
        this.service.createSecondVisitByChemistApp(dto).subscribe(
          (res) => {
            console.log(res);
            this.notify.saved();
            this.dialog.closeAll();
          },
          (error) => {
            this.notify.error(error.message, 'FAILED OPERATION');
            console.log(error);
          }
        );
      }
    }
  }

  close() {
    this.dialog.closeAll();
  }
  getTimeSlot(id: string, date: any) {
    this.times = [];
    this.loading = true;
    var date1 = formatDate(new Date(), 'yyyy-MM-dd', 'en_US');
    var date2 = formatDate(date, 'yyyy-MM-dd', 'en_US');
    if (date2 < date1) {
      this.notify.error(
        'You Cannot Choose Date Less Than Today',
        'FAILED OPERATION'
      );
      this.inValidDate = true;
      this.loading = false;
    } else {
      this.inValidDate = false;
      return this.service.GetAvailableVisitsInArea(id, date).subscribe(
        (items) => {
          this.times = items.response;
          this.loading = false;
        },
        (error) => {
          this.notify.error(error.message, 'FAILED OPERATION');
          this.loading = false;
        }
      );
    }
  }
  visitDateChanged() {
    this.checkType();
  }
  handleChange(evt) {
    //var target = evt.target;
    this.getChemistList(this.geZoneId);
    this.times = [];
    //this.checkType();
  }
  timeChange(evt) {
    //var target = evt.target;
    this.chemistId = null;
    this.times = [];
    this.checkType();
  }
  getChemistList(geoZoneId: string) {
    return this.service.getChemistListItem(geoZoneId).subscribe((items) => {
      this.chemists = items.response;
    });
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
      this.notify.error(
        'You Cannot Choose Date Less Than Today',
        'FAILED OPERATION'
      );
      this.loading = false;
      this.inValidDate = true;
    }
    //  else if (chemistId == null) {
    //   this.notify.error('You Must Select Chemist', 'FAILED OPERATION');
    //   this.loading = false;
    //   this.inValidDate = false;
    // }
    else {
      this.inValidDate = false;
      return this.service
        .GetAvailableVisitsForChemist(chemistId, id, date)
        .subscribe(
          (items) => {
            this.times = items.response;
            this.loading = false;
          },
          (error) => {
            this.notify.error(error.message, 'FAILED OPERATION');
            console.log(error);
            this.loading = false;
          }
        );
    }
  }
  checkType() {
    if (this.visitDate != undefined && this.selectedType == 'selectbytime') {
      this.getTimeSlot(this.geZoneId, this.visitDate);
    }
    if (
      this.visitDate != undefined &&
      this.selectedType == 'selectbychemist' &&
      this.chemistId
    ) {
      this.getChemistTimeSlot(this.chemistId, this.geZoneId, this.visitDate);
    }
  }
  checkTimeChange(values: any) {
    if (values.currentTarget.checked) {
      this.setSpecificTime = true;
    } else {
      this.setSpecificTime = false;
    }
  }
}
