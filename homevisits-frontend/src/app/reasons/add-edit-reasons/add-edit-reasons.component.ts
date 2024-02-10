import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ClientService } from 'src/app/core/data-services/client.service';
import { NotifyService } from 'src/app/core/data-services/notify.service';
import { ReasonsType } from 'src/app/core/models/models';
import { BaseComponent } from 'src/app/core/permissions/base.component';
import { ActionsEnum } from 'src/app/core/permissions/models/actions';
import { PagesEnum } from 'src/app/core/permissions/models/pages';

@Component({
  selector: 'app-add-edit-reasons',
  templateUrl: './add-edit-reasons.component.html',
  styleUrls: ['./add-edit-reasons.component.css'],
})
export class AddEditReasonsComponent extends BaseComponent implements OnInit {
  userData: any;
  reasonForm: FormGroup;
  visitId;
  submitted = false;

  selectedCountryId: string;
  reasonId;
  reasonsActions;
  reasons = [
    { name: 'Cancel Visit Type Action', id: '4' },
    { name: 'Reject visit Type Action', id: '8' },
    { name: 'request second visit Type Action', id: '9' },
    { name: 'Reassign visit Type Action', id: '10' },
  ];

  constructor(
    public router: Router,
    private route: ActivatedRoute,
    private service: ClientService,
    public notify: NotifyService,
    private fb: FormBuilder
  ) {
    super(PagesEnum.SystemConfiguration, ActionsEnum.View, router, notify);
  }
  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      this.visitId = params.visitId;
      this.reasonId = params.reasonId;
      if (this.reasonId) {
        this.getById();
      }
      this.getReasonName();
    });
    this.getReasonActions();
    this.initForm();
  }
  getById() {
    this.service.reasonsService.getById(this.reasonId).subscribe((res) => {
      this.reasonForm.patchValue(res.response);
    });
  }

  initForm() {
    this.reasonForm = this.fb.group({
      reasonName: ['', Validators.required],
      visitTypeActionId: [+this.visitId],
      reasonActionId: this.rejectCase(),
      isActive: [false],
    });
  }

  rejectCase() {
    if (this.visitId == 8) {
      return ['', Validators.required];
    }
    return [null];
  }

  getReasonActions() {
    const newLocal = this.service.reasonsService
      .getActionReasons()
      .subscribe((res) => {
        this.reasonsActions = res.response;
      });
  }
  get f() {
    return this.reasonForm.controls;
  }

  backToList() {
    this.router.navigate(['reasons/list', this.visitId || 4]);
  }
  getReasonPage() {
    if (this.visitId == 4) {
      return PagesEnum.CancellationReasons;
    } else if (this.visitId == 8) {
      return PagesEnum.RejectReasons;
    } else if (this.visitId == 9) {
      return PagesEnum.RequestSecondVisitReasons;
    } else if (this.visitId == 10) {
      return PagesEnum.ReAssignReasons;
    }
  }
  getReasonName() {
    if (this.visitId == 4) {
      return 'Cancel Visit  ';
    } else if (this.visitId == 8) {
      return 'Reject Visit  ';
    } else if (this.visitId == 9) {
      return 'Request Visit  ';
    } else if (this.visitId == 10) {
      return 'Reassign Visit  ';
    }
  }
  onSubmit() {
    this.submitted = true;
    this.reasonForm.markAllAsTouched();

    if (this.reasonForm.invalid) {
      this.respondFaild('Please Fill The Form');
      return;
    }
    let ReasonDto = this.reasonForm.value;

    if (!this.reasonId) {
      this.create(ReasonDto);
    } else {
      this.edit(ReasonDto);
    }
  }
  create(ReasonDto) {
    this.service.reasonsService.create(ReasonDto).subscribe(
      (res) => {
        this.respondSuccess();
      },
      (err) => {
        this.respondFaild(err.message);
      }
    );
  }
  edit(ReasonDto) {
    this.service.reasonsService.edit(ReasonDto, this.reasonId).subscribe(
      (res) => {
        this.respondSuccess();
      },
      (err) => {
        this.respondFaild(err.message);
      }
    );
  }
  respondSuccess() {
    this.notify.success(
      'Reason has been Updated successfully',
      'SUCCESS OPERATION'
    );
    this.router.navigate(['reasons/list', this.visitId || 4]);
    this.submitted = false;
  }
  respondFaild(err) {
    this.notify.error(err, 'FAILED OPERATION');
    this.submitted = false;
  }
}
