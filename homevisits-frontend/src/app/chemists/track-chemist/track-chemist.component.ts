import { DatePipe } from '@angular/common';
import {
  ChangeDetectorRef,
  Component,
  NgZone,
  OnDestroy,
  OnInit,
} from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { tap } from 'rxjs/operators';
import { ClientService } from 'src/app/core/data-services/client.service';
import {
  ChemistLastTrackingLogListResponse,
  ChemistListResponse,
  ChemistTrackingLog,
  GetChemistsLastTracking,
  IpagedList,
} from 'src/app/core/models/models';
import { ActionsEnum } from 'src/app/core/permissions/models/actions';
import { BaseComponent } from 'src/app/core/permissions/base.component';
import { ShowChemistCardComponent } from './show-chemist-card/show-chemist-card.component';
import { PagesEnum } from 'src/app/core/permissions/models/pages';
import { ToastrService } from 'ngx-toastr';
import { NotifyService } from 'src/app/core/data-services/notify.service';

@Component({
  selector: 'app-track-chemist',
  templateUrl: './track-chemist.component.html',
  styleUrls: ['./track-chemist.component.css'],
})
export class TrackChemistComponent extends BaseComponent implements OnInit, OnDestroy {
  zoom: number = 12;
  ChemistsLastTracking: GetChemistsLastTracking;
  result: IpagedList<ChemistLastTrackingLogListResponse>;
  trackPage = PagesEnum.TrackChemists;
  constructor(
    private service: ClientService,
    public dialog: MatDialog,
    public datepipe: DatePipe,
    public router: Router,
    public notify : NotifyService
  ) {
    super(PagesEnum.TrackChemists, ActionsEnum.View, router,notify);
    this.result = {
      response: {
        chemistLastTrackingLogs: [],
        currentPageIndex: 1,
        pageSize: 25,
        totalCount: 0,
      },
    };
    this.ChemistsLastTracking = {
      name: '',
      currentPageIndex: 1,
      pageSize: 200,
    };
  }
  ngOnDestroy(): void {
    this.dialog.closeAll();
  }

  ngOnInit(): void {
    this.search();
  }
  onSearchChange(searchValue: string): void {
    if (searchValue.length >= 2) {
      this.ChemistsLastTracking.name = searchValue;
      this.search();
    }
    if (searchValue.length == 0) {
      this.ChemistsLastTracking.name = '';
      this.search();
    }
  }
  getInitails(str: string) {
    var names = str.split(' '),
      initials = names[0].substring(0, 1).toUpperCase();

    if (names.length > 1) {
      initials += names[names.length - 1].substring(0, 1).toUpperCase();
    }
    return initials;
  }
  getVisitInfo(visitNo: string, visitDate: string, visitTime: any) {
    if (visitTime == null) return visitNo;
    else {
      var date = this.datepipe.transform(visitDate, 'dd/MM/yyyy');
      var time = this.timeFunction(visitTime.value);
      return '#' + visitNo + ' - ' + date + ' ' + time;
    }
  }
  timeFunction(timeObj) {
    var min = timeObj.minutes < 10 ? '0' + timeObj.minutes : timeObj.minutes;
    var hour = timeObj.hours < 10 ? '0' + timeObj.hours : timeObj.hours;
    return hour + ':' + min;
  }
  openDialog(log: ChemistTrackingLog) {
    this.dialog.closeAll();
    this.dialog.open(ShowChemistCardComponent, {
      data: log,
      closeOnNavigation: true,
    });
  }
  decrementPage() {
    this.ChemistsLastTracking.currentPageIndex--;
    this.search();
  }
  incrementPage() {
    this.ChemistsLastTracking.currentPageIndex++;
    this.search();
  }
  search() {
    this.service
      .getChemistsLastTracking(this.ChemistsLastTracking)
      .subscribe((result) => {
        this.result.response = result.response;
      });
  }
}
