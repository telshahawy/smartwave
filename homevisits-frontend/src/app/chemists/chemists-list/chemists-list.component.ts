import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { strict } from 'assert';
import { ClientService } from 'src/app/core/data-services/client.service';
import { NotifyService } from 'src/app/core/data-services/notify.service';
import {
  ChemistsSearchCriteria,
  ChemistsListDto,
  IpagedList,
  ChemistListResponse,
  CountryList,
  GovernateList,
  AreaList,
  CountryLookup,
  GovernateLookup,
  AreaLookup,
  chemistStatus,
  Gender,
  ExpertChemist,
  SearchParentCriteria,
  areaStatus,
} from 'src/app/core/models/models';
import { BaseComponent } from 'src/app/core/permissions/base.component';
import { ActionsEnum } from 'src/app/core/permissions/models/actions';
import { PagesEnum } from 'src/app/core/permissions/models/pages';
import { dialogConfirmComponent } from 'src/app/shared/component/dialog-confirm/dialog-confirm.component';
import { DialogComponent } from 'src/app/shared/component/dialog/dialog.component';

@Component({
  selector: 'app-chemists-list',
  templateUrl: './chemists-list.component.html',
  styleUrls: ['./chemists-list.component.css'],
})
export class ChemistsListComponent extends BaseComponent implements OnInit {
  data: IpagedList<ChemistListResponse>;
  criteria: ChemistsSearchCriteria;
  showForm = false;
  chemistSchedulePage : PagesEnum = PagesEnum.ChemistSchedule;
  selectedActionId = [];
  actions = [
    {
      id: 1,
      name: 'Edit',
    },
    {
      id: 2,
      name: 'Schedule',
    },
    // {
    //   id: 3,
    //   name: 'Send Credentials',
    // },
    // {
    //   id: 4,
    //   name: 'Chemist Permissions',
    // },
    {
      id: 5,
      name: 'Delete',
    },
  ];
  displayedColumns: string[] = [
    'chemistId',
    'name',
    'gender',
    'age',
    'mobile NO',
    'govenateName',
    'area',
    'join date',
    'edit',
  ];
  dataSource: MatTableDataSource<ChemistsListDto>;
  sortKey: String;
  sortDir: String;
  loading;
  isShowEmpty: boolean;
  joinDateFrom: Date;
  joinDateTo: Date;
  selectedGovernateId: string='null';
  selectedCountryId: string = 'null';
  selectedAreaId: string='null';
  selectedGender: number=null;
  code: string;
  chemistName: string;
  phoneNo: string;
  AreaAssignStatus: number;
  chemistStatusType: boolean=null;
  areaStatusType: boolean=null;
  isExpertChemist: boolean=null;
  geoZoneId: string;
  countries: CountryLookup[];
  governats: GovernateLookup[];
  pageSize=5;

