import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ClientService } from 'src/app/core/data-services/client.service';
import { NotifyService } from 'src/app/core/data-services/notify.service';

import {
  AreaLookup,
  AssignedStatus,
  Chemists,
  ExpertChemist,
  Gender,
  GovernateLookup,
  IpagedList,
  SortBy,
  VisitListResponse,
  VisitsListDto,
  VisitsSearchCriteria,
  VisitStatus,
} from 'src/app/core/models/models';
import { BaseComponent } from 'src/app/core/permissions/base.component';
import { ActionsEnum } from 'src/app/core/permissions/models/actions';
import { PagesEnum } from 'src/app/core/permissions/models/pages';
import { dialogConfirmComponent } from 'src/app/shared/component/dialog-confirm/dialog-confirm.component';
import { CancelVisitComponent } from '../cancel-visit/cancel-visit.component';
import { ReassignChemistVisitComponent } from '../reassign-chemist-visit/reassign-chemist-visit.component';
import { SecondVisitCreateComponent } from '../second-visit-create/second-visit-create.component';
import { VisitDetailsComponent } from '../visit-details/visit-details.component';

@Component({
  selector: 'app-home-visits-list',
  templateUrl: './home-visits-list.component.html',
  styleUrls: ['./home-visits-list.component.css'],
})
export class HomeVisitsListComponent extends BaseComponent implements OnInit {
  showForm = false;
  data: IpagedList<VisitListResponse>;
  criteria: VisitsSearchCriteria;
  viewPage : PagesEnum = PagesEnum.ViewVisit;
  displayedColumns: string[] = [
    'chemistId',
    'visitdate',
    'timeslot',
    'patient name',
    'gender',
    'age',
    'mobile NO',
    'area',
    'chemist',
    'status',
    'edit',
    'log',
  ];

  dataSource: MatTableDataSource<VisitsListDto>;
  sortKey: String;
  sortDir: String;
  loading;
  isShowEmpty: boolean;
  visitDateFrom: Date;
  visitDateTo: Date;
  visitNoFrom: number;
  visitNoTo: number;
  creationDateFrom: Date;
  creationDateTo: Date;
  selectedGovernateId: string = 'null';
  patientNo: string;
  selectedAreaId: string = 'null';
  selectedGender: number = null;
  patientName: string;
  chemistName: string;
  phoneNo: string;
  AreaAssignStatus: number;
  chemistStatusType: boolean = null;
  isExpertChemist: boolean = null;
  geoZoneId: string;
  governats: GovernateLookup[];
  isNeedExpert: boolean = null;
  visitStatusTypeId: number = null;
  sortBy: number = null;
  assignStatus: number = null;
  assignedTo: string = null;
  genderType = Gender;
  areas: AreaLookup[];
  keys: any[];
  chemists: Chemists[];
  isCancelledPermission: boolean = false;
  isAcceptedPermission: boolean = false;
  pageSize;
  needExpert = ExpertChemist;
  assignStatusType = AssignedStatus;
  visitStatusType = VisitStatus;
  sortByType = SortBy;
  expertkeys = Object.keys;
  assignKeys: any[];
  sortKeys: any[];
  visitStatusKeys: any[];
  action;
  setInterval = setInterval;
  interval;
  selectedPeriod: number = 180000;
  isRefreshTime: boolean;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  selectedActionId = [];
  actions = [
    {
      id: 1,
      name: 'Request second visit',
    },
    {
      id: 2,
      name: 'Re-assign visit',
    },
    {
      id: 3,
      name: 'Cancel visit',
    },
    {
      id: 4,
      name: 'Approve',
    },
  ];
  constructor(
    private service: ClientService,
    public router: Router,
    public toastr: ToastrService,
    private dialog: MatDialog,
    private route: ActivatedRoute,
    public notify: NotifyService
  ) {
    super(PagesEnum.ViewVisit, ActionsEnum.View, router, notify);
  }
  ngOnInit() {
    this.criteria = new VisitsSearchCriteria();
    this.loading = true;
    this.getAreas();
    this.getGovernts();
    this.getChemistList();
    this.getCancelledPermission();
    this.getAcceptedPermission();
    // this.enumFilter();
    this.route.params.subscribe((res) => {
      console.log(res);

      this.getHomePageFilters(res.id);
    });
    // this.search();
  }
  //   onOptionsSelected(value) {
  //    if(value==1)
  //    {
  //     this.openSecondVisitDialog();
  //    }
  //    if(value==2)
  //    {
  //     this.openReassignChemistDialog();
  //    }

  //    if(value==3)
  //    {
  // this.openCancelVisitDialog();
  //    }
  //   }

  getCancelledPermission() {
    const x = {
      isCancelledBy: 'CallCenter',
    };
    this.service.systemParameters
      .GetVisitAcceptCancelPermission(x)
      .subscribe((res) => {
        this.isCancelledPermission = res.response.value;
      });
  }

