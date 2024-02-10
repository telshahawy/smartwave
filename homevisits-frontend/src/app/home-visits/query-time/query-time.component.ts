import { formatDate } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { time } from 'console';
import { ClientService } from 'src/app/core/data-services/client.service';
import { NotifyService } from 'src/app/core/data-services/notify.service';
import {
  AreaLookup,
  Chemists,
  ChemistsSearchCriteria,
  CountryLookup,
  GovernateLookup,
  IpagedList,
  QueryTimeCriteria,
  QueryTimesListDTO,
  QueryTimesListResponse,
} from 'src/app/core/models/models';
import { BaseComponent } from 'src/app/core/permissions/base.component';
import { ActionsEnum } from 'src/app/core/permissions/models/actions';
import { PagesEnum } from 'src/app/core/permissions/models/pages';
import { VisitLostTimeComponent } from '../visit-lost-time/visit-lost-time.component';

@Component({
  selector: 'app-query-time',
  templateUrl: './query-time.component.html',
  styleUrls: ['./query-time.component.css'],
})
export class QueryTimeComponent extends BaseComponent implements OnInit {
  submitted = false;
  queryForm: FormGroup;
  countries: CountryLookup[];
  governats: GovernateLookup[];
  areas: AreaLookup[];
  loading;
  isShowEmpty: boolean;
  chemists: Chemists[];
  times = [];
  showForm = true;
  allowedDates;
  startDate;
  endDate;
  selectedTimeZoneFrame;
  selectedChemistId;
  selectedVisitDate;
  selectedGeoZoneId;
  constructor(
    private service: ClientService,
    public router: Router,
    private dialog: MatDialog,
    private fb: FormBuilder,
    private route: ActivatedRoute,
    public notify: NotifyService
  ) {
    super(PagesEnum.QueryTime, ActionsEnum.View, router, notify);
  }

  ngOnInit(): void {
    this.initForm();
    this.getCountries();
    this.getAllowedDate();
  }
  getChemistList(GeoZoneId) {
    return this.service.getChemistListItem(GeoZoneId).subscribe((items) => {
      this.chemists = items.response;
    });
  }

  openVisitLostTimeDialog() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.autoFocus = true;
    dialogConfig.width = '100%';

    this.dialog.open(VisitLostTimeComponent, dialogConfig);

    this.dialog.afterAllClosed.subscribe((res) => {});
  }
  getAllowedDate() {
    this.service.systemParameters.dateVisit().subscribe((res) => {
      this.allowedDates = res.response;

      this.startDate = this.allowedDates.startDate;
      this.startDate = formatDate(this.startDate, 'yyyy-MM-dd', 'en_US');

      this.endDate = this.allowedDates.endDate;
      this.endDate = formatDate(this.endDate, 'yyyy-MM-dd', 'en_US');

      console.log(this.startDate);
      console.log(this.endDate);
    });
  }

  initForm() {
    this.queryForm = this.fb.group({
      governateId: ['', [Validators.required]],
      countryId: ['', [Validators.required]],
      GeoZoneId: ['', [Validators.required]],
      Date: [
        formatDate(new Date(), 'yyyy-MM-dd', 'en_US'),
        Validators.required,
      ],
      ChemistId: [''],
      showAllChimists: [false],
    });
  }
  getAllChimsts(value) {
    if (value) {
      let geZoneId = '';
      this.getChemistList(geZoneId);
    }
  }
  getAreas(id?: string) {
    if (id) {
      return this.service.getAreas(id).subscribe((items) => {
        this.areas = items.response.geoZones;
      });
    }
  }
  getCountries() {
    return this.service.getCountries().subscribe((items) => {
      this.countries = items.response.countries;
    });
  }
  getGovernts(id?: string) {
    if (id) {
      return this.service.getGovernats(id).subscribe((items) => {
        this.governats = items.response.governats;
      });
    }
  }
  search() {
    this.selectedChemistId = undefined;
    this.selectedGeoZoneId = undefined;
    this.selectedVisitDate = undefined;
    this.selectedTimeZoneFrame = undefined;
    let queryTime = this.queryForm.value;
    this.loading = true;
    this.submitted = true;
    this.queryForm.markAllAsTouched();

    if (this.queryForm.invalid) {
      this.responseFailed('Please Fill The Form');

      return;
    }

    if (queryTime.GeoZoneId && queryTime.Date && queryTime.ChemistId) {
      this.service
        .GetAvailableVisitsForChemist(
          queryTime.ChemistId,
          queryTime.GeoZoneId,
          queryTime.Date
        )
        .subscribe(
          (res) => {
            this.times = res.response;
            this.loading = false;
            this.submitted = false;
            this.showForm = false;
          },
          (err) => {
            this.responseFailed(err.message);
          }
        );
    } else {
      this.service
        .GetAvailableVisitsInArea(queryTime.GeoZoneId, queryTime.Date)
        .subscribe(
          (res) => {
            this.times = res.response;
            this.loading = false;
            this.submitted = false;
            this.showForm = false;
          },
          (err) => {
            this.responseFailed(err.message);
          }
        );
    }
  }
  responseSuccess() {
    this.notify.success(
      'Age Segment has been created successfully',
      'SUCCESS OPERATION'
    );
    this.submitted = false;
  }
  selectTimeZone(timeZoneId) {
    let query = this.queryForm.value
    this.selectedVisitDate = query.Date;
    this.selectedChemistId = query.ChemistId;
    this.selectedGeoZoneId = query.GeoZoneId;
    this.selectedTimeZoneFrame = timeZoneId;
    
  }
  responseFailed(err) {
    this.notify.error(err, 'FAILED OPERATION');
    this.submitted = false;
  }
  continueReservation() {
    if(this.selectedChemistId)
    this.router.navigate([`visits/home-visits-create/${this.selectedTimeZoneFrame}/${this.selectedVisitDate}/${this.selectedGeoZoneId}/${this.selectedChemistId}`]);
    else
    this.router.navigate([`visits/home-visits-create/${this.selectedTimeZoneFrame}/${this.selectedVisitDate}/${this.selectedGeoZoneId}`]);

  }

  clear() {
    this.queryForm.reset();
  }

  get f() {
    return this.queryForm.controls;
  }
}
