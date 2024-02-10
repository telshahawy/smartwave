import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { BaseComponent } from 'src/app/core/permissions/base.component';
import { ActionsEnum } from 'src/app/core/permissions/models/actions';
import { PagesEnum } from 'src/app/core/permissions/models/pages';
import { ClientService } from '../../../core/data-services/client.service';
import { NotifyService } from '../../../core/data-services/notify.service';

@Component({
  selector: 'app-governorates-create',
  templateUrl: './governorates-create.component.html',
  styleUrls: ['./governorates-create.component.css'],
})
export class GovernoratesCreateComponent
  extends BaseComponent
  implements OnInit {
  govPage: PagesEnum = PagesEnum.Governorates;
  governmentForm: FormGroup;
  submitted: boolean = false;
  userData: any;
  isActive;
  name;
  Id;
  selectedCountryId: string;
  customerServiceEmail;
  GoverId;
  countries = [];
  myaction = 'create';
  constructor(
    public router: Router,
    private route: ActivatedRoute,
    private service: ClientService,
    public notify: NotifyService,
    private fb: FormBuilder
  ) {
    super(PagesEnum.Governorates, ActionsEnum.Create, router, notify);
    this.route.params.subscribe((paramsId) => {
      this.GoverId = paramsId.id;
      if (this.GoverId) {
        this.myaction = 'edit';
        this.getById();
      }
    });
  }
  getById() {
    this.service.getGover(this.GoverId).subscribe((res) => {
      this.governmentForm.patchValue(res.response);
      this.governmentForm.patchValue({
        governateNameEn: res.response.governateName,
      });
    });
  }

  ngOnInit(): void {
    this.getCountries();
    this.initForm();
  }
  initForm() {
    this.governmentForm = this.fb.group({
      customerServiceEmail: ['', [Validators.email]],
      countryId: [null, Validators.required],
      governateNameEn: ['', Validators.required],
      isActive: [false, [Validators.required]],
    });
  }
  getCountries() {
    return this.service.getCountries().subscribe((items) => {
      this.countries = items.response.countries;
    });
  }
  get f() {
    return this.governmentForm.controls;
  }
  backToList() {
    this.router.navigate(['data/governorates-list']);
  }
  onSubmit() {
    this.submitted = true;
    this.governmentForm.markAllAsTouched();

    if (this.governmentForm.invalid) {
      this.responseFailed('Please Fill The Form');
      return;
    }

    let segment = this.governmentForm.value;
    if (this.GoverId) {
      this.edit(segment);
    } else {
      this.create(segment);
    }
  }
  create(agesegment) {
    this.service.createGover(agesegment).subscribe(
      (res) => {
        this.responseSuccess();
      },
      (err) => {
        this.responseFailed(err.message);
      }
    );
  }
  edit(agesegment) {
    this.service.updateGover(agesegment, this.GoverId).subscribe(
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
      'Governorate has been created successfully',
      'SUCCESS OPERATION'
    );
    this.router.navigate(['data/governorates-list']);
    this.submitted = false;
  }

  responseFailed(err) {
    this.notify.error(err, 'FAILED OPERATION');
    this.submitted = false;
  }
}
