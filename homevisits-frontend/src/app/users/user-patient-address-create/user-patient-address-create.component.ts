import { MapsAPILoader } from '@agm/core';
import { Appearance, GermanAddress,Location } from '@angular-material-extensions/google-maps-autocomplete';
import { Component, ElementRef, NgZone, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ClientService } from 'src/app/core/data-services/client.service';
import { NotifyService } from 'src/app/core/data-services/notify.service';
import { AreaLookup, CountryLookup, GovernateLookup } from 'src/app/core/models/models';
import PlaceResult = google.maps.places.PlaceResult;
@Component({
  selector: 'app-user-patient-address-create',
  templateUrl: './user-patient-address-create.component.html',
  styleUrls: ['./user-patient-address-create.component.css']
})
export class UserPatientAddressCreateComponent implements OnInit {
  countries: CountryLookup[];
  governats: GovernateLookup[];
  areas:AreaLookup[];
  geoZoneId:string=null;
  selectedGovernateId: string;
  selectedCountryId:string;
  selectedAreaId:string;
  patientId:string;
  relativeType:number;
  isBackToSearch:boolean;
  private geoCoder;
  public appearance = Appearance;
  public zoom: number;
  public latitude: number;
  public longitude: number;
  public selectedAddress: PlaceResult;
  address: string;
  isSelectAddress:boolean=false;
  submitted:boolean = false;
  @ViewChild('search')
  public searchElementRef: ElementRef;
  constructor(private service:ClientService,private notify:NotifyService,
    private route :ActivatedRoute,private router:Router,
    private mapsAPILoader: MapsAPILoader,
    private ngZone: NgZone) { }

    ngOnInit(): void {
      this.route.params.subscribe(paramsId=>{this.patientId=paramsId.patientId,this.relativeType=paramsId.relativeType});
      //this.getAreas();
      this.getCountries();
     // this.getGovernts();
    // this.geoCoder = new google.maps.Geocoder;
    // this.setCurrentPosition();
    this.mapsAPILoader.load().then(() => {
      this.setCurrentPosition();
      this.geoCoder = new google.maps.Geocoder;
  
      let autocomplete = new google.maps.places.Autocomplete(this.searchElementRef.nativeElement);
      autocomplete.addListener("place_changed", () => {
        this.ngZone.run(() => {
          let place: google.maps.places.PlaceResult = autocomplete.getPlace();
  
          if (place.geometry === undefined || place.geometry === null) {
            return;
          }
          this.isSelectAddress=true;
          this.latitude = place.geometry.location.lat();
          this.longitude = place.geometry.location.lng();
          this.zoom = 12;
        });
      });
    });
    }
    getAreas(id?:string) {
      if(id)
      {
      return this.service.getAreas(id).subscribe(items => {
        this.areas = items.response.geoZones;
      });
    }
    else
    {
      this.areas =[];
    }
    }
    getCountries() {
      return this.service.getCountries().subscribe(items => {
        this.countries = items.response.countries;
      });
    }
    getGovernts(id?:string) {
      this.governats=[] ;
      if(id)
      {
        return this.service.getGovernats(id).subscribe(items => {
          this.governats = items.response.governats;
        });
      }
     else
     {
       this.governats=[] ;
       this.areas =[];  }
      
    }
  
  onSubmit(form: NgForm) {
    this.submitted = true;
      const dto = form.value;
      if (dto) {
        if(this.latitude!=undefined&& this.longitude!=undefined && this.isSelectAddress)
        {
            dto.latitude=this.latitude.toString();
            dto.longitude=this.longitude.toString();
  
        }
        dto.patientId=this.patientId;
        this.service.createPatientAddress(dto).subscribe(res => {
          console.log(res);
          this.router.navigate(['/users/patients-data', this.patientId]); 
          this.notify.saved();
          this.submitted = false;
        
        }, error => {
          this.notify.error(error.message,'FAILED OPERATION');
          console.log(error);
          this.submitted = false;
        });
      }
  }
  onOptionsSelected(value:string){
    this.getGovernts(value);
  }
  onGovernateOptionsSelected(value:string){
    this.getAreas(value);
  }
  backToSearchPatient()
  {
    this.isBackToSearch=true;
    this.router.navigate(['users/patients-list']);
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
    this.address=result.formatted_address;
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
     this.longitude =  $event.latLng.lng();
     this.isSelectAddress=true;
   // this.getAddress(this.latitude, this.longitude);
  }

}
