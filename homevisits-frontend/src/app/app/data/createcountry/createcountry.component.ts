import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { ClientService } from 'src/app/core/data-services/client.service';
import { NotifyService } from '../../../core/data-services/notify.service';
import { BaseComponent } from 'src/app/core/permissions/base.component';
import { ActionsEnum } from 'src/app/core/permissions/models/actions';
import { PagesEnum } from 'src/app/core/permissions/models/pages';

@Component({
  selector: 'app-createcountry',
  templateUrl: './createcountry.component.html',
  styleUrls: ['./createcountry.component.css'],
})
export class CreatecountryComponent extends BaseComponent implements OnInit {
  submitted:boolean=false;
  countryForm:FormGroup
  userData: any;
  isActive;
  name;
  Id;
  modileLength;
  CountryId;
  countryPage : PagesEnum = PagesEnum.Countries;
  myaction = 'create';
  constructor(
    public router: Router,
    private route: ActivatedRoute,
    private service: ClientService,
    public notify: NotifyService,
    private fb:FormBuilder
  ) {
    super(PagesEnum.Countries,ActionsEnum.Create,router,notify);
    this.route.params.subscribe((paramsId) => {
      this.CountryId = paramsId.id;
      if (this.CountryId) {
        this.myaction = 'edit';
       
        this.service.getCountry(this.CountryId).subscribe((res) => {
          this.countryForm.patchValue({
            countryNameEn :res.response?.countryName,
            mobileNumberLength :res.response?.mobileNumberLength,
          // console.log('geoZones : ', geoZones),
          isActive :res.response?.isActive,
          })
         
        });
      }
    });
  }

  ngOnInit(): void {  
     this.initForm()
  }
  initForm() {
    this.countryForm = this.fb.group({
      countryNameEn: ['', [ Validators.required]],
      mobileNumberLength: [null, [Validators.required,Validators.pattern('^[0-9]*$'),Validators.max(99)]],
      isActive: [false, [Validators.required]],
    })
  }
  get f() {
    return this.countryForm.controls;
  }
  backToList() {
    this.router.navigate(['data/countries-list']);
  }



  onSubmit() {
    this.submitted=true
    this.countryForm.markAllAsTouched()

    if (this.countryForm.invalid) {
      this.responseFailed('Please Fill The Form')
      
      return;
    }

    let country = this.countryForm.value;
    if (this.CountryId) {
      this.edit(country);
    } else {
      this.create(country);
    }
  }
  create(country) {
    this.service.createCountry(country).subscribe(res => {
      this.responseSuccess()
    },
      (err) => {
        console.log(err);
        
        this.responseFailed(err.message)
      })
  }
  edit(country) {
    
    this.service.updateCountry(country, this.CountryId).subscribe(res => {
      this.responseSuccess()
    },
      (err) => {

        this.responseFailed(err.message)
      })
  }
  responseSuccess() {
    this.notify.success('countries has been created successfully',
      'SUCCESS OPERATION')
    this.router.navigate(['data/countries-list']);
    this.submitted = false

  }

  responseFailed(err) {
    this.notify.error(err, 'FAILED OPERATION');
    this.submitted = false

  }

}