  genderkeys = Object.keys;
  expertkeys = Object.keys;
  areaStatusKeys=Object.keys;
  chemistSatuskeys = Object.keys;
  areas: AreaLookup[];
  chemistStatus = chemistStatus;
  areaStatus=areaStatus;
  expertChemist = ExpertChemist;
  keys: any[];
  chemistPage: PagesEnum = PagesEnum.Chemists;
  viewChemistPage: PagesEnum = PagesEnum.ViewChemists;
  viewChemistPermitPage: PagesEnum = PagesEnum.ChemistPermit;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(
    private service: ClientService,
    public router: Router,
    private dialog: MatDialog,
    public notify : NotifyService
  ) {
    super(PagesEnum.ViewChemists, ActionsEnum.View, router, notify);

  }
  ngOnInit() {
    this.criteria = new ChemistsSearchCriteria();
    this.loading = true;
    this.getAreas();
    this.getCountries();
    this.getGovernts();
  //  this.enumFilter();
    this.search();
  }
  getCountries() {
    return this.service.getCountries().subscribe((items) => {
      this.countries = items.response.countries;
    });
  }
  changeAction(action, currentChemist , userName) {
    console.log('selected action : ', action);
    console.log('current cemist : ', currentChemist);
    if (action == 1) {
      this.router.navigate(['/chemists/chemists-edit/', currentChemist]);
      this.selectedActionId[0]=''
    }
    if (action == 2) {
      let str =userName.replace(/\s/g, '');
      console.log(str);
      
      this.router.navigate(['/chemists/chemist-schedules/', currentChemist,userName]);
      this.selectedActionId[0]=''
      
    }
    if(action == 3){
      this.router.navigate([`/chemists/chemists-permits/${currentChemist}`]);
    }
    if (action == 5) {
      this.openDeleteDialog(currentChemist);
      this.selectedActionId[0]=''

    }
  }
  getAreas() {
    return this.service.getAreas().subscribe((items) => {
      this.areas = items.response.geoZones;
    });
  }
  getGovernts() {
    return this.service.getGovernats().subscribe((items) => {
      this.governats = items.response.governats;
    });
  }
  changePageList(e){
    this.search(e)
  }
  search(page?:any) {
    this.showForm=false;

    this.pageSize =page || 5
    this.criteria.currentPageIndex = (page && page.pageIndex + 1) || 1;
    this.criteria.pageSize =this.pageSize;


    if (this.code != undefined) {
      this.criteria.code = this.code;
    }
    if (this.joinDateFrom != undefined) {
      this.criteria.joinDateFrom = this.joinDateFrom;
    }
    if (this.joinDateTo != undefined) {
      this.criteria.joinDateto = this.joinDateTo;
    }

    if (this.chemistName != undefined) {
      this.criteria.chemistName = this.chemistName;
    }
    if (this.phoneNo != undefined) {
      this.criteria.phoneNo = this.phoneNo;
    }

    if (this.isExpertChemist != undefined) {
      this.criteria.expertChemist = this.isExpertChemist;
    }
    if (this.chemistStatusType != undefined) {
      this.criteria.chemistStatus = this.chemistStatusType;
    }
    if (this.selectedCountryId != undefined &&this.selectedCountryId!="null") {
      this.criteria.countryId = this.selectedCountryId;
    } else {
      this.criteria.countryId = '';
    }
    if (this.selectedAreaId != undefined&&this.selectedAreaId!="null") {
      this.criteria.geoZoneId = this.selectedAreaId;
    } else {
      this.criteria.geoZoneId = '';
    }
    if (this.selectedGovernateId != undefined&&this.selectedGovernateId!="null") {
      this.criteria.governateId = this.selectedGovernateId;
    } else {
      this.criteria.governateId = '';
    }
    if (this.selectedGender != undefined) {
      this.criteria.gender = this.selectedGender;
    } else {
      this.criteria.gender = null;
    }
    if (this.isExpertChemist != undefined) {
      this.criteria.expertChemist = this.isExpertChemist;
    } else {
      this.criteria.expertChemist = null;
    }
    if (this.areaStatusType != undefined) {
      this.criteria.areaAssignStatus = this.areaStatusType;
    } else {
      this.criteria.areaAssignStatus = null;
    }
    if (this.chemistStatusType != undefined) {
      this.criteria.chemistStatus = this.chemistStatusType;
    } else {
      this.criteria.chemistStatus = null;
    }

    this.loading = true;
    this.data = undefined;
    return this.service.searchChemists(this.criteria).subscribe((items) => {
      console.log(items);
      if (items.response.totalCount > 0) {
        this.loading = false;
        this.data = items;
        this.selectedActionId.length = this.data.response.chemists.length;
        for (let i = 0; i < this.selectedActionId.length; i++) {
          this.selectedActionId[i] = '';
          this.data.response.chemists[i]['index'] = i;
        }
        this.dataSource = new MatTableDataSource<any>(
          this.data.response.chemists
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

  gotoEdit() {
    this.router.navigate(['chemists/chemists-create']);
  }
  clear(chemistForm: NgForm) {
    //this.search();cre
    this.criteria=new ChemistsSearchCriteria();
     this.phoneNo = '';
     this.chemistName = '';
     this.joinDateFrom = null;
     this.joinDateTo = null;
     this.isExpertChemist = null;
      this.chemistStatusType = null;
      this.areaStatusType = null;
     this.selectedGender = null;
     this.selectedCountryId = '';
     this.selectedGovernateId = '';
     this.selectedAreaId = '';
     this.code = '';
    chemistForm.reset(this.search());
  }
  private navigate(product) {
    this.router.navigate(['chemists/chemists-edit', product]); //we can send product object as route param
  }

  openDeleteDialog(id: string): void {
    const dialogRef = this.dialog.open(dialogConfirmComponent, {
      data: 'Are You Sure Delete Chemist',
      disableClose: true,
      hasBackdrop:true,
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.service.deleteChemist(id).subscribe((res) => {
          if (res != undefined) {
            this.search();
          }
        });
      }
    });
  }
  // enumFilter() {
  //   this.keys = Object.keys(this.genderType).filter((k) => !isNaN(Number(k))).map(k => parseInt(k));
  // }
 
}
