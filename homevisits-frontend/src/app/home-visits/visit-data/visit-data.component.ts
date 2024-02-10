import { formatDate } from '@angular/common';
import {
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
  ViewChild,
} from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { CountdownComponent } from 'ngx-countdown';
import { debounce } from 'rxjs/operators';
import { ClientService } from 'src/app/core/data-services/client.service';
import { NotifyService } from 'src/app/core/data-services/notify.service';
import {
  AgeSegmentItem,
  AgeSegmentList,
  Attachment,
  ChemistListItem,
  Chemists,
  CreateHoldVisit,
  ExportPatientData,
  GetAvailableVisitsInAreaList,
} from 'src/app/core/models/models';
import { BaseComponent } from 'src/app/core/permissions/base.component';
import { ActionsEnum } from 'src/app/core/permissions/models/actions';
import { PagesEnum } from 'src/app/core/permissions/models/pages';
import { VisitLostTimeComponent } from '../visit-lost-time/visit-lost-time.component';

@Component({
  selector: 'app-visit-data',
  templateUrl: './visit-data.component.html',
  styleUrls: ['./visit-data.component.css'],
})
export class VisitDataComponent extends BaseComponent implements OnInit {
  @ViewChild('cd') countdown: CountdownComponent;
  isActiveTimeStart = '';
  ShowSearcData: boolean;
  selectedType = '';
  timeZoneGeoZoneId: string;
  chemistId: string = null;
  times: GetAvailableVisitsInAreaList[];
  @ViewChild('visitForm') visitForm: NgForm;
  @Input() recievedPatientData: ExportPatientData;
  showAgeSegment: boolean = false;
  relativeAgeSegmentId: string;
  visitDate = formatDate(new Date(), 'yyyy-MM-dd', 'en_US');
  chemists: Chemists[];
  fileToUpload: File = null;
  plannedNoOfPatients: number = 1;
  url: any;
  url2: any;
  atachments: Attachment[] = [];
  createAttachment: Attachment;
  segments: AgeSegmentList;
  loading;
  setSpecificTime: boolean = false;
  inValidDate: boolean;
  noZeroPattern = '^[1-9][0-9]*(.[0-9]+)?|0+.[0-9]*[1-9][0-9]*$';
  sub: any;
  submitted: boolean = false;
  patientId: string;
  addressId: string;
  relativeType: number;
  geoZoneId: string;
  isStartCount: boolean = false;
  notSure: boolean = false;
  isBackToSearch: boolean;
  relativebirthdate: Date;
  allowedDates;
  startDate;
  endDate;
  isTimeSlotChecked: boolean = false;
  selectedTimeZoneFrameGeoZoneId;
  selectedChemistId;
  selectedVisitDate;
  selectedGeoZoneId;
  constructor(
    private service: ClientService,
    private route: ActivatedRoute,
    public notify: NotifyService,
    public router: Router,
    public dialog: MatDialog
  ) {
    super(PagesEnum.ViewVisit, ActionsEnum.View, router, notify);
  }

