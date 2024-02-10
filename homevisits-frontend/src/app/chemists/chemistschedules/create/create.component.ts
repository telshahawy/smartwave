import { Component, OnInit } from '@angular/core';
import { ClientService } from '../../../core/data-services/client.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
//
//import {  MouseEvent } from '@agm/core';
import { NotifyService } from '../../../core/data-services/notify.service';
import { FormManage } from 'src/app/shared/custom-validators';
import { ActionsEnum } from 'src/app/core/permissions/models/actions';
import { PagesEnum } from 'src/app/core/permissions/models/pages';
@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css'],
})
export class CreateComponent extends FormManage implements OnInit {
  chemistScheduleForm: FormGroup;
  userName: string;
  address: string;
  private geoCoder;
  scheduleData = {
    chemistId: null,
    assignedChemistGeoZoneId: null,
    geoZoneId: null,
    startLatitude: null,
    startLangitude: null,
    startDate: null,
    endDate: null,
    sunStart: null,
    sunEnd: null,
    monStart: null,
    monEnd: null,
    tueStart: null,
    tueEnd: null,
    wedStart: null,
    wedEnd: null,
    thuStart: null,
    thuEnd: null,
    friStart: null,
    friEnd: null,
    satStart: null,
    satEnd: null,
  };
  chemistId;
  scheduleId;
  isSubmitted = false;
  areas = [];
  latitude: number;
  longitude: number;
  zoom: number;
  constructor(
    private service: ClientService,
    private route: ActivatedRoute,
    public router: Router,
    public notify: NotifyService,
    private formBuilder: FormBuilder
  ) {
    super(PagesEnum.ChemistSchedule, ActionsEnum.Create, router, notify)
    if(!this.scheduleId){
      this.setCurrentLocation();

    }
    this.chemistScheduleForm = this.formBuilder.group(
      {
        assignedChemistGeoZoneId: ['', Validators.required],
        geoZoneId: ['', Validators.required],
        startDate: ['', Validators.required],
        endDate: ['', Validators.required],
        satStart: [''],
        satEnd: [''],
        friStart: [''],
        friEnd: [''],
        thuStart: [''],
        thuEnd: [''],
        wedStart: [''],
        wedEnd: [''],
        tueStart: [''],
        tueEnd: [''],
        monStart: [''],
        monEnd: [''],
        sunStart: [''],
        sunEnd: [''],
        startLatitude: [
          null,
          [
            Validators.required,
            Validators.pattern('^(?=.)([+-]?([0-9]*)(.([0-9]+))?)$'),
          ],
        ],
        startLangitude: [
          null,
          [
            Validators.required,
            Validators.pattern('^(?=.)([+-]?([0-9]*)(.([0-9]+))?)$'),
          ],
        ],
      },
      {
        validators: [
          this.compareTwoTimes('satStart', 'satEnd'),
          this.compareTwoTimes('sunStart', 'sunEnd'),
          this.compareTwoTimes('monStart', 'monEnd'),
          this.compareTwoTimes('thuStart', 'thuEnd'),
          this.compareTwoTimes('tueStart', 'tueEnd'),
          this.compareTwoTimes('friStart', 'friEnd'),
          this.compareTwoTimes('wedStart', 'wedEnd'),
          this.compareTwoDates('startDate', 'endDate'),
        ],
      }
    );
  }

  isEditMode() {
    if (this.scheduleId) {
      return [{ value: '', disabled: true }, [Validators.required]];
    } else {
      return ['', [Validators.required]];
    }
  }

  get f() {
    return this.chemistScheduleForm.controls;
  }

  ngOnInit(): void {
    this.chemistId = this.route.snapshot.params.chemistId;
    let ll = this.route.snapshot.params.userName;
    if (ll.includes('%20')) {
      this.userName = ll.toString().replace('%20', ' ');
    } else {
      this.userName = ll.replace(/[ ]+$/g, '');
    }

    this.scheduleId = this.route.snapshot.params.scheduleId;

    if (this.scheduleId) {
      this.getById(this.scheduleId);
    }
    this.getAreas();
  }