  getAcceptedPermission() {
    const x = {
      isAcceptedBy: 'CallCenter',
    };
    this.service.systemParameters
      .GetVisitAcceptCancelPermission(x)
      .subscribe((res) => {
        this.isAcceptedPermission = res.response.value;
      });
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
  changePageList(e) {
    this.search(e);
  }
  search(page?: PageEvent) {
    
    this.showForm = false;
    this.pageSize = page || 5;

    this.criteria.currentPageIndex = (page && page.pageIndex + 1) || 1;
    this.criteria.pageSize = this.pageSize;

    if (this.visitDateFrom != undefined) {
      this.criteria.visitDateFrom = this.visitDateFrom;
    }
    if (this.visitDateTo != undefined) {
      this.criteria.visitDateTo = this.visitDateTo;
    }

    if (this.creationDateFrom != undefined) {
      this.criteria.creationDateFrom = this.creationDateFrom;
    }
    if (this.creationDateTo != undefined) {
      this.criteria.creationDateTo = this.creationDateTo;
    }

    if (this.visitNoFrom != undefined) {
      this.criteria.visitNoFrom = this.visitNoFrom;
    }
    if (this.visitNoTo != undefined) {
      this.criteria.visitNoTo = this.visitNoTo;
    }
    if (this.patientName != undefined) {
      this.criteria.patientName = this.patientName;
    }
    if (this.patientNo != undefined) {
      this.criteria.patientNo = this.patientNo;
    }
    if (this.phoneNo != undefined) {
      this.criteria.patientMobileNo = this.phoneNo;
    }

    if (this.selectedAreaId != undefined && this.selectedAreaId != 'null') {
      this.criteria.geoZoneId = this.selectedAreaId;
    } else {
      this.criteria.geoZoneId = '';
    }
    if (
      this.selectedGovernateId != undefined &&
      this.selectedGovernateId != 'null'
    ) {
      this.criteria.governateId = this.selectedGovernateId;
    } else {
      this.criteria.governateId = '';
    }
    if (this.assignedTo != undefined && this.assignedTo != null) {
      this.criteria.assignedTo = this.assignedTo;
    } else {
      this.criteria.assignedTo = '';
    }
    if (this.selectedGender != undefined) {
      this.criteria.gender = this.selectedGender;
    } else {
      this.criteria.gender = null;
    }
    if (this.assignStatus != undefined) {
      this.criteria.assignStatus = this.assignStatus;
    } else {
      this.criteria.assignStatus = null;
    }
    if (this.sortBy != undefined) {
      this.criteria.sortBy = this.sortBy;
    } else {
      this.criteria.sortBy = null;
    }
    if (this.visitStatusTypeId != undefined) {
      this.criteria.visitStatusTypeId = this.visitStatusTypeId;
    } else {
      this.criteria.visitStatusTypeId = null;
    }
    if (this.isNeedExpert != undefined) {
      this.criteria.needExpert = this.isNeedExpert;
    } else {
      this.criteria.needExpert = null;
    }

    this.loading = true;
    this.data = undefined;
    return this.service.searchVisits(this.criteria).subscribe((items) => {
      console.log(items);

      this.loading = false;
      this.data = items;
      if (items.response.totalCount > 0) {
        this.selectedActionId.length = this.data.response.visits.length;
        for (let i = 0; i < this.selectedActionId.length; i++) {
          this.selectedActionId[i] = '';
          this.data.response.visits[i]['index'] = i;
        }
        // this.data.response.pageSize = 10;
        this.dataSource = new MatTableDataSource<any>(
          this.data.response.visits
        );
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
        this.isShowEmpty = false;
      } else {
        this.data.response.visits = [];
        this.loading = false;
        this.isShowEmpty = true;
      }
    });
  }

  gotoEdit() {
    this.router.navigate(['visits/home-visits-create']);
  }
  clear(chemistForm: NgForm) {
    this.criteria = new VisitsSearchCriteria();
    this.phoneNo = '';
    this.creationDateTo = null;
    this.creationDateFrom = null;
    this.visitDateTo = null;
    this.visitDateFrom = null;
    this.visitNoTo = null;
    this.visitNoFrom = null;
    this.selectedGender = null;
    this.patientNo = '';
    this.selectedGovernateId = '';
    this.selectedAreaId = '';
    this.patientName = '';
    this.assignedTo = '';
    this.visitStatusTypeId = null;
    this.sortBy = null;
    this.isNeedExpert = null;
    this.assignStatus = null;
    this.isRefreshTime = false;

    this.pauseTimeLine();
    chemistForm.reset(this.search());
    this.selectedPeriod = 180000;
  }
  private navigate(product) {
    this.router.navigate(['chemists/chemists-edit', product]); //we can send product object as route param
  }

  openDeleteDialog(id: string): void {
    const dialogRef = this.dialog.open(dialogConfirmComponent, {
      width: '350px',
      data: 'Are You Sure Delete Visit',
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
  openSecondVisitDialog(id: string, geZoneId: string, patientNo) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.autoFocus = true;
    dialogConfig.width = '100%';
    dialogConfig.height = '90%';
    dialogConfig.data = {
      visitId: id,
      geZoneId: geZoneId,
      patientNo: patientNo,
    };
    this.dialog.open(SecondVisitCreateComponent, dialogConfig);
    let count = 0;
    this.dialog.afterAllClosed.subscribe((res) => {
      if (count == 0) {
        this.search();
        count++;
      }
    });
  }
  openCancelVisitDialog(id: string, status: string) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.autoFocus = true;
    dialogConfig.width = '100%';
    dialogConfig.data = { visitId: id, status: status };
    let count = 0;
    this.dialog.open(CancelVisitComponent, dialogConfig);

    this.dialog.afterAllClosed.subscribe((res) => {
      if (count == 0) {
        this.search();
        count++;
      }
    });
  }
  openReassignChemistDialog(id: string, geZoneId: string) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.autoFocus = true;
    dialogConfig.width = '100%';
    dialogConfig.height = '90%';
    dialogConfig.data = { visitId: id, geZoneId: geZoneId };

    this.dialog.open(ReassignChemistVisitComponent, dialogConfig);
    let count = 0;
    this.dialog.afterAllClosed.subscribe((res) => {
      if (count == 0) {
        this.search();
        count++;
      }
    });
  }
  changeAction(action, visitId, geZoneId, status, patientNo) {
    if (action == 1) {
      this.openSecondVisitDialog(visitId, geZoneId, patientNo);
    }
    if (action == 2) {
      this.openReassignChemistDialog(visitId, geZoneId);
    }
    if (action == 3) {
      this.openCancelVisitDialog(visitId, status);
    }
    if (action == 4) {
      this.approveVisit(visitId);
    }
  }
  approveVisit(visitId) {
    const dialogRef = this.dialog.open(dialogConfirmComponent, {
      width: '350px',
      data: 'Are you sure you want to confirm this visit?',
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        const x = {
          visitActionTypeId: 2,
          visitStatusTypeId: 2,

          visitId: visitId,
        };
        this.service.sendApproveVisit(x).subscribe(
          (res) => {
            this.search();
            this.responseSuccess();
          },
          (err) => {
            this.responseFailed(err.message);
          }
        );
      }
    });
  }
  getChemistList() {
    let geoZoneId = '';
    return this.service.getChemistListItem(geoZoneId).subscribe((items) => {
      this.chemists = items.response;
    });
  }
  openVisitDetailslog(id: string) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.autoFocus = true;
    dialogConfig.width = '100%';
    dialogConfig.data = { visitId: id };
    this.dialog.open(VisitDetailsComponent, dialogConfig);

