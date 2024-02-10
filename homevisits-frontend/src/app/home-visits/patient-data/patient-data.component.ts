import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { ClientService } from 'src/app/core/data-services/client.service';
import { NotifyService } from 'src/app/core/data-services/notify.service';
import { AgeSegmentList, ExportPatientData, PatientAddresses, SearchPatientsList, SearchPatientsSendToParent } from 'src/app/core/models/models';
import { BaseComponent } from 'src/app/core/permissions/base.component';
import { ActionsEnum } from 'src/app/core/permissions/models/actions';
import { PagesEnum } from 'src/app/core/permissions/models/pages';
import { PatientPhoneCreateComponent } from '../patient-phone-create/patient-phone-create.component';
import { PendingDialogComponent } from '../pending-dialog/pending-dialog.component';

@Component({
  selector: 'app-patient-data',
  templateUrl: './patient-data.component.html',
  styleUrls: ['./patient-data.component.css'],
})
export class PatientDataComponent extends BaseComponent implements OnInit {
  @Input() patientData: SearchPatientsList;
  segments: AgeSegmentList;
  name: string;
  phoneNumber: string;
  birthDate: Date;
  genderName: string;
  secondPhoneNumber: string;
  visitRelative: boolean;
  patientAddresses: PatientAddresses[];
  patientPhoneNumber = [];
  selectedAddress: string;
  //selectedRelativeType:number;
  exportPatientData: ExportPatientData;
  nextToVisitData: boolean;
  relative: boolean = false;
  notSure: boolean = false;
  isBackToSearch: boolean;
  relativebirthdate: Date;
  relativeAgeSegmentId: string;
  patientId: string;
  @Output() childPatientData = new EventEmitter<ExportPatientData>();
  @Output() backToSearchPatient = new EventEmitter<boolean>();
  constructor(
    private service: ClientService,
    public router: Router,
    public dialog: MatDialog,
    public notify: NotifyService,
    private route: ActivatedRoute
  ) {
    super(PagesEnum.Patient, ActionsEnum.View, router, notify);
  }

  ngOnInit(): void {
   
    if (this.patientData?.isTherePendingVisits) {
      this.openDialog();
    }
    this.route.params.subscribe(paramsId => { this.patientId = paramsId.patientId });
    if (this.patientId != undefined) {
      this.getPatientientByPatientID(this.patientId);
      this.getSegments();
    }
    else {
      this.getPatientData();
      this.getSegments();
    }
    console.log(this.patientData);

    this.exportPatientData = new ExportPatientData();
  }

  openPhoneDialog() {
    const dialogRef = this.dialog.open(PatientPhoneCreateComponent, {
      width: '570px',
      height: 'auto',
      data: this.patientData.userId,
    });

    dialogRef.afterClosed().subscribe((result) => {
      console.log(`Dialog result: ${result}`);
      this.getSearchPatients(result);
    });
  }

  openDialog() {
    const dialogRef = this.dialog.open(PendingDialogComponent, {
      width: '500px',
      height: '360px',
      data: this.patientData?.pendingVistis,
    });

    dialogRef.afterClosed().subscribe((result) => {
      console.log(`Dialog result: ${result}`);
    });
  }
  getSearchPatients(phone) {
    const x = {
      phoneNumber: phone,
    };
    this.patientPhoneNumber.push({ phoneNumber: phone });
    this.service.searchPatients(x).subscribe((res) => {
      this.patientPhoneNumber = res?.response[0]?.patientPhoneNumbers;
    });
  }

  getSegments() {
    return this.service.getAllAgeSegments().subscribe((items) => {
      this.segments = items.response;
    });
  }
  getPatientData() {
    this.name = this.patientData.name;
    this.phoneNumber = this.patientData.phoneNumber;
    this.genderName = this.patientData.genderName;
    //this.secondPhoneNumber=this.patientData;
    this.birthDate = this.patientData.birthDate;
    this.patientAddresses = this.patientData.patientAddresses;
    this.patientPhoneNumber = this.patientData.patientPhoneNumbers;
  }
  gotoCreatePhoneNumber() {
    this.router.navigate(['/visits/patients-phones', this.patientData.userId]);
  }
  getPatientientByPatientID(id: string) {
    this.service.getPatient(id).subscribe((res) => {
      this.name = res.response.name;
      this.phoneNumber = res.response.phoneNumber;
      this.genderName = res.response.genderName;
      this.birthDate = res.response.birthDate;
      this.patientAddresses = res.response.patientAddresses;
    });
  }
  sendPatientData() {}
  backTosearch() {
    this.isBackToSearch = true;
    this.backToSearchPatient.emit(this.isBackToSearch);
  }
  gotoVisitData() {
    
    if (this.selectedAddress) {
      this.nextToVisitData = true;
      if (this.patientId != undefined) {
        this.exportPatientData.patientId = this.patientId;
        this.exportPatientData.geoZoneId = this.patientAddresses.find(
          (x) => x.patientAddressId == this.selectedAddress
        ).geoZoneId;
      } else {
        this.exportPatientData.patientId = this.patientData.userId;
        this.exportPatientData.geoZoneId = this.patientData.patientAddresses.find(
          (x) => x.patientAddressId == this.selectedAddress
        ).geoZoneId;
      }
      if (this.relative) {
        this.exportPatientData.relativeType = 2;
        
      }
      else {
        this.exportPatientData.relativeType = 1;
        this.notSure = null;
        this.relativeAgeSegmentId = null;
        this.relativebirthdate = null;
      }
      this.exportPatientData.relativeAgeSegmentId = this.relativeAgeSegmentId;
      this.exportPatientData.iamNotSure= this.notSure;
      this.exportPatientData.relativeDateOfBirth = this.relativebirthdate;
      this.exportPatientData.patientAddressId = this.selectedAddress;
      this.exportPatientData.isShowVisitData = this.nextToVisitData;
      if (this.patientId != undefined) {
        //this.router.navigate(['visits/visit-data', {patientId: this.patientId, addressId: this.exportPatientData.geoZoneId,relativeType:this.exportPatientData.relativeType }]);
        this.router.navigate(['visits/visit-data'], { queryParams: { patientId: this.patientId, geoZoneId: this.exportPatientData.geoZoneId, relativeType: this.exportPatientData.relativeType, addressId: this.exportPatientData.patientAddressId , ageSegmentId: this.relativeAgeSegmentId , notSure: this.notSure , relativeBirthDate: this.relativebirthdate} });
      }
      else {
        this.childPatientData.emit(this.exportPatientData);
      }
    } else {
      this.notify.error('Please Choose Address', 'FAILED OPERATION');
    }
  }
  gotoPatientSearch() {
    this.router.navigate(['visits/search-patients']);
  }
  gotoCreatePatientAddress() {
    let relativeType;
    if (this.relative) {
      relativeType = 2;
    } else {
      relativeType = 1;
    }
    if (this.patientId != undefined) {
      this.router.navigate([
        '/visits/patients-address',
        this.patientId,
        relativeType,
      ]);
    } else {
      this.router.navigate([
        '/visits/patients-address',
        this.patientData.userId,
        relativeType,
      ]);
    }
  }
  filterProducts(event) {
    var target = event.target;
  }
}
