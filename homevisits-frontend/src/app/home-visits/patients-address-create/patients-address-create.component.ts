import { MapsAPILoader } from '@agm/core';
import {
  Appearance,
  GermanAddress,
  Location,
} from '@angular-material-extensions/google-maps-autocomplete';
import {
  Component,
  ElementRef,
  NgZone,
  OnInit,
  ViewChild,
} from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ClientService } from 'src/app/core/data-services/client.service';
import { NotifyService } from 'src/app/core/data-services/notify.service';
import {
  AreaLookup,
  CountryLookup,
  GovernateLookup,
} from 'src/app/core/models/models';
import { BaseComponent } from 'src/app/core/permissions/base.component';
import { ActionsEnum } from 'src/app/core/permissions/models/actions';
import { PagesEnum } from 'src/app/core/permissions/models/pages';
import PlaceResult = google.maps.places.PlaceResult;

@Component({
  selector: 'app-patients-address-create',
  templateUrl: './patients-address-create.component.html',
  styleUrls: ['./patients-address-create.component.css'],
})
export class PatientsAddressCreateComponent extends BaseComponent
  implements OnInit {
  countries: CountryLookup[];
  governats: GovernateLookup[];
  areas: AreaLookup[];
  geoZoneId: string = null;
  selectedGovernateId: string;
  selectedCountryId: string;
  selectedAreaId: string;
  patientId: string;
  relativeType: number;
  isBackToSearch: boolean;
  private geoCoder;
  public appearance = Appearance;
  public zoom: number;
  public latitude: number;
  public longitude: number;
  public selectedAddress: PlaceResult;
  address: string;
  isSelectAddress: boolean = false;
  submitted: boolean = false;
  @ViewChild('search')
  public searchElementRef: ElementRef;
  constructor(
    private service: ClientService,
    public notify: NotifyService,
    private route: ActivatedRoute,
    public router: Router,
    private mapsAPILoader: MapsAPILoader,
    private ngZone: NgZone
  ) {
    super(PagesEnum.Patient, ActionsEnum.Create, router, notify);
  }

  ngOnInit(): void {
    this.route.params.subscribe((paramsId) => {
      (this.patientId = paramsId.patientId),
        (this.relativeType = paramsId.relativeType);
    });
    //this.getAreas();
    this.getCountries();
    // this.getGovernts();
    // this.geoCoder = new google.maps.Geocoder;
    // this.setCurrentPosition();
    this.mapsAPILoader.load().then(() => {
      this.setCurrentPosition();
      this.geoCoder = new google.maps.Geocoder();

      let autocomplete = new google.maps.places.Autocomplete(
        this.searchElementRef.nativeElement
      );
      autocomplete.addListener('place_changed', () => {
        this.ngZone.run(() => {
          let place: google.maps.places.PlaceResult = autocomplete.getPlace();

          if (place.geometry === undefined || place.geometry === null) {
            return;
          }
          this.isSelectAddress = true;
          this.latitude = place.geometry.location.lat();
          this.longitude = place.geometry.location.lng();
          this.zoom = 12;
        });
      });
    });
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
    const dto = form.value;
    if (dto) {
      if (
        this.latitude != undefined &&
        this.longitude != undefined &&
        this.isSelectAddress
      ) {
        dto.latitude = this.latitude.toString();
        dto.longitude = this.longitude.toString();
      }
      dto.patientId = this.patientId;
      this.service.createPatientAddress(dto).subscribe(
        (res) => {
          console.log(res);
          // this.router.navigate(['visits/home-visits-create']);
          //let relative=+this.relativeType
          this.router.navigate(['visits/visit-data'], {
            queryParams: {
              patientId: this.patientId,
              geoZoneId: res.response.geoZoneId,
              relativeType: +this.relativeType,
              addressId: res.response.patientAddressId,
            },
          });
          this.notify.saved();
          this.submitted = false;
        },
        (error) => {
          this.notify.error(error.message, 'FAILED OPERATION');
          console.log(error);
          this.submitted = false;
        }
      );
    }
  }
  onOptionsSelected(value: string) {
    this.getGovernts(value);
  }
  onGovernateOptionsSelected(value: string) {
    this.getAreas(value);
  }
  backToSearchPatient() {
    this.isBackToSearch = true;
    this.router.navigate(['visits/home-visits-create']);
  }
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
    this.isSelectAddress = true;
    // this.getAddress(this.latitude, this.longitude);
  }

  // getAddress(latitude, longitude) {
  //   this.geoCoder.geocode(
  //     { location: { lat: latitude, lng: longitude } },
  //     (results, status) => {
  //       console.log(results);
  //       console.log(status);
  //       if (status === 'OK') {
  //         if (results[0]) {
  //           this.zoom = 12;
  //           this.address = results[0].formatted_address;
  //         } else {
  //           window.alert('No results found');
  //         }
  //       } else {
  //         window.alert('Geocoder failed due to: ' + status);
  //       }
  //     }
  //   );
  // }
  // getAddress(latitude, longitude) {
  //   this.geoCoder = new google.maps.Geocoder;
  //   this.geoCoder.geocode({ 'location': { lat: latitude, lng: longitude } }, (results, status) => {
  //     console.log(results);
  //     console.log(status);
  //     if (status === 'OK') {
  //       if (results[0]) {
  //         this.zoom = 12;
  //         this.address = results[0].formatted_address;
  //       } else {
  //         window.alert('No results found');
  //       }
  //     } else {
  //       window.alert('Geocoder failed due to: ' + status);
  //     }

  //   });
  // }
}