  getById(id) {
    this.service.chemistScheduleService.getById(id).subscribe((res) => {
      this.chemistScheduleForm.patchValue(res.response);
      this.chemistScheduleForm.patchValue({
        geoZoneId: res.response.geoZoneId,
      });
      this.userName = res.response.chemistName;
    this.latitude =res.response.startLatitude
    this.longitude=res.response.startLangitude

    });
    console.log(
      this.latitude,
this.longitude
    );
  }
  getAreas() {
    this.service.GETGeoChemistZonesKeyValue(this.chemistId).subscribe((res) => {
      this.areas = res.response;
    });
  }
  backToList() {
    window.history.back();
    // this.router.navigate(['chemists/chemists-list']);
  }
  // Get Current Location Coordinates
  private setCurrentLocation() {
    if ('geolocation' in navigator) {
      navigator.geolocation.getCurrentPosition((position) => {
        this.latitude = position.coords.latitude;
        this.longitude = position.coords.longitude;
        this.zoom = 15;
      });
    }
  }

  markerDragEnd($event: google.maps.MouseEvent) {
    
    this.latitude = $event.latLng.lat();
    this.longitude = $event.latLng.lng();
   

    this.chemistScheduleForm.patchValue({
      startLatitude: this.latitude,
      startLangitude: this.longitude,
    });

    this.getAddress(this.latitude, this.longitude);
  }

  getAddress(latitude, longitude) {
    this.geoCoder?.geocode(
      { location: { lat: latitude, lng: longitude } },
      (results, status) => {
        
        if (status === 'OK') {
          if (results[0]) {
            this.zoom = 12;
            this.address = results[0].formatted_address;
          } else {
            window.alert('No results found');
          }
        } else {
          window.alert('Geocoder failed due to: ' + status);
        }
      }
    );
  }

