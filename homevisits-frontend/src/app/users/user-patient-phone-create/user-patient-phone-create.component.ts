import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { ClientService } from 'src/app/core/data-services/client.service';
import { NotifyService } from 'src/app/core/data-services/notify.service';
import { PatientPhoneCreateComponent } from 'src/app/home-visits/patient-phone-create/patient-phone-create.component';

@Component({
  selector: 'app-user-patient-phone-create',
  templateUrl: './user-patient-phone-create.component.html',
  styleUrls: ['./user-patient-phone-create.component.css']
})
export class UserPatientPhoneCreateComponent implements OnInit {
  patientId;
  phoneForm: FormGroup;
  submitted = false

  constructor(private fb: FormBuilder, private route: ActivatedRoute
    , private router: Router, private service: ClientService,
    @Inject(MAT_DIALOG_DATA) public data: any,public dialogRef: MatDialogRef<PatientPhoneCreateComponent>,
    private notify: NotifyService) {
    this.patientId= data 
  }
  ngOnInit(): void {
    this.initForm()
  }
  initForm() {
    this.phoneForm = this.fb.group({
      phone: ['', [Validators.required, Validators.minLength(8),
      Validators.maxLength(20),
      Validators.pattern('([+]|0)[0-9]{1,}')]],
      patientId: [this.patientId, Validators.required]
    })

  }
  get f() {
    return this.phoneForm.controls;
  }
  createPhoneNumber() {
    this.submitted = true
    this.phoneForm.markAllAsTouched()

console.log(this.phoneForm);

    if (this.phoneForm.invalid) {
      this.responseFailed('Please Fill The Form')

      return;
    }

    this.service.createPatientPhone(this.phoneForm.value).subscribe(res => {
      this.responseSuccess()
    },
      (err) => {

        this.responseFailed(err.message)
      })
  }
  responseSuccess() {
    this.notify.success('Phone Number has been created successfully',
      'SUCCESS OPERATION')
      this.dialogRef.close(this.phoneForm.value.phone)    // window.history.back();

    this.submitted = false


  }

  responseFailed(err) {
    this.notify.error(err, 'FAILED OPERATION');
    this.submitted = false
  }

}