  ngOnInit(): void {
    
    this.sub = this.route.queryParams.subscribe((params) => {
      this.patientId = params['patientId'];
      this.addressId = params['addressId'];
      this.relativeType = +params['relativeType'];
      this.geoZoneId = params['geoZoneId'];
      this.relativeAgeSegmentId = params['ageSegmentId'];
      this.notSure = params['notSure'];
      this.relativebirthdate = params['relativeBirthDate'];
    });
    this.selectedTimeZoneFrameGeoZoneId = this.route.snapshot.paramMap.get(
      'timeZoneFrameGeoZoneId'
    );
    this.selectedVisitDate = this.route.snapshot.paramMap.get('visitDate');
    this.selectedChemistId = this.route.snapshot.paramMap.get('chemistId');
    this.selectedGeoZoneId = this.route.snapshot.paramMap.get('geoZoneId');

    if (
      this.recievedPatientData != undefined &&
      this.recievedPatientData.relativeType == 2
    ) {
      this.showAgeSegment = true;
      this.getSegments();
    } else if (this.relativeType == 2) {
      this.showAgeSegment = true;
      this.getSegments();
    }
    this.getAllowedDate();
    // this.checkType()
    if (
      this.selectedTimeZoneFrameGeoZoneId &&
      this.selectedVisitDate &&
      this.selectedGeoZoneId &&
      this.selectedChemistId &&
      this.selectedGeoZoneId == this.recievedPatientData.geoZoneId
    ) {
      this.service
        .GetAvailableVisitsForChemist(
          this.selectedChemistId,
          this.selectedGeoZoneId,
          this.selectedVisitDate
        )
        .subscribe(
          (items) => {
            this.getChemistList(this.selectedGeoZoneId);
            this.times = items.response;
            this.loading = false;
            this.visitDate = this.selectedVisitDate;
            this.chemistId = this.selectedChemistId;
            this.geoZoneId = this.selectedGeoZoneId;
            this.selectedType = 'selectbychemist';
            this.startCount(this.selectedTimeZoneFrameGeoZoneId);
          },
          (error) => {
            this.notify.error(error.message, 'FAILED OPERATION');
            console.log(error);
            this.loading = false;
          }
        );
    } else if (
      this.selectedTimeZoneFrameGeoZoneId &&
      this.selectedVisitDate &&
      this.selectedGeoZoneId &&
      this.selectedGeoZoneId == this.recievedPatientData.geoZoneId
    ) {
      this.service
        .GetAvailableVisitsInArea(
          this.selectedGeoZoneId,
          this.selectedVisitDate
        )
        .subscribe(
          (res) => {
            this.times = res.response;
            this.loading = false;
            this.visitDate = this.selectedVisitDate;
            this.geoZoneId = this.selectedGeoZoneId;
            this.selectedType = 'selectbytime';
            this.startCount(this.selectedTimeZoneFrameGeoZoneId);
          },
          (error) => {
            this.notify.error(error.message, 'FAILED OPERATION');
            console.log(error);
            this.loading = false;
          }
        );
    }
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
  onSubmit(form: NgForm) {
    
    this.submitted = true;
    console.log('Your form data : ', form.value);
    const dto = form.value;
    if (dto) {
      // if (this.timeZoneGeoZoneId == undefined ) {
      //   this.notify.error('Please Choose Time Slot', 'FAILED OPERATION');
      //   this.submitted = false;
      // }
      // else if (!this.selectedType && dto.chemistId == null) {
      //   this.notify.error('Please Choose Chemist And Time', 'FAILED OPERATION');
      //   this.submitted = false;
      // }

      //else {
      if (this.timeZoneGeoZoneId) {
        dto.timeZoneGeoZoneId = this.timeZoneGeoZoneId;
      }
      if (this.setSpecificTime) {
        dto.visitTime = dto.visitTime;
      }
      if (this.recievedPatientData != undefined) {
        dto.patientAddressId = this.recievedPatientData.patientAddressId;
        dto.patientId = this.recievedPatientData.patientId;
        dto.visitTypeId = this.recievedPatientData.relativeType;
        dto.relativeAgeSegmentId = this.recievedPatientData.relativeAgeSegmentId;
        dto.iamNotSure = this.recievedPatientData.iamNotSure;
        dto.relativeDateOfBirth = this.recievedPatientData.relativeDateOfBirth;
      } else {
        dto.patientAddressId = this.addressId;
        dto.patientId = this.patientId;
        dto.visitTypeId = this.relativeType;
      }

      if (this.atachments.length > 0) {
        dto.atachments = this.atachments;
      } else {
        dto.atachments = [];
      }

      this.service.createVisitByChemistApp(dto).subscribe(
        (res) => {
          console.log(res);
          if (res.response) {
            this.router.navigate(['visits/visits-list']);
            this.notify.saved();
            this.submitted = false;
          } else {
            this.notify.error(res.message, 'FAILED OPERATION');
            this.submitted = false;
          }
        },
        (error) => {
          this.notify.error(error.message, 'FAILED OPERATION');
          this.submitted = false;
          console.log(error);
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
      noOfPatients: +this.plannedNoOfPatients,
      deviceSerialNo: '0',
    };
    this.service.createholdVisit(x).subscribe(
      (res) => {
        console.log(res);
        this.isStartCount = true;
        this.countdown.begin();
        this.isTimeSlotChecked = true;
        this.notify.success('Counter Start Successfuly', 'SUCCESS OPERATION');
      },
      (err) => {
        this.isStartCount = false;
        this.countdown.restart();

        this.isTimeSlotChecked = false;
        this.notify.error(err.message, 'FAILED OPERATION');
      }
    );
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
      this.submitted = false;
      this.inValidDate = true;
      this.times = [];
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
          this.submitted = false;
          console.log(error);
          this.loading = false;
        }
      );
    }
  }
  visitDateChanged() {
    this.checkType();
  }
  checkType() {
    if (this.visitDate != undefined && this.selectedType == 'selectbytime') {
      if (this.recievedPatientData != undefined) {
        this.getTimeSlot(this.recievedPatientData.geoZoneId, this.visitDate);
      } else {
        this.getTimeSlot(this.geoZoneId, this.visitDate);
      }
    }
    if (this.visitDate != undefined && this.selectedType == 'selectbychemist') {
      if (this.recievedPatientData != undefined) {
        this.getChemistTimeSlot(
          this.chemistId,
          this.recievedPatientData.geoZoneId,
          this.visitDate
        );
      } else {
        this.getChemistTimeSlot(this.chemistId, this.geoZoneId, this.visitDate);
      }
    }
  }
  getChemistList(geoZoneId: string) {
    return this.service.getChemistListItem(geoZoneId).subscribe((items) => {
      this.chemists = items.response;
    });
  }
  handleChange(evt) {
    //var target = evt.target;
    if (this.recievedPatientData != undefined) {
      this.getChemistList(this.recievedPatientData.geoZoneId);
    } else {
      this.getChemistList(this.geoZoneId);
    }

    this.times = [];
    //this.checkType();
  }
  timeChange(evt) {
    //var target = evt.target;
    this.chemistId = null;
    this.times = [];
    this.checkType();
  }
  selectAllChemistsChange(values: any) {
    if (values.currentTarget.checked) {
      let geZoneId = '';
      this.getChemistList(geZoneId);
    } else {
      if (this.recievedPatientData != undefined) {
        this.getChemistList(this.recievedPatientData.geoZoneId);
      } else {
        this.getChemistList(this.geoZoneId);
      }
    }
  }
  checkTimeChange(values: any) {
    if (values.currentTarget.checked) {
      this.setSpecificTime = true;
    } else {
      this.setSpecificTime = false;
    }
  }
  backFromCreateVisit() {
    this.ShowSearcData = true;
    this.router.navigate(['visits/visits-list']);
  }
  openVisitLostTimeDialog() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.autoFocus = true;
    dialogConfig.width = '100%';

