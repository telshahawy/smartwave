import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ClientService } from 'src/app/core/data-services/client.service';
import { NotifyService } from 'src/app/core/data-services/notify.service';
import { BaseComponent } from 'src/app/core/permissions/base.component';
import { ActionsEnum } from 'src/app/core/permissions/models/actions';
import { PagesEnum } from 'src/app/core/permissions/models/pages';

@Component({
  selector: 'app-age-segment-add-edit',
  templateUrl: './age-segment-add-edit.component.html',
  styleUrls: ['./age-segment-add-edit.component.css'],
})
export class AgeSegmentAddEditComponent extends BaseComponent implements OnInit {
  AgeSegmentForm: FormGroup;
  submitted = false;
  segmentId;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    public router: Router,
    private service: ClientService,
    public notify: NotifyService
  ) {
    super(PagesEnum.AgeSegments,ActionsEnum.Create,router,notify)
  }
  ngOnInit(): void {
    this.segmentId = this.route.snapshot.params.id;
    this.initForm();
    if (this.segmentId) {
      this.getById(this.segmentId);
    }
  }
  backToList() {
    this.router.navigate(['data/agesegments-list']);
  }
  initForm() {
    this.AgeSegmentForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(2)]],
      ageFromDay: [
        '',
        [
          Validators.required,
          Validators.min(0),
          Validators.max(30),
          Validators.pattern('^[0-9]*$'),
        ],
      ],

      ageFromMonth: [
        '',
        [
          Validators.required,
          Validators.min(0),
          Validators.max(11),
          Validators.pattern('^[0-9]*$'),
        ],
      ],

      ageFromYear: [
        '',
        [
          Validators.required,
          Validators.min(0),
          Validators.max(150),
          Validators.pattern('^[0-9]*$'),
        ],
      ],

      ageToDay: [
        '',
        [
          Validators.required,
          Validators.min(0),
          Validators.max(30),
          Validators.pattern('^[0-9]*$'),
        ],
      ],

      ageToMonth: [
        '',
        [
          Validators.required,
          Validators.min(0),
          Validators.max(11),
          Validators.pattern('^[0-9]*$'),
        ],
      ],

      ageToYear: [
        '',
        [
          Validators.required,
          Validators.min(0),
          Validators.max(150),
          Validators.pattern('^[0-9]*$'),
        ],
      ],

      ageFromInclusive: [false, [Validators.required]],
      ageToInclusive: [false, [Validators.required]],
      isActive: [false, [Validators.required]],
      needExpert: [false, [Validators.required]],
    });
  }
  get f() {
    return this.AgeSegmentForm.controls;
  }

  getById(id) {
    this.service.ageSegmentsService.getById(id).subscribe((res) => {
      this.AgeSegmentForm.patchValue(res.response);
      console.log(this.AgeSegmentForm.value);
    });
  }
  onSubmit() {
    this.submitted = true;
    this.AgeSegmentForm.markAllAsTouched();

    if (this.AgeSegmentForm.invalid) {
      this.responseFailed('Please Fill The Form');

      return;
    }

    let segment = this.AgeSegmentForm.value;
    if (this.segmentId) {
      this.edit(segment);
    } else {
      this.create(segment);
    }
  }
  create(agesegment) {
    this.service.ageSegmentsService.create(agesegment).subscribe(
      (res) => {
        this.responseSuccess();
      },
      (err) => {
        this.responseFailed(err.message);
      }
    );
  }
  edit(agesegment) {
    this.service.ageSegmentsService.edit(agesegment, this.segmentId).subscribe(
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
      'Age Segment has been created successfully',
      'SUCCESS OPERATION'
    );
    this.router.navigate(['data/agesegments-list']);
    this.submitted = false;
  }

  responseFailed(err) {
    this.notify.error(err, 'FAILED OPERATION');
    this.submitted = false;
  }
}
