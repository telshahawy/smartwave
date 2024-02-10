import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { ClientService } from 'src/app/core/data-services/client.service';
import { NotifyService } from 'src/app/core/data-services/notify.service';
import { IpagedList, RoleStatus } from 'src/app/core/models/models';
import { BaseComponent } from 'src/app/core/permissions/base.component';
import { ActionsEnum } from 'src/app/core/permissions/models/actions';
import { PagesEnum } from 'src/app/core/permissions/models/pages';
import { dialogConfirmComponent } from 'src/app/shared/component/dialog-confirm/dialog-confirm.component';

@Component({
  selector: 'app-details-reports',
  templateUrl: './details-reports.component.html',
  styleUrls: ['./details-reports.component.css']
})
export class DetailsReportsComponent extends BaseComponent implements OnInit {

  data;
  // data: IpagedList<any>;
  criteria: any;
  showForm = false;
  visitList;
  displayedColumns: string[] = [
    'CHEMIST',
    'visitNo',
    'phoneNumber',
    'visitDate',
    'area',
    'country',
    'governates',
    'delayed',
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
    public router: Router,
    public notify: NotifyService,
    private dialog: MatDialog
  ) {
    super(PagesEnum.VisitReports, ActionsEnum.View, router, notify);
  }
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
      if (res?.visitReports?.length > 0) {
        
        this.loading = false;
        this.data = res;
        this.dataSource = new MatTableDataSource<any>(
          this.data?.visitReports
        );
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
        this.isShowEmpty = false;
      } else {
        

        this.loading = false;
        this.isShowEmpty = true;

      }
      }else{
        this.router.navigate(['/client/reports/visit-reports/create']);

      }
      
    });

  }


  //------Tree-----

  //-----------

}
