import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ClientService } from 'src/app/core/data-services/client.service';
import { NotifyService } from 'src/app/core/data-services/notify.service';
import { RolesSearchCriteria } from 'src/app/core/models/models';
import { BaseComponent } from 'src/app/core/permissions/base.component';
import { ActionsEnum } from 'src/app/core/permissions/models/actions';
import { PagesEnum } from 'src/app/core/permissions/models/pages';
@Component({
  selector: 'app-system-add-edit',
  templateUrl: './system-add-edit.component.html',
  styleUrls: ['./system-add-edit.component.css'],
})
export class SystemAddEditComponent extends BaseComponent implements OnInit {
  systemForm: FormGroup;
  submitted = false;
  paramsPage: PagesEnum = PagesEnum.SystemParameters;
  isEditedMode = true;
  visitApprovel = [
    {
      id: 'chemist',
      name: 'Chemist ',
    },
    {
      id: 'callcenter',
      name: 'Call Center',
    },
    {
      id: 'Both',
      name: 'Both',
    },
  ];

  countries = [];
  governorates = [];
  criteria;
  file;

  fileSize;
  isFileLoading = false;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    public router: Router,
    private service: ClientService,
    public notify: NotifyService
  ) {
    super(PagesEnum.SystemParameters,ActionsEnum.View,router,notify)
  }
  ngOnInit(): void {
    this.initForm();
    this.getGovernorates();
    this.criteria = new RolesSearchCriteria();

    this.getById();
    this.getCountries();
  }

  getCountries() {
    return this.service.getCountries().subscribe((items) => {
      this.countries = items.response.countries;
    });
  }

  getGovernorates() {
    return this.service.searchgovers(this.criteria).subscribe((items) => {
      this.governorates = items.response.governats;
    });
  }

  backToList() {
    this.router.navigate(['data/system-list']);
  }
  initForm() {
    this.systemForm = this.fb.group(
      {
        estimatedVisitDurationInMin: [
          null,
          [
            Validators.required,
            Validators.pattern('^[0-9]*$'),
            Validators.max(999),
            Validators.min(0),
          ],
        ],
        nextReserveHomevisitInDay: [
          null,
          [
            Validators.required,
            Validators.pattern('^[0-9]*$'),
            Validators.max(99),
            Validators.min(0),
          ],
        ],
        routingSlotDurationInMin: [
          null,
          [
            Validators.required,
            Validators.pattern('^[0-9]*$'),
            Validators.max(999),
            Validators.min(0),
          ],
        ],
        visitApprovalBy: [null, [Validators.required]],
        visitCancelBy: [null, [Validators.required]],
        defaultCountryId: [null],
        defaultGovernorateId: [null],
        isSendPatientTimeConfirmation: [false],
        isOptimizezonebefore: [false],
        optimizezonebeforeInMin: [
          0,
          [
            Validators.pattern('^[0-9]*$'),
            Validators.max(999),
            Validators.min(0),
          ],
        ],
        callCenterNumber: [
          null,
          [
            Validators.pattern(/^[\d\(\)\-+]+$/),
            Validators.minLength(5),
            Validators.maxLength(20),
          ],
        ],
        whatsappBusinessLink: [null],
        precautionsFile: [''],
        fileName: [''],
      },
      {
        validators: this.isOptionBeforeTrue(
          'isOptimizezonebefore',
          'optimizezonebeforeInMin'
        ),
      }
    );
  }
  get f() {
    return this.systemForm?.controls;
  }

  isOptionBeforeTrue(
    isOptimizezonebefore: string,
    optimizezonebeforeInMin: string
  ) {
    return (formGroup: FormGroup) => {
      const isOptimizezonebeforeControl =
        formGroup.controls[isOptimizezonebefore];
      const optimizezonebeforeInMinControl =
        formGroup.controls[optimizezonebeforeInMin];

      if (isOptimizezonebeforeControl.value == true) {
        if (optimizezonebeforeInMinControl.value) {
          optimizezonebeforeInMinControl.setErrors(null);
        } else {
          optimizezonebeforeInMinControl.setErrors({ required: true });
        }
      } else {
        optimizezonebeforeInMinControl.setErrors(null);
      }
    };
  }

  getFiles(event) {
    if (event.target.files && event.target.files[0]) {
      const formData = new FormData();
      let file = event.target.files[0];
      this.isFileLoading = true;

      formData.append('precautionsFile', file);
      console.log(file);
      if (file) {
        let fileSize = 0;
        if (file.size > 1024 * 1024)
          this.fileSize =
            (Math.round((file.size * 100) / (1024 * 1024)) / 100).toString() +
            'MB';
        if (fileSize > 10) {
          this.responseFailed('Maximum File Size 10 MB');
        } else if (file.type != 'application/pdf') {
          this.responseFailed(' File Type Should Be PDF');
        } else if (fileSize <= 10 && file.type == 'application/pdf') {
          this.fileSize =
            (Math.round((file.size * 100) / 1024) / 100).toString() + 'KB';
          this.service.systemParameters.upload(formData).subscribe((res) => {
            this.systemForm.controls['precautionsFile'].setValue(res.response);
            this.systemForm.controls['fileName'].setValue(file.name);
            this.file = file;
            this.isFileLoading = false;
          });
        }
      }
    }
  }

  removeFile() {
    this.systemForm.controls['precautionsFile'].setValue('');
    this.systemForm.controls['fileName'].setValue('');
    this.file = '';
  }
  getById() {
    this.service.systemParameters.getById().subscribe((res) => {
      console.log();
      if (res.response.systemParameters != null) {
        this.systemForm.patchValue(res.response.systemParameters);
        this.file = {
          name: res.response.systemParameters.fileName,
          url: res.response.systemParameters.precautionsFile,
        };
        this.isEditedMode = true;
      } else {
        this.isEditedMode = false;
      }
    });
  }
  onSubmit() {
    this.submitted = true;
    this.systemForm.markAllAsTouched();
    if (this.systemForm.invalid) {
      this.responseFailed('Please Fill The Form');
      return;
    }

    let systemParams = this.systemForm.value;
    console.log(systemParams.defaultCountryId);

    if (
      systemParams.defaultCountryId == null ||
      systemParams.defaultCountryId == 'null'
    ) {
      delete systemParams.defaultCountryId;
      console.log('cu');
    }
    if (
      systemParams.defaultGovernorateId == null ||
      systemParams.defaultGovernorateId == 'null'
    ) {
      delete systemParams.defaultGovernorateId;
      console.log('gov');
    }

    if (this.isEditedMode == true) {
      console.log(systemParams);

      this.edit(systemParams);
    } else {
      this.create(systemParams);
    }
  }
  create(systemParams) {
    this.service.systemParameters.create(systemParams).subscribe(
      (res) => {
        this.responseSuccess();
        this.isEditedMode = true;
      },
      (err) => {
        this.responseFailed(err.message);
      }
    );
  }
  edit(systemParams) {
    this.service.systemParameters.edit(systemParams).subscribe(
      (res) => {
        this.responseSuccess();
        this.getById();
      },
      (err) => {
        this.responseFailed(err?.error?.message);
      }
    );
  }
  responseSuccess() {
    this.notify.success(
      'System Parameters has been created successfully',
      'SUCCESS OPERATION'
    );
    this.submitted = false;
  }

  responseFailed(err) {
    this.notify.error(err, 'FAILED OPERATION');
    this.submitted = false;
  }
}