  submit() {
    this.isSubmitted = true;
    this.chemistScheduleForm.markAllAsTouched()

    if (this.chemistScheduleForm.errors) {
      this.responseFailed('Please Fill The Form');
      return;
    }
    if(this.chemistScheduleForm?.value?.assignedChemistGeoZoneId && !this.scheduleId){
  this.scheduleData.assignedChemistGeoZoneId = JSON.parse(this.chemistScheduleForm.value.assignedChemistGeoZoneId).id;
    this.scheduleData.geoZoneId = JSON.parse(this.chemistScheduleForm.value.assignedChemistGeoZoneId).geoZoneId;
    }
    if(this.scheduleId){
    this.scheduleData.assignedChemistGeoZoneId =this.chemistScheduleForm.value.assignedChemistGeoZoneId
    this.scheduleData.geoZoneId =this.chemistScheduleForm.value.geoZoneId}
    if(this.chemistScheduleForm?.value?.startDate){
      this.scheduleData.startDate =
      this.chemistScheduleForm?.value?.startDate?.getFullYear() +
      '-' +
      (String(
        parseInt(this.chemistScheduleForm?.value?.startDate?.getMonth()) + 1
      ).length == 1
        ? '0' +
          String(
            parseInt(this.chemistScheduleForm?.value?.startDate?.getMonth()) + 1
          )
        : String(
            parseInt(this.chemistScheduleForm?.value?.startDate?.getMonth()) + 1
          )) +
      '-' +
      (String(this.chemistScheduleForm?.value?.startDate?.getDate()).length == 1
        ? '0' + this.chemistScheduleForm?.value?.startDate?.getDate()
        : this.chemistScheduleForm?.value?.startDate?.getDate());
    this.scheduleData.endDate =
      this.chemistScheduleForm?.value?.endDate?.getFullYear() +
      '-' +
      (String(parseInt(this.chemistScheduleForm?.value?.endDate?.getMonth()) + 1)
        .length == 1
        ? '0' +
          String(
            parseInt(this.chemistScheduleForm?.value?.endDate?.getMonth()) + 1
          )
        : String(
            parseInt(this.chemistScheduleForm?.value?.endDate?.getMonth()) + 1
          )) +
      '-' +
      (String(this.chemistScheduleForm?.value?.endDate?.getDate()).length == 1
        ? '0' + this.chemistScheduleForm.value.endDate?.getDate()
        : this.chemistScheduleForm.value.endDate?.getDate());
    }
    
    if (this.chemistScheduleForm.value.sunStart?.length != 0) {
      this.scheduleData.sunStart = this.chemistScheduleForm.value.sunStart;
    }
    if (this.chemistScheduleForm.value.sunEnd?.length != 0) {
      this.scheduleData.sunEnd = this.chemistScheduleForm.value.sunEnd;
    }
    if (this.chemistScheduleForm.value.monStart?.length != 0) {
      this.scheduleData.monStart = this.chemistScheduleForm.value.monStart;
    }
    if (this.chemistScheduleForm.value.monEnd?.length != 0) {
      this.scheduleData.monEnd = this.chemistScheduleForm.value.monEnd;
    }
    if (this.chemistScheduleForm.value.tueStart?.length != 0) {
      this.scheduleData.tueStart = this.chemistScheduleForm.value.tueStart;
    }
    if (this.chemistScheduleForm.value.tueEnd?.length != 0) {
      this.scheduleData.tueEnd = this.chemistScheduleForm.value.tueEnd;
    }
    if (this.chemistScheduleForm.value.wedStart?.length != 0) {
      this.scheduleData.wedStart = this.chemistScheduleForm.value.wedStart;
    }
    if (this.chemistScheduleForm.value.wedEnd?.length != 0) {
      this.scheduleData.wedEnd = this.chemistScheduleForm.value.wedEnd;
    }
    if (this.chemistScheduleForm.value.thuStart?.length != 0) {
      this.scheduleData.thuStart = this.chemistScheduleForm.value.thuStart;
    }
    if (this.chemistScheduleForm.value.thuEnd?.length != 0) {
      this.scheduleData.thuEnd = this.chemistScheduleForm.value.thuEnd;
    }

    if (this.chemistScheduleForm.value.friStart?.length != 0) {
      this.scheduleData.friStart = this.chemistScheduleForm.value.friStart;
    }

    if (this.chemistScheduleForm.value.friEnd?.length != 0) {
      this.scheduleData.friEnd = this.chemistScheduleForm.value.friEnd;
    }
    if (this.chemistScheduleForm.value.satStart?.length != 0) {
      this.scheduleData.satStart = this.chemistScheduleForm.value.satStart;
    }
    if (this.chemistScheduleForm.value.satEnd?.length != 0) {
      this.scheduleData.satEnd = this.chemistScheduleForm.value.satEnd;
    }

    this.scheduleData.startLatitude = this.chemistScheduleForm.value.startLatitude;
    this.scheduleData.startLangitude = this.chemistScheduleForm.value.startLangitude;
    this.scheduleData.chemistId = this.chemistId;

    if (this.scheduleId) {
      this.edit(this.scheduleData);
    } else {
      this.create(this.scheduleData);
    }
  }
  create(scheduleData) {
    this.service.chemistScheduleService.create(scheduleData).subscribe(
      (res) => {
        this.responseSuccess();
        
      },
      (err) => {
        this.responseFailed(err.message);
      }
    );
  }
  edit(scheduleData) {
    this.service.chemistScheduleService
      .edit(scheduleData, this.scheduleId)
      .subscribe(
        (res) => {
          this.responseSuccess();
        },
        (err) => {
          this.responseFailed(err.message);
        }
      );
  }
  responseSuccess() {
    this.notify.success(
      'Schedule has been created successfully',
      'SUCCESS OPERATION'
    );
    window.history.back();
    this.isSubmitted = false;
  }

  responseFailed(err) {
    this.notify.error(err, 'FAILED OPERATION');
    this.isSubmitted = false;
  }
}