    this.dialog.afterAllClosed.subscribe((res) => {});
  }
  enableRefreshTime(values: any) {
    if (values.currentTarget.checked) {
      this.setIntrvl();
      this.isRefreshTime = true;
    } else {
      this.pauseTimeLine();
      this.isRefreshTime = false;
    }
  }

  // enumFilter() {
  //   this.keys = Object.keys(this.genderType).filter((k) => !isNaN(Number(k))).map(k => parseInt(k));
  // }
  setIntrvl() {
    this.interval = setInterval(() => this.search(), this.selectedPeriod);
  }
  pauseTimeLine() {
    clearInterval(this.interval);
  }
  changeIntervalTime() {
    this.pauseTimeLine();
    this.setIntrvl();
  }

  getHomePageFilters(route, page?: PageEvent) {
    if (route == 'total') {
      this.service.homeService.getTotalVisit().subscribe((res) => {
        console.log(res);
        this.getResponseAfterVisit(res);
      });
    } else if (route == 'delayed') {
      this.service.homeService.getDelyedVisit().subscribe((res) => {
        console.log(res);
        this.getResponseAfterVisit(res);
      });
    } else if (route == 'pending') {
      this.service.homeService.getPendingVisit().subscribe((res) => {
        console.log(res);
        this.getResponseAfterVisit(res);
      });
    } else if (route == 'reassigned') {
      this.service.homeService.getRessaginedVisit().subscribe((res) => {
        console.log(res);
        this.getResponseAfterVisit(res);
      });
    } else {
      this.search();
    }
  }
  getResponseAfterVisit(items) {
    this.loading = false;
    this.data = items;
    if (items.response.totalCount > 0) {
      this.selectedActionId.length = this.data.response.visits.length;
      for (let i = 0; i < this.selectedActionId.length; i++) {
        this.selectedActionId[i] = '';
        this.data.response.visits[i]['index'] = i;
      }
      // this.data.response.pageSize = 10;
      this.dataSource = new MatTableDataSource<any>(this.data.response.visits);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
      this.isShowEmpty = false;
    } else {
      this.data.response.visits = [];
      this.loading = false;
      this.isShowEmpty = true;
    }
  }
  responseSuccess() {
    this.notify.success(
      'Visit has been Approved successfully',
      'SUCCESS OPERATION'
    );
  }

  responseFailed(err) {
    this.notify.error(err, 'FAILED OPERATION');
  }
}
