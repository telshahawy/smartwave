import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { ClientService } from 'src/app/core/data-services/client.service';
import { NotifyService } from 'src/app/core/data-services/notify.service';
import { AgeSegmentList, ExportPatientData, PatientAddresses, SearchPatientsList } from 'src/app/core/models/models';
import { PatientPhoneCreateComponent } from 'src/app/home-visits/patient-phone-create/patient-phone-create.component';
import { PendingDialogComponent } from 'src/app/home-visits/pending-dialog/pending-dialog.component';

@Component({
  selector: 'app-user-patient-data',
  templateUrl: './user-patient-data.component.html',
  styleUrls: ['./user-patient-data.component.css']
})
export class UserPatientDataComponent implements OnInit {
  @Input() patientData: SearchPatientsList;
  segments: AgeSegmentList;
  name: string;
  phoneNumber: string;
  birthDate: Date;
  genderName: string;
  secondPhoneNumber: string;
  visitRelative: boolean;
  patientAddresses: PatientAddresses[];
  patientPhoneNumber = []
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
  constructor(private service: ClientService, private router: Router, public dialog: MatDialog
    , private notify: NotifyService, private route: ActivatedRoute,) {

  }

  ngOnInit(): void {
   
    if (this.patientData?.isTherePendingVisits) {
      this.openDialog()

    }
    this.route.params.subscribe(paramsId => { this.patientId = paramsId.patientId });
    this.route.params.subscribe(paramsId => {this.phoneNumber = paramsId.phoneNumber});
    if (this.patientId != undefined) {
      //this.getPatientientByPatientID(this.patientId);
      this.getSearchPatientsByPhoneNumber(this.phoneNumber);
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
      data:this.patientId
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
      this.getSearchPatients(result)

    });
  }

  openDialog() {
    const dialogRef = this.dialog.open(PendingDialogComponent, {

      width: '500px',
      height: '360px',
      data: this.patientData?.pendingVistis
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
    });
  }
  getSearchPatientsByPhoneNumber(phone : string){
    
    const x ={
      phoneNumber:phone
    }
    this.service.searchPatients(x).subscribe(res => {
      this.name = res?.response[0]?.name;
      this.phoneNumber = res?.response[0]?.phoneNumber;
      this.genderName = res?.response[0]?.genderName;
      this.birthDate = res?.response[0]?.birthDate;
      this.patientAddresses = res?.response[0]?.patientAddresses;
      this.patientPhoneNumber= res?.response[0]?.patientPhoneNumbers;
    });
  }
  getSearchPatients(phone){
    const x ={
      phoneNumber:phone
    }
    this.patientPhoneNumber.push({phoneNumber:phone})
    this.service.searchPatients(x).subscribe(res=>{
      this.patientPhoneNumber =res?.response[0]?.patientPhoneNumbers
    })
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
    this.router.navigate(['/users/patients-phones', this.patientData.userId]);

  }
  getPatientientByPatientID(id: string) {
    
    this.service.getPatient(id).subscribe(res => {
      this.name = res.response.name;
      this.phoneNumber = res.response.phoneNumber;
      this.genderName = res.response.genderName;
      this.birthDate = res.response.birthDate;
      this.patientAddresses = res.response.patientAddresses;
    });
    console.log(this.phoneNumber);
  }
  backTosearch() {
    this.router.navigate(['users/patients-list']);
  }
  gotoCreatePatientAddress() {
    let relativeType;
    if (this.relative) {
      relativeType = 2;
    }
    else {
      relativeType = 1;
    }
    if (this.patientId != undefined) {
      this.router.navigate(['/users/patients-address', this.patientId, relativeType]);
    }
    else {
      this.router.navigate(['/users/patients-address', this.patientData.userId, relativeType]);
    }

  }

}
