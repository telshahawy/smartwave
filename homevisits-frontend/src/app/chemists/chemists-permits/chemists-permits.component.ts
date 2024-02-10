import { stringify } from '@angular/compiler/src/util';
import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { ClientService } from 'src/app/core/data-services/client.service';
import { NotifyService } from 'src/app/core/data-services/notify.service';
import {
  ChemistListResponse,
  ChemistPermitDto,
  ChemistPermitListResponse,
  ChemistPermitSearchCriteria,
  ChemistsSearchCriteria,
  CreateChemistPermitModel,
  IpagedList,
} from 'src/app/core/models/models';
import { BaseComponent } from 'src/app/core/permissions/base.component';
import { ActionsEnum } from 'src/app/core/permissions/models/actions';
import { PagesEnum } from 'src/app/core/permissions/models/pages';
import { dialogConfirmComponent } from 'src/app/shared/component/dialog-confirm/dialog-confirm.component';

@Component({
  selector: 'app-chemists-permits',
  templateUrl: './chemists-permits.component.html',
  styleUrls: ['./chemists-permits.component.css'],
})
export class ChemistsPermitsComponent extends BaseComponent implements OnInit {
  constructor(
    public router: Router,
    public notify: NotifyService,
    private client: ClientService,
    private route: ActivatedRoute,
    private dialog: MatDialog
  ) {
    super(PagesEnum.ChemistPermit, ActionsEnum.View, router, notify);
  }
  viewChemistPage: PagesEnum = PagesEnum.ChemistPermit;
  data: IpagedList<ChemistPermitListResponse>;
  criteria: ChemistPermitSearchCriteria;
  createPermits : CreateChemistPermitModel;
  showForm = false;
  chemistId: string;
  chemistSchedulePage: PagesEnum = PagesEnum.ChemistSchedule;
  selectedActionId = [];
  displayedColumns: string[] = ['Date', 'From', 'To', 'edit'];
  dataSource: MatTableDataSource<ChemistPermitDto>;
  loading;
  createPermitDate: Date;

  isShowEmpty: boolean;
  permitDate: Date;
  startTime : object;
  endTime : object;
  submitted=false
  @ViewChild('chemistPermitForm') chemistPermitForm: any;
  ngOnInit() {
    this.chemistId = this.route.snapshot.paramMap.get('chemistId');
    this.criteria = new ChemistPermitSearchCriteria();
    this.createPermits= new CreateChemistPermitModel();
    this.criteria.chemistId = this.chemistId;
    this.search();
  }

  search() {
    this.showForm = false;

    if (this.permitDate != undefined) {
      this.criteria.permitDate = this.permitDate;
    }

    this.loading = true;
    this.data = undefined;
    return this.client
      .searchChemistPermits(this.criteria)
      .subscribe((items) => {
        console.log(items);
        if (items.response.permits && items.response.permits.length > 0) {
          this.loading = false;
          this.data = items;
          this.dataSource = new MatTableDataSource<any>(
            this.data.response.permits
          );
          this.isShowEmpty = false;
        } else {
          this.loading = false;
          this.isShowEmpty = true;
        }
      });
  }
 

  
  gotoCreate() {
    // Create
   this.router.navigate([`/chemists/create-chemists-permits/${this.chemistId}`]);
  }
  changeAction($event, chemistPermitId: string) {
    if ($event == 1) {
      // Edit
      this.router.navigate([`/chemists/update-chemists-permits/`,chemistPermitId]);
    }
    if ($event == 2) {
      // Delete
      this.DeletePermit(chemistPermitId);
    }
  }
  timeFunction(timeObj) {
    return timeObj.slice(0,8);
  };
  DeletePermit(chemistPermitId: string) {
    const dialogRef = this.dialog.open(dialogConfirmComponent, {
      data: 'Are You Sure you want to Delete Permit?',
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.client.deleteChemistPermit(chemistPermitId).subscribe((res) => {
          if (res != undefined) {
            this.search();
          }
        });
      }
    });
  }
}
