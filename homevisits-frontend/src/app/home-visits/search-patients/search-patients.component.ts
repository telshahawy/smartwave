import {
  Component,
  EventEmitter,
  OnInit,
  Output,
  ViewChild,
} from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { ClientService } from 'src/app/core/data-services/client.service';
import { NotifyService } from 'src/app/core/data-services/notify.service';
import {
  IpagedList,
  SearchParentCriteria,
  SearchPatientsList,
  SearchPatientsResponse,
  SearchPatientsSendToParent,
} from 'src/app/core/models/models';
import { BaseComponent } from 'src/app/core/permissions/base.component';
import { ActionsEnum } from 'src/app/core/permissions/models/actions';
import { PagesEnum } from 'src/app/core/permissions/models/pages';

@Component({
  selector: 'app-search-patients',
  templateUrl: './search-patients.component.html',
  styleUrls: ['./search-patients.component.css'],
})
export class SearchPatientsComponent implements OnInit {
  data: SearchPatientsResponse;
  sendData: SearchPatientsSendToParent;
  criteria: SearchParentCriteria;
  mobNumberPattern = '([+]|0)[0-9]{1,}';
  submitted: boolean = false;

  displayedColumns: string[] = ['name', 'gender', 'dob', 'edit'];
  dataSource: MatTableDataSource<SearchPatientsList>;
  sortKey: String;
  sortDir: String;
  loading;
  @ViewChild('patientForm') patientForm: NgForm;
  isShowEmpty: boolean;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  @Output() notifyy = new EventEmitter<SearchPatientsSendToParent>();
  phoneNumber: string;
  constructor(private service: ClientService, public router: Router, public notifyService : NotifyService) {
  }

  ngOnInit(): void {
    this.criteria = new SearchPatientsList();
    this.isShowEmpty = true;
    //this.loading=true;
    // this.search();
    this.sendData = new SearchPatientsSendToParent();
  }
  search() {
    this.submitted = true;
    if (this.phoneNumber != undefined && this.phoneNumber != '') {
      this.criteria.phoneNumber = this.phoneNumber;
    } else {
      this.criteria.phoneNumber = undefined;
    }
    this.loading = true;
    this.isShowEmpty = false;
    this.data = undefined;
    return this.service.searchPatients(this.criteria).subscribe((items) => {
      console.log(items);
      if (items.response.length > 0) {
        this.loading = false;
        this.data = items;
        //this.data.response.pageSize=10;
        this.dataSource = new MatTableDataSource<any>(this.data.response);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
        this.isShowEmpty = false;
      } else if (this.phoneNumber) {
        this.router.navigate(['/visits/patients-create', this.phoneNumber]);
      } else {
        this.loading = false;
        // this.isShowEmpty=true;
        this.router.navigate(['/visits/patients-create']);
      }
    });
  }
  sendNotify(id: string) {
    let data = this.data.response.find((x) => x.userId == id);
    this.sendData.patientData = data;
    this.sendData.isShow = false;
    this.notifyy.emit(this.sendData);
  }
  gotoAddPatient() {
    this.router.navigate(['/visits/patients-create']);
  }
  onlyNumberKey(event) {
    return event.charCode == 8 || event.charCode == 0
      ? null
      : event.charCode >= 48 && event.charCode <= 57;
  }
}