    this.dialog.open(VisitLostTimeComponent, dialogConfig);

    this.dialog.afterAllClosed.subscribe((res) => {});
  }
  handleFileInput(files: FileList) {
    this.fileToUpload = files.item(0);
  }
  getFiles(event) {
    if (event.target.files && event.target.files[0]) {
      const formData = new FormData();
      let file = event.target.files[0];
      formData.append('fileKey', file);
      this.service.uplodaVisitFile(formData).subscribe((res) => {
        this.url2 = res.response;
        this.createAttachment = new Attachment();
        this.createAttachment.fileName = res.response;
        this.atachments.push(this.createAttachment);
      });
    }
  }
  getSegments() {
    return this.service.getAllAgeSegments().subscribe((items) => {
      this.segments = items.response;
    });
  }
  chemistChanged() {
    if (this.visitDate != undefined) {
      if (this.recievedPatientData != undefined) {
        this.getChemistTimeSlot(
          this.chemistId,
          this.recievedPatientData.geoZoneId,
          this.visitDate
        );
      } else {
        this.getChemistTimeSlot(this.chemistId, this.geoZoneId, this.visitDate);
      }
    }
  }
  getChemistTimeSlot(chemistId: string, id: string, date: any) {
    this.loading = true;
    this.times = [];
    var date1 = formatDate(new Date(), 'yyyy-MM-dd', 'en_US');
    var date2 = formatDate(date, 'yyyy-MM-dd', 'en_US');
    if (date2 < date1) {
      this.notify.error(
        'You Cannot Choose Date Less Than Today',
        'FAILED OPERATION'
      );
      this.times = [];
      this.loading = false;
      this.inValidDate = true;
    }
    // else if (chemistId == null) {
    //   this.notify.error("You Must Select Chemist", 'FAILED OPERATION');
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
}
