import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { NotifyService } from 'src/app/core/data-services/notify.service';
import {
  ExportPatientData,
  SearchPatientsList,
  SearchPatientsSendToParent,
} from 'src/app/core/models/models';
import { BaseComponent } from 'src/app/core/permissions/base.component';
import { ActionsEnum } from 'src/app/core/permissions/models/actions';
import { PagesEnum } from 'src/app/core/permissions/models/pages';

@Component({
  selector: 'app-home-visits-create',
  templateUrl: './home-visits-create.component.html',
  styleUrls: ['./home-visits-create.component.css'],
})
export class HomeVisitsCreateComponent extends BaseComponent implements OnInit {
showSearchPatient:boolean=true;
showPatientData:boolean=false;
nextToVisitData:boolean=false;
//showPatientCreate:boolean=false;
relativeAgeSegmentId : string;
iamNotSure : boolean;
relativeDateOfBirth : Date;
patientId:string;
patientData:SearchPatientsList;
sentData:SearchPatientsSendToParent;
exportedPatientData:ExportPatientData;
relativeType:number;
addressPatientId:string;
selectedTimeZoneFrameGeoZoneId;

  constructor(
    private route: ActivatedRoute,
    public router: Router,
    public notify: NotifyService
  ) {
    super(PagesEnum.AddNewVisit, ActionsEnum.Create, router, notify);
  }

  ngOnInit(): void {
    this.selectedTimeZoneFrameGeoZoneId =  this.route.snapshot.paramMap.get('timeZoneFrameGeoZoneId');
  }
  getSentData(event) {
    this.sentData = event;
    this.showSearchPatient = this.sentData.isShow;
    this.patientData = this.sentData.patientData;
  }

  sendPatientId() {
    return this.patientData;
  }
  getPatientData(event)
  {
    
this.exportedPatientData=event;
if(this.exportedPatientData.patientId!=undefined)
{
  this.patientId=this.exportedPatientData.patientId;
}
if(this.exportedPatientData.relativeAgeSegmentId != undefined){
  this.relativeAgeSegmentId = this.exportedPatientData.relativeAgeSegmentId;
}
if(this.exportedPatientData.relativeDateOfBirth != undefined){
  this.relativeDateOfBirth = this.exportedPatientData.relativeDateOfBirth;
}
if(this.exportedPatientData.iamNotSure != undefined){
  this.iamNotSure = this.exportedPatientData.iamNotSure;
}
this.relativeType=this.exportedPatientData.relativeType;
this.addressPatientId=this.exportedPatientData.patientAddressId;
this.nextToVisitData=this.exportedPatientData.isShowVisitData;
  }
  sendToVisit() {
    return this.exportedPatientData;
  }
  backToSearchPatient(event) {
    this.showSearchPatient = event;
  }
}
