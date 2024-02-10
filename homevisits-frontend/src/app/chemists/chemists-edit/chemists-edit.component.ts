import { DatePipe } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { Form, NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
//import { format } from 'path';
import { ClientService } from 'src/app/core/data-services/client.service';
import { NotifyService } from 'src/app/core/data-services/notify.service';
import {
  AreaLookup,
  CountryLookup,
  Gender,
  GovernateLookup,
} from 'src/app/core/models/models';
import { BaseComponent } from 'src/app/core/permissions/base.component';
import { ActionsEnum } from 'src/app/core/permissions/models/actions';
import { PagesEnum } from 'src/app/core/permissions/models/pages';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-chemists-edit',
  templateUrl: './chemists-edit.component.html',
  styleUrls: ['./chemists-edit.component.css'],
})
export class ChemistsEditComponent extends BaseComponent implements OnInit {
  areas: AreaLookup[];
  chemistName: string;
  countries: CountryLookup[];
  submitted = false;
  governats: GovernateLookup[];
  selectedCountryId: string;
  selectedGovernateId: string;
  chemistId: string;
  url: any;
  url2: any;
  keys: any[];
  birthdate: Date;
  areasArray: any[];
  genderkeys = Object.keys;
  isView: boolean = true;
  genderType = Gender;
  expertChemist: boolean = false;
  isActive: boolean = false;
  @ViewChild('chemistForm') chemistForm: NgForm;
  baseUrl: string = environment.baseUrl;
  mobNumberPattern = '([+]|0)[0-9]{1,}';
  constructor(
    private service: ClientService,
    private route: ActivatedRoute,
    public router: Router,
    public datepipe: DatePipe,
    public notify: NotifyService
  ) {
    super(PagesEnum.ViewChemists, ActionsEnum.Update, router, notify);
  }

  ngOnInit(): void {
    this.route.params.subscribe((paramsId) => {
      this.chemistId = paramsId.chemistId;
    });
    // this.getCountries();
    this.enumFilter();
    if (this.chemistId != undefined) {
      this.getChemist(this.chemistId);
      this.getAreas();
      this.getCountries();
      this.getGovernts();
    }
  }
  getAreas(id?: string) {
    return this.service.getAreas(id).subscribe((items) => {
      this.areas = items.response.geoZones;
    });
  }
  getCountries() {
    return this.service.getCountries().subscribe((items) => {
      this.countries = items.response.countries;
    });
  }
  getGovernts(id?: string) {
    return this.service.getGovernats(id).subscribe((items) => {
      this.governats = items.response.governats;
    });
  }
  onSubmit(form: NgForm) {
    this.submitted = true;
    console.log('Your form data : ', form.value);
    const dto = form.value;
    if (dto) {
      dto.gender = parseInt(dto.gender);
      if (dto.geoZoneIds == '' || dto.geoZoneIds == 'undefined') {
        dto.geoZoneIds = [];
      }
      this.service.updateChemist(dto).subscribe(
        (res) => {
          console.log(res);
          this.router.navigate(['chemists/chemists-list']);
          this.notify.update();
          this.submitted = false;
        },
        (error) => {
          this.notify.error(error.message, 'FAILED OPERATION');
          this.submitted = false;
        }
      );
    }
  }

  getChemist(id: string) {
    this.service.getChemist(id).subscribe((res) => {
      this.chemistForm.controls['name'].setValue(res.response.chemist.name);
      this.chemistForm.controls['code'].setValue(res.response.chemist.code);
      this.chemistForm.controls['userId'].setValue(
        res.response.chemist.chemistId
      );
      if (res.response.chemist.geoZones.length > 0) {
        this.areasArray = res.response.chemist.geoZones.map((o) => {
          return o.geoZoneId;
        });
        this.chemistForm.controls['geoZoneIds'].setValue(this.areasArray);
        this.chemistForm.controls['countryId'].setValue(
          res.response.chemist.geoZones[0].countryId
        );
        this.chemistForm.controls['governateId'].setValue(
          res.response.chemist.geoZones[0].governateId
        );
        this.isView = false;
      }

      this.chemistForm.controls['gender'].setValue(res.response.chemist.gender);

      this.chemistForm.controls['phoneNumber'].setValue(
        res.response.chemist.phoneNumber
      );

      this.birthdate = res.response.chemist.birthdate;
      //this.chemistForm.controls["birthdate"].setValue(res.response.chemist.birthdate);
      this.url = this.baseUrl + res.response.chemist.personalPhoto;
      this.chemistForm.controls['personalPhoto'].setValue(
        res.response.chemist.personalPhoto
      );
      this.chemistForm.controls['isActive'].setValue(
        res.response.chemist.isActive
      );
      this.chemistForm.controls['expertChemist'].setValue(
        res.response.chemist.expertChemist
      );
      this.chemistForm.controls['userName'].setValue(
        res.response.chemist.userName
      );
      //this.url=res.response.chemist.personalPhoto;
    });
  }
  onOptionsSelected(value: string) {
    if (!this.isView) {
      this.getGovernts(value);
    }
  }
  onGovernateOptionsSelected(value: string) {
    if (!this.isView) {
      this.getAreas(value);
    }
  }
  getFiles(event) {
    if (event.target.files && event.target.files[0]) {
      const formData = new FormData();
      let file = event.target.files[0];
      formData.append('personalPhoto', file);
      this.service.getAttachment(formData).subscribe((res) => {
        this.url2 = res.response;
        //.personalPhoto=this.url2;
        this.chemistForm.controls['personalPhoto'].setValue(this.url2);
      });

      var reader = new FileReader();

      reader.readAsDataURL(event.target.files[0]); // read file as data url

      reader.onload = (event) => {
        // called once readAsDataURL is completed
        this.url = event.target.result;
      };
    }
  }
  backToList() {
    this.router.navigate(['chemists/chemists-list']);
  }
  enumFilter() {
    this.keys = Object.keys(this.genderType)
      .filter((k) => !isNaN(Number(k)))
      .map((k) => parseInt(k));
  }
  // onlyNumberKey(event) {
  //   return (event.charCode == 8 || event.charCode == 0) ? null : event.charCode >= 48 && event.charCode <= 57;
  // }
}
