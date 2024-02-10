import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
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

@Component({
  selector: 'app-chemists-create',
  templateUrl: './chemists-create.component.html',
  styleUrls: ['./chemists-create.component.css'],
})
export class ChemistsCreateComponent extends BaseComponent implements OnInit {
  areas: AreaLookup[];
  chemistName: string;
  countries: CountryLookup[];
  governats: GovernateLookup[];
  selectedCountryId: string='';
  selectedGovernateId: string='';
  submitted=false
  chemistId: string;
  expertChemist:boolean=false;
  isActive:boolean=false;
  areeeas = [
    {
      name: 1,
      geoZoneId: 1,
    },
    {
      name: 2,
      geoZoneId: 2,
    },
  ];
  url: any;
  url2: any;
  keys: any[];
  mobNumberPattern = "([+]|0)[0-9]{1,}"; 
  passwordPattern="(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*?&])[\\A-Z\\a-z\\d\\$@$!%*?&].{7,}";  
  personalPhoto: string;
  genderkeys = Object.keys;
  genderType = Gender;
  gender:number=null;
  showPass = false;
  @ViewChild('chemistForm') chemistForm: NgForm;
  constructor(
    private service: ClientService,
    public notify: NotifyService,
    private route: ActivatedRoute,
    public router: Router
  ) {
    super(PagesEnum.AddNewChemists, ActionsEnum.Create, router, notify);

  }

  ngOnInit(): void {
   // this.chemistForm.reset();
  //  this.chemistForm.controls["password"].setValue('');
    this.getCountries();
    this.enumFilter();
  }
  TogglePassword() {
    var passwordInput = <HTMLInputElement>document.getElementById('loginPass');
    if (passwordInput.type === 'password') {
      passwordInput.type = 'text';
      this.showPass = true;
    } else {
      passwordInput.type = 'password';
      this.showPass = false;
    }
  }

  getAreas(id?: string) {
    console.log(id,'area');
    
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
    console.log(id,'gover');
    
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
    this.submitted=true
    
    const dto = form.value;
    if (dto) {
      dto.gender=parseInt( dto.gender);
      if(dto.geoZoneIds=="" || dto.geoZoneIds=="undefined")
      {
        dto.geoZoneIds=[];
      }
      this.service.createChemist(dto).subscribe(
        (res) => {
          form.reset({
            userName:'',
            password:''
 
           });
         
          this.router.navigate(['chemists/chemists-list']);
          this.notify.saved();
    this.submitted=false

         
        },
        (error) => {
          this.notify.error(error.message, 'FAILED OPERATION');
    this.submitted=false

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
  setValidation() {
    this.chemistForm.controls['name'].setValidators([Validators.required]);
  }

  backToList() {
    this.router.navigate(['chemists/chemists-list']);
  }
  enumFilter() {
    this.keys = Object.keys(this.genderType)
      .filter((k) => !isNaN(Number(k)))
      .map(k => parseInt(k));
  }
  onlyNumberKey(event) {
    return (event.charCode == 8 || event.charCode == 0) ? null : event.charCode >= 48 && event.charCode <= 57;
}
}
