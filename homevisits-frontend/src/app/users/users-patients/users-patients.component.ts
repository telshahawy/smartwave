import { Component, OnInit, ViewChild } from '@angular/core';
import { SearchParentCriteria, SearchPatientsList, SearchPatientsResponse, SearchPatientsSendToParent } from 'src/app/core/models/models';
import { Router } from '@angular/router'
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { ClientService } from 'src/app/core/data-services/client.service';

@Component({
  selector: 'app-users-patients',
  templateUrl: './users-patients.component.html',
  styleUrls: ['./users-patients.component.css']
})
export class UsersPatientsComponent implements OnInit {
  showSearchPatient:boolean=true;
  patientData:SearchPatientsList;
  sentData:SearchPatientsSendToParent;
 
  constructor(private router : Router, private service : ClientService) { }

  ngOnInit(): void {
  }
  getSentData(event)
  {
this.sentData=event;
this.showSearchPatient=this.sentData.isShow;
this.patientData=this.sentData.patientData;

  }
 
}
