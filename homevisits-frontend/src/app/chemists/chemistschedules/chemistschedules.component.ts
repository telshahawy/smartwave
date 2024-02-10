import { ViewChild } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router, ActivatedRoute } from '@angular/router';
import { MaskedTextBoxComponent } from '@syncfusion/ej2-angular-inputs';
import { TreeViewComponent } from '@syncfusion/ej2-angular-navigations';
import { ClientService } from 'src/app/core/data-services/client.service';
import { NotifyService } from 'src/app/core/data-services/notify.service';
import { IpagedList } from 'src/app/core/models/models';
import { BaseComponent } from 'src/app/core/permissions/base.component';
import { ActionsEnum } from 'src/app/core/permissions/models/actions';
import { PagesEnum } from 'src/app/core/permissions/models/pages';
import { dialogConfirmComponent } from 'src/app/shared/component/dialog-confirm/dialog-confirm.component';
import { scheduleSearchCriteria } from '../../core/models/models';

@Component({
  selector: 'app-chemistschedules',
  templateUrl: './chemistschedules.component.html',
  styleUrls: ['./chemistschedules.component.css'],
})
export class ChemistschedulesComponent extends BaseComponent implements OnInit {
  data: IpagedList<any>;
  showForm = false;
  criteria: scheduleSearchCriteria;
  displayedColumns: string[] = ['NAME', 'FROM', 'TO', 'ACTIONS'];
  dataSource: MatTableDataSource<any>;
  sortKey: String;
  sortDir: String;
  selectedActionId = [];
  userName;
  chemistId;
  actions = [
    {
      id: 1,
      name: 'Edit',
    },
    {
      id: 2,
      name: 'Duplicate',
    },
    {
      id: 5,
      name: 'Delete',
    },
  ];
  loading;
  isShowEmpty: boolean;
  ChemistId;
  ChemistscheduletId;
  selectedAreaId;
  StartDate;
  EndDate;
  StartDate2;
  EndDate2;
  areas = [];
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(
    private service: ClientService,
    public router: Router,
    public notify: NotifyService,

    private route: ActivatedRoute,
    private dialog: MatDialog
  ) {
    super(PagesEnum.ChemistSchedule, ActionsEnum.View, router, notify);
    this.route.params.subscribe((paramsId) => {
      this.ChemistId = paramsId.chemistId;
      console.log(paramsId);

      this.service.updatechemistId(this.ChemistId);
      this.service
        .GETGeoChemistZonesKeyValue(this.ChemistId)
        .subscribe((res) => {
          this.areas = res.response;
        });
    });
  }

  ngOnInit(): void {
    let ll = this.route.snapshot.params.userName;
    this.userName = ll.replace('/s/g', '');
    console.log(this.userName, 'kkkk');

    this.chemistId = this.route.snapshot.params.chemistId;

    this.criteria = new scheduleSearchCriteria();
    this.loading = true;
    this.search();
  }
  changeArea() {
    console.log();
  }
  duplicate() {
    const x = {
      chemistScheduleId: this.ChemistscheduletId,
      chemistId: this.ChemistId,
      startDate: this.StartDate2,
      endDate: this.EndDate2,
    };
    this.service.chemistScheduleService.duplicate(x).subscribe(
      (res) => {
        this.search();
        this.StartDate2 = '';
        this.EndDate2 = '';
        let element: HTMLElement = document.getElementById(
          'closeDuplicate'
        ) as HTMLElement;
        element.click();
      },
      (err) => {
        this.notify.error(err.message, 'FAILED OPERATION');
        this.StartDate2 = '';
        this.EndDate2 = '';
        let element: HTMLElement = document.getElementById(
          'closeDuplicate'
        ) as HTMLElement;
        element.click();
      }
    );
  }
  changeAction(action, currentChemist) {
    if (action == 1) {
      this.router.navigate([
        '/chemists/chemist-schedules/' +
          this.chemistId +
          '/edit/' +
          currentChemist +
          '/' +
          this.userName,
      ]);
    }
    if (action == 2) {
      document.getElementById('LaunchModal').click();
      this.ChemistscheduletId = currentChemist;
      console.log('its duplicate', this.ChemistscheduletId);
      // this.router.navigate(['/chemists/chemist-schedules/', currentChemist]);
    }
    if (action == 5) {
      this.openDeleteDialog(currentChemist);
    }
  }
  search(page?: PageEvent) {
    // this.criteria.currentPageIndex = (page && page.pageIndex + 1) || 1;
    // this.criteria.pageSize = (page && page.pageSize) || 5;
    if (this.ChemistId != undefined) {
      this.criteria.ChemistId = this.ChemistId;
    }
    if (this.selectedAreaId != undefined) {
      this.criteria.AssignedGeoZoneId = this.selectedAreaId;
    }
    if (this.StartDate != undefined) {
      this.criteria.StartDate =
        this.StartDate.getFullYear() +
        '-' +
        (String(parseInt(this.StartDate.getMonth() + 1)).length == 1
          ? '0' + String(parseInt(this.StartDate.getMonth() + 1))
          : String(parseInt(this.StartDate.getMonth() + 1))) +
        '-' +
        (String(this.StartDate.getDate()).length == 1
          ? '0' + this.StartDate.getDate()
          : this.StartDate.getDate());
    }
    if (this.EndDate != undefined) {
      this.criteria.EndDate =
        this.EndDate.getFullYear() +
        '/' +
        (String(parseInt(this.EndDate.getMonth() + 1)).length == 1
          ? '0' + String(parseInt(this.EndDate.getMonth() + 1))
          : String(parseInt(this.EndDate.getMonth() + 1))) +
        '/' +
        (String(this.EndDate.getDate()).length == 1
          ? '0' + this.EndDate.getDate()
          : this.EndDate.getDate());
    }
    this.loading = true;
    this.data = undefined;
    return this.service.chemistScheduleService
      .search(this.criteria)
      .subscribe((items) => {
        console.log(items);
        // items = {
        //   response: {
        //     schedules: [
        //       {
        //         chemistScheduleId: '6dff9c65-7257-4332-9077-43ec30ff91e1',
        //         geoZoneName: 'Mohandseen',
        //         startDate: '2020-11-29T00:00:00',
        //         endDate: '2020-12-30T00:00:00',
        //       },
        //     ],
        //   },
        // };
        if (items.response) {
          this.loading = false;
          this.data = items;
          this.selectedActionId.length = this.data.response.schedules.length;
          for (let i = 0; i < this.selectedActionId.length; i++) {
            this.selectedActionId[i] = '';
            this.data.response.schedules[i]['index'] = i;
          }
          this.dataSource = new MatTableDataSource<any>(
            this.data.response.schedules
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

  // gotoAdd() {
  //   this.router.navigate(['create']);
  // }

  private navigate(role) {
    this.router.navigate(['client/role-edit', role]); //we can send product object as route param
  }
  openDeleteDialog(id: string): void {
    const dialogRef = this.dialog.open(dialogConfirmComponent, {
      data: 'Are You Sure Delete Schedule',
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.service.chemistScheduleService.delete(id).subscribe((res) => {
          if (res != undefined) {
            this.search();
          }
        });
      }
    });
  }
}
