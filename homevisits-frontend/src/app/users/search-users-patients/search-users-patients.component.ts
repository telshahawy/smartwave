import { Component, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { ClientService } from 'src/app/core/data-services/client.service';
import { NotifyService } from 'src/app/core/data-services/notify.service';
import { SearchParentCriteria, SearchPatientsList, SearchPatientsResponse, SearchPatientsSendToParent } from 'src/app/core/models/models';
import { BaseComponent } from 'src/app/core/permissions/base.component';
import { ActionsEnum } from 'src/app/core/permissions/models/actions';
import { PagesEnum } from 'src/app/core/permissions/models/pages';

@Component({
  selector: 'app-search-users-patients',
  templateUrl: './search-users-patients.component.html',
  styleUrls: ['./search-users-patients.component.css']
})
export class SearchUsersPatientsComponent extends BaseComponent implements OnInit {
  data: SearchPatientsResponse;
  sendData: SearchPatientsSendToParent;
  criteria: SearchParentCriteria;
  mobNumberPattern = "([+]|0)[0-9]{1,}";
  submitted: boolean = false;
  patientsPage : PagesEnum = PagesEnum.Patient;
  displayedColumns: string[] = ['name', 'gender', 'dob', 'edit'];
  dataSource: MatTableDataSource<SearchPatientsList>;
  sortKey: String
  sortDir: String
  phoneNumber: string;
  isShowEmpty: boolean;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  loading;
  @ViewChild('patientForm') patientForm: NgForm;
  //@Output() notify = new EventEmitter<SearchPatientsSendToParent>();
  constructor(private service : ClientService, public router : Router , public notifyService: NotifyService) {
    super(PagesEnum.Patient, ActionsEnum.View, router, notifyService);
   }

  ngOnInit(): void {
    this.criteria = new SearchPatientsList();
    this.isShowEmpty = true;
    this.sendData = new SearchPatientsSendToParent();
  }
  search() {
    
    this.submitted=true
    if (this.phoneNumber != undefined && this.phoneNumber != "") {
      this.criteria.phoneNumber = this.phoneNumber;
    }
    else {
      this.criteria.phoneNumber = undefined;
    }
    this.loading = true;
    this.isShowEmpty = false;
    this.data = undefined;
    return this.service.searchPatients(this.criteria).subscribe(items => {
      console.log(items);
      if (items.response.length > 0) {

        this.loading = false;
        this.data = items;
        //this.data.response.pageSize=10;
        this.dataSource = new MatTableDataSource<any>(this.data.response);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
        this.isShowEmpty = false;

      }
      else if (this.phoneNumber) {
        this.router.navigate(['/users/patients-create', this.phoneNumber]);
      }
      else {
        this.loading = false;
        // this.isShowEmpty=true;    
        this.router.navigate(['/users/patients-create']);
      }
    });
  }
  gotoAddPatient() {
    this.router.navigate(['/users/patients-create']);
  }
  onlyNumberKey(event) {
    return (event.charCode == 8 || event.charCode == 0) ? null : event.charCode >= 48 && event.charCode <= 57;
  }
  patientData(id: string) {
    this.router.navigate(['/users/patients-data', id,this.phoneNumber]);
  }
}
