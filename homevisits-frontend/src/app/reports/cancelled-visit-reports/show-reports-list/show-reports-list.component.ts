import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { ClientService } from 'src/app/core/data-services/client.service';
import { IpagedList, RoleStatus } from 'src/app/core/models/models';
import { dialogConfirmComponent } from 'src/app/shared/component/dialog-confirm/dialog-confirm.component';


@Component({
  selector: 'app-show-reports-list',
  templateUrl: './show-reports-list.component.html',
  styleUrls: ['./show-reports-list.component.css']
})
export class ShowReportsListComponent implements OnInit {
  data;
  // data: IpagedList<any>;
  criteria: any;
  showForm = false;
  visitList;
  displayedColumns: string[] = [
    'visitId','age',
    'area',
    'cancellationReason',
    'cancellationTime',
    'gender',
    'cancelledBy',
    'mobileNumber',
    'PatientName',
    'visitDate',
    
  ];
  dataSource: MatTableDataSource<any>;
  sortKey: String;
  sortDir: String;
  loading;
  isShowEmpty: boolean;
  name: string;
  code: number;
  isActive: boolean = null;
  needExpertType: boolean = null;
  roleStatus = RoleStatus;
  roleSatuskeys = Object.keys;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;


  constructor(
    private service: ClientService,
    private router: Router,
    private dialog: MatDialog
  ) {}
  ngOnInit(): void {
    // this.criteria = new RolesSearchCriteria();
    this.loading = true;
    this.search();
   
  }
  search(page?: PageEvent) {


    this.loading = true;
    this.data = undefined;
    this.dataSource = new MatTableDataSource<any>(null);
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    this.isShowEmpty = false;

   
    this.service.reportsService.visitSubject$.subscribe(res=>{
      if(res){
        this.visitList=res
      if (res?.canceledVisitReports?.length > 0) {
        
        this.loading = false;
        this.data = res;
        this.dataSource = new MatTableDataSource<any>(
          this.data?.canceledVisitReports
        );
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
        this.isShowEmpty = false;
      } else {
        

        this.loading = false;
        this.isShowEmpty = true;

      }
      }else{
        this.router.navigate(['/client/reports/cancelled-visit-reports/create']);

      }
      
    });

  }



}
