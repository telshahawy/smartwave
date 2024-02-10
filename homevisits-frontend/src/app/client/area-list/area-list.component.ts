import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { ClientService } from 'src/app/core/data-services/client.service';
import { NotifyService } from 'src/app/core/data-services/notify.service';
import { areaActiveStatus, AreaListResponse, AreaLookup, AreaSearchCriteria, AreasListDto, ChemistsSearchCriteria, CountryLookup, GovernateLookup, IpagedList } from 'src/app/core/models/models';
import { BaseComponent } from 'src/app/core/permissions/base.component';
import { ActionsEnum } from 'src/app/core/permissions/models/actions';
import { PagesEnum } from 'src/app/core/permissions/models/pages';
import { dialogConfirmComponent } from 'src/app/shared/component/dialog-confirm/dialog-confirm.component';

@Component({
  selector: 'app-area-list',
  templateUrl: './area-list.component.html',
  styleUrls: ['./area-list.component.css']
})
export class AreaListComponent extends BaseComponent implements OnInit {
  areaStatusType: boolean=null;
  data: IpagedList<AreaListResponse>;
  criteria: AreaSearchCriteria;
  countries: CountryLookup[];
  governats: GovernateLookup[];
   areaStatus = areaActiveStatus;
   showForm = false;
   loading;
  isShowEmpty: boolean;
  selectedGovernateId: string='null';
  selectedCountryId: string = 'null';
  dataSource: MatTableDataSource<AreasListDto>;
  sortKey: String;
  sortDir: String;
  code: number;
  name: string;
  areasPage: PagesEnum = PagesEnum.Areas;
  mappingCode:string;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
   areaSatuskeys = Object.keys;
   displayedColumns: string[] = [
    'status',
    'areaId',
    'name',
    'governorate',
    'mappingCode',
    'edit',
  ];
  constructor(
    private service: ClientService,
    public router: Router,
    private dialog: MatDialog,
    public notify:NotifyService
  ) { 
    super(PagesEnum.Areas, ActionsEnum.View, router, notify);
  }
  getCountries() {
    return this.service.getCountries().subscribe((items) => {
      this.countries = items.response.countries;
    });
  }
  
  getGovernts() {
    return this.service.getGovernats().subscribe((items) => {
      this.governats = items.response.governats;
    });
  }
  ngOnInit(): void {
    this.criteria = new AreaSearchCriteria();
    this.loading = true;
    this.getCountries();
    this.getGovernts();
    this.search();
  }
  search(page?: PageEvent) {
    this.showForm=false;
    this.criteria.currentPageIndex = (page && page.pageIndex + 1) || 1;
    this.criteria.pageSize = (page && page.pageSize) || 5;
    if (this.code != undefined) {
      this.criteria.code = this.code;
    }
    if (this.code != undefined) {
      this.criteria.code = this.code;
    }
   

    if (this.name != undefined) {
      this.criteria.name = this.name;
    }
    if (this.mappingCode != undefined) {
      this.criteria.mappingCode = this.mappingCode;
    }
    
    if (this.areaStatusType != undefined) {
      this.criteria.isActive = this.areaStatusType;
    }
   
    if (this.selectedCountryId != undefined &&this.selectedCountryId!="null") {
      this.criteria.countryId = this.selectedCountryId;
    } else {
      this.criteria.countryId = '';
    }
   
    if (this.selectedGovernateId != undefined&&this.selectedGovernateId!="null") {
      this.criteria.governateId = this.selectedGovernateId;
    } else {
      this.criteria.governateId = '';
    }
   

    this.loading = true;
    this.data = undefined;
    return this.service.searchAreas(this.criteria).subscribe((items) => {
      console.log(items);
      if (items.response.totalCount > 0) {
        this.loading = false;
        this.data = items;
       
        this.dataSource = new MatTableDataSource<any>(
          this.data.response.geoZones
        );
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
        this.isShowEmpty = false;
      } else {
        this.loading = false;
        this.isShowEmpty = true;
      }
    });
  }

  gotoCreate() {
    this.router.navigate(['client/area-create']);
  }
  clear(areaForm: NgForm) {
   
    this.criteria=new AreaSearchCriteria();
     this.code = null;
     this.name = '';
     this.mappingCode = null;
     this.areaStatusType = null;
    this.areaStatusType = null;
     this.selectedCountryId = '';
     this.selectedGovernateId = '';
     areaForm.reset(this.search());
  }
  private navigate(areaId) {
    this.router.navigate(['client/area-edit', areaId]); 
  }

  openDeleteDialog(id: string): void {
    const dialogRef = this.dialog.open(dialogConfirmComponent, {
      data: 'Are You Sure Delete Area',
      disableClose: true,
      hasBackdrop:true,
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.service.deleteArea(id).subscribe((res) => {
          if (res.response) {
            this.notify.delete();
            this.search();
          }
        }, error => {
          this.notify.error(error.message,'FAILED OPERATION');
        });
      }
    });
  }

}
