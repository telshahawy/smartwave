import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { ClientService } from 'src/app/core/data-services/client.service';
import { IpagedList, VisitListResponse, VisitsSearchCriteria, VisitsListDto, AreaLookup } from 'src/app/core/models/models';

@Component({
  selector: 'app-my-visits',
  templateUrl: './my-visits.component.html',
  styleUrls: ['./my-visits.component.css']
})
export class MyVisitsComponent  implements OnInit {
  showForm = false;
  data: IpagedList<VisitListResponse>;
  criteria: VisitsSearchCriteria;
  displayedColumns: string[] = [
    'chemistId',
    'visitdate',
    'patient name',
    'gender',
    'age',
    'mobile NO',
    'area',
    'chemist',
    'status',
    'edit',
    'log'
  ];
  
  dataSource: MatTableDataSource<VisitsListDto>;
  sortKey: String;
  sortDir: String;
  loading;
  isShowEmpty: boolean;
  visitDateFrom: Date;
  visitDateTo: Date;
  areas: AreaLookup[];
  selectedAreaId: string=null;
  visitStatusTypeId: number=null;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  constructor( private service: ClientService,
    private router: Router) { }

  ngOnInit(): void {
    this.criteria = new VisitsSearchCriteria();
    this.loading = true;
    this.getAreas();
    this.search();
  }
  getAreas() {
    return this.service.getAreas().subscribe((items) => {
      this.areas = items.response.geoZones;
    });
  }
  search(page?: PageEvent) {
    this.showForm=false;
    this.criteria.currentPageIndex = (page && page.pageIndex + 1) || 1;
    this.criteria.pageSize = (page && page.pageSize) || 5;

    if (this.visitDateFrom != undefined) {
      this.criteria.visitDateFrom = this.visitDateFrom;
    }
    if (this.visitDateTo != undefined) {
      this.criteria.visitDateTo = this.visitDateTo;
    }

   
    if (this.selectedAreaId != undefined&&this.selectedAreaId!=null) {
      this.criteria.geoZoneId = this.selectedAreaId;
    } else {
      this.criteria.geoZoneId = '';
    }
   
    if (this.visitStatusTypeId != undefined) {
      this.criteria.visitStatusTypeId = this.visitStatusTypeId;
    } else {
      this.criteria.visitStatusTypeId = null;
    }
   

    this.loading = true;
    this.data = undefined;
    return this.service.searchVisits(this.criteria).subscribe((items) => {
      console.log(items);
     
        this.loading = false;
        this.data = items;
        if (items.response.totalCount > 0) {
         
       // this.data.response.pageSize = 10;
        this.dataSource = new MatTableDataSource<any>(
          this.data.response.visits
        );
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
        this.isShowEmpty = false;
      } else {
        this.data.response.visits=[];
        this.loading = false;
        this.isShowEmpty = true;
      }
    });
  }
  clear(chemistForm: NgForm) {
    this.criteria=new VisitsSearchCriteria();
    this.visitDateTo = null;
    this.visitDateFrom = null;
    this.selectedAreaId = '';
    this.visitStatusTypeId=null;
    chemistForm.reset(this.search());
   
  
  }
}
