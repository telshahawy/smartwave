import { formatDate } from '@angular/common';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ClientService } from 'src/app/core/data-services/client.service';
import { NotifyService } from 'src/app/core/data-services/notify.service';
import PlaceResult = google.maps.places.PlaceResult;
import {
  AreaLookup,
  CountryLookup,
  CreatePatientAddress,
  Gender,
  GovernateLookup,
  Phones,
  SearchPatientsList,
  SearchPatientsSendToParent,
} from 'src/app/core/models/models';
import {
  Appearance,
  GermanAddress,
  Location,
} from '@angular-material-extensions/google-maps-autocomplete';
import { BaseComponent } from 'src/app/core/permissions/base.component';
import { ActionsEnum } from 'src/app/core/permissions/models/actions';
import { PagesEnum } from 'src/app/core/permissions/models/pages';


@Component({
  selector: 'app-patient-create',
  templateUrl: './patient-create.component.html',
  styleUrls: ['./patient-create.component.css'],
})
export class PatientCreateComponent extends BaseComponent implements OnInit {
  genderType = Gender;
  gender: number = null;
  phones: Phones[] = [];
  geoZoneId: string = null;
  mobNumberPattern = '([+]|0)[0-9]{1,}';
  countries: CountryLookup[];
  governats: GovernateLookup[];
  areas: AreaLookup[];
  selectedGovernateId: string;
  selectedCountryId: string;
  selectedAreaId: string = 'null';
  addresses: CreatePatientAddress[] = [];
  createPatientAddress: CreatePatientAddress;
  phone: Phones;
  patientData: SearchPatientsList;
  sendData: SearchPatientsSendToParent;
  submitted: boolean = false;
  public appearance = Appearance;
  public zoom: number;
  public latitude: number;
  public longitude: number;
  public selectedAddress: PlaceResult;
  address: string;
  phoneNumber;
  //@Output() createdPatient=new EventEmitter<SearchPatientsSendToParent>();
  isBackToSearch: boolean;

  @Output() backFromPatientCreate = new EventEmitter<boolean>();
  constructor(
    private service: ClientService,
    private route: ActivatedRoute,
    public notify: NotifyService,
    public router: Router
  ) {
    super(PagesEnum.Patient, ActionsEnum.Create, router, notify);
  }

  ngOnInit(): void {
    this.phoneNumber = this.route.snapshot.params.phoneNumber;
    this.getCountries();
    this.setCurrentPosition();
  }
  getAreas(id?: string) {
    if (id) {
      return this.service.getAreas(id).subscribe((items) => {
        this.areas = items.response.geoZones;
      });
    } else {
      this.areas = [];
    }
  }
  getCountries() {
    return this.service.getCountries().subscribe((items) => {
      this.countries = items.response.countries;
    });
  }
  getGovernts(id?: string) {
    this.governats = [];
    if (id) {
      return this.service.getGovernats(id).subscribe((items) => {
        this.governats = items.response.governats;
      });
    } else {
      this.governats = [];
      this.areas = [];
    }
  }
  onSubmit(form: NgForm) {
    this.submitted = true;
    this.createPatientAddress = new CreatePatientAddress();
    this.phone = new Phones();
    const dto = form.value;
    if (dto) {
      var date1 = formatDate(new Date(), 'yyyy-MM-dd', 'en_US');
      var date2 = formatDate(dto.birthDate, 'yyyy-MM-dd', 'en_US');
      if (date2 > date1) {
        this.notify.error(
          'You Must Choose Date of Birth Less Than Today',
          'FAILED OPERATION'
        );
        this.submitted = false;
      } else {
        if (this.latitude != undefined && this.longitude != undefined) {
          dto.latitude = this.latitude.toString();
          dto.longitude = this.longitude.toString();
        }
        // this.createPatientAddress.street=dto.street;
        // this.createPatientAddress.building=dto.building;
        // this.createPatientAddress.flat=dto.flat;
        // this.createPatientAddress.floor=dto.floor;
        // this.createPatientAddress.longitude=dto.longitude;
        // this.createPatientAddress.latitude=dto.latitude;
        // this.createPatientAddress.geoZoneId=dto.geoZoneId;
        // this.createPatientAddress.additionalInfo=dto.additionalInfo;
        // this.addresses.push(this.createPatientAddress);
        dto.gender = parseInt(dto.gender);
        if (dto.phoneNumber != undefined) {
          this.phone.phone = dto.phoneNumber;
          this.phones.push(this.phone);
        }
        dto.phones = this.phones;
        dto.addresses = []; //this.addresses
        this.service.createPatient(dto).subscribe(
          (res) => {
            console.log(res);
            if (res.responseCode == 1) {
              this.submitted = false;
              this.notify.saved();
              // let relativeType=1;
              let patientId = res.response;
              // this.router.navigate(['/visits/patients-address', patientId,relativeType]);
              this.router.navigate(['visits/patients-data', patientId]);
            }

            //this.router.navigate(['visits/home-visits-create']);
          },
          (error) => {
            this.submitted = false;
            this.notify.error(error.message, 'FAILED OPERATION');
            console.log(error);
          }
        );
      }
    }
  }
  onOptionsSelected(value: string) {
    this.getGovernts(value);
  }
  onGovernateOptionsSelected(value: string) {
    this.getAreas(value);
  }
  onlyNumberKey(event) {
    return event.charCode == 8 || event.charCode == 0
      ? null
      : event.charCode >= 48 && event.charCode <= 57;
  }
  backToSearchPatient() {
    this.isBackToSearch = true;
    this.router.navigate(['visits/home-visits-create']);
  }
  getPatientById(id: string) {}

  setCurrentPosition() {
    if ('geolocation' in navigator) {
      navigator.geolocation.getCurrentPosition((position) => {
        this.latitude = position.coords.latitude;
        this.longitude = position.coords.longitude;
        this.zoom = 12;
        // this.getAddress(this.latitude, this.longitude);
      });
    }
  }

  onAutocompleteSelected(result: PlaceResult) {
    console.log('onAutocompleteSelected: ', result);
    this.address = result.formatted_address;
  }

  onLocationSelected(location: Location) {
    console.log('onLocationSelected: ', location);
    this.latitude = location.latitude;
    this.longitude = location.longitude;
  }

  onGermanAddressMapped($event: GermanAddress) {
    console.log('onGermanAddressMapped', $event);
  }
  markerDragEnd($event: google.maps.MouseEvent) {
    console.log($event);
    this.latitude = $event.latLng.lat();
    this.longitude = $event.latLng.lng();
    // this.getAddress(this.latitude, this.longitude);
  }
}
