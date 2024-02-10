import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ClientService } from 'src/app/core/data-services/client.service';
import { NotifyService } from 'src/app/core/data-services/notify.service';
import { RolesSearchCriteria } from 'src/app/core/models/models';
import { BaseComponent } from 'src/app/core/permissions/base.component';
import { ActionsEnum } from 'src/app/core/permissions/models/actions';
import { PagesEnum } from 'src/app/core/permissions/models/pages';
import { FormManage } from 'src/app/shared/custom-validators';

@Component({
  selector: 'app-tat-tracking',
  templateUrl: './tat-tracking.component.html',
  styleUrls: ['./tat-tracking.component.css'],
})
export class TatTrackingComponent extends FormManage implements OnInit {
  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    public router: Router,
    private service: ClientService,
    public notify: NotifyService,
  ) {
    super(PagesEnum.TATTracking, ActionsEnum.View, router, notify);
  }


  visitReportForm: FormGroup;
  submitted = false
  segmentId;
  governorates;
  countries;
  criteria;
  chemist;
  areas;
  delayed = [
    { name: 'all' },
    { name: 'yes' },
    { name: 'no' },
  ];

  ngOnInit(): void {
    this.initForm()
    this.getGovernorates();
    this.getCountries();
    this.getChemist();
    this.getAreas();

  }
  getGovernorates() {
    return this.service.searchgovers(this.criteria).subscribe((items) => {
      this.governorates = items.response.governats;
    });
  }
  getCountries() {
    return this.service.getCountries().subscribe((items) => {
      this.countries = items.response.countries;
    });
  }
  getAreas() {
    return this.service.searchAreas(this.criteria).subscribe((items) => {
      this.areas = items.response.geoZones;
    });
  }
  getChemist() {
    return this.service.searchChemists(this.criteria).subscribe((items) => {
      this.chemist = items.response.chemists;
    });
  }


  // goToDetails(value) {
  //   console.log(value);
  //   this.router.navigate(['/client/reports/visit-reports/details'])

  // }
  initForm() {
    this.visitReportForm = this.fb.group({
      visitDateFrom: ['', [Validators.required]],
      visitDateTo: ['', [Validators.required]],
      delayed: [''],
      countryId: [''],
      governorateId: [''],
      chemistId: [''],
      areaId: [''],
      reason: [''],
      showDetails: [false]
    }, {
      validators: this.compareTwoDates('visitDateFrom', 'visitDateTo'),
    })

  }
  get f() {
    return this.visitReportForm.controls;
  }

  onSubmit() {
    this.submitted = true

    if (this.visitReportForm.invalid) {
      this.responseFailed('Please Fill The Form')
      return;
    }

    let visit = this.visitReportForm.value;
    var searchData = {};
    if (visit.visitDateFrom) {
      searchData['visitDateFrom'] =
        visit.visitDateFrom.getFullYear() +
        '-' +
        (String(parseInt(visit.visitDateFrom.getMonth() + 1)).length == 1
          ? '0' + String(parseInt(visit.visitDateFrom.getMonth() + 1))
          : String(parseInt(visit.visitDateFrom.getMonth() + 1))) +
        '-' +
        (String(visit.visitDateFrom.getDate()).length == 1
          ? '0' + visit.visitDateFrom.getDate()
          : visit.visitDateFrom.getDate());
    }
    if (visit.visitDateTo) {
      searchData['visitDateTo'] =
        visit.visitDateTo.getFullYear() +
        '-' +
        (String(parseInt(visit.visitDateTo.getMonth() + 1)).length == 1
          ? '0' + String(parseInt(visit.visitDateTo.getMonth() + 1))
          : String(parseInt(visit.visitDateTo.getMonth() + 1))) +
        '-' +
        (String(visit.visitDateTo.getDate()).length == 1
          ? '0' + visit.visitDateTo.getDate()
          : visit.visitDateTo.getDate());
    }
    if (visit.delayed) {
      searchData['delayed'] = visit['delayed']
    }
    if (visit.countryId) {
      searchData['countryId'] = visit['countryId']

    }
    if (visit.governorateId) {
      searchData['governorateId'] = visit['governorateId']

    }
    if (visit.chemistId) {
      searchData['chemistId'] = visit['chemistId']

    }
    if (visit.areaId) {
      searchData['areaId'] = visit['areaId']
    }
    if (visit.showDetails)
      searchData['showDetails'] = visit['showDetails']

    if (searchData['showDetails'] == false) {
      this.createTATTotalReport(searchData);
    } else {
      this.createTATTotalReport(searchData);
    }
  }

  createTATTotalReport(visit) {
    this.service.reportsService.getVisitReject(visit).subscribe(res => {
      this.responseSuccess(res)
      this.router.navigate(['/client/reports/tat-tracking/total']);

    },
      (err) => {
        this.responseFailed(err.message)
      })
  }

createTATDetailsReport(visit) {
    this.service.reportsService.getVisitReject(visit).subscribe(res => {
      this.responseSuccess(res)
      this.router.navigate(['/client/reports/tat-tracking/details']);

    },
      (err) => {
        this.responseFailed(err.message)
      })
  }


  responseSuccess(res) {
    this.service.reportsService.getVisitesRes(res?.response)

    this.notify.success('Reports has been Generated successfully',
      'SUCCESS OPERATION')

    this.submitted = false


  }

  responseFailed(err) {
    this.notify.error(err, 'FAILED OPERATION');
    this.submitted = false

  }
}
