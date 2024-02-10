import {
  AbstractControl,
  FormArray,
  FormControl,
  FormGroup,
  ValidationErrors,
} from '@angular/forms';
import { Router } from '@angular/router';
import { NotifyService } from 'src/app/core/data-services/notify.service';
import { BaseComponent } from 'src/app/core/permissions/base.component';
import { ActionsEnum } from 'src/app/core/permissions/models/actions';
import { PagesEnum } from 'src/app/core/permissions/models/pages';

export abstract class FormManage extends BaseComponent {
  private form: FormGroup;

  constructor(page: PagesEnum, action: ActionsEnum, public router: Router, public notify: NotifyService) {
    super(page, action, router, notify);
  }

  get isFormValid() {
    this.markAllFeildsTouched();
    return this.form.valid;
  }

  get FormValue() {
    return this.form.value;
  }

  setForm(form) {
    this.form = form;
  }

  markAllFeildsTouched() {
    Object.keys(this.form.controls).forEach((key) => {
      if (key !== 'items') {
        this.form.controls[key].markAsTouched();
      }
    });
  }

  isFieldValid(
    ControlName: string,
    controlIndex?: number,
    formArrayName?: string
  ) {
    if (formArrayName) {
      const formGroup = (this.form.get(formArrayName) as FormArray).controls[
        controlIndex
      ] as FormGroup;
      return (
        formGroup.controls[ControlName].touched &&
        formGroup.controls[ControlName].invalid
      );
    } else {
      return (
        this.form.controls[ControlName].touched &&
        this.form.controls[ControlName].invalid
      );
    }
  }

  setContollerValue(name: string, value: any) {
    this.form.controls[name].setValue(value);
  }

  getFormValidationErrors() {
    Object.keys(this.form.controls).forEach((key) => {
      const controlErrors: ValidationErrors = this.form.get(key).errors;
      if (controlErrors != null) {
        Object.keys(controlErrors).forEach((keyError) => {
          console.log(
            'Key control: ' + key + ', keyError: ' + keyError + ', err value: ',
            controlErrors[keyError]
          );
        });
      }
    });
  }

  resetForm() {
    this.form.reset();
  }

  setFormErrors(errors) {
    Object.keys(errors).forEach((error) => {
      if (this.form.controls[error]) {
        this.form.controls[error].setErrors({ incorrect: true });
      }
    });
  }

  /**
   * set data to the form
   * @param data
   */
  setDataToForm(data) {
    Object.keys(data).forEach((key) => {
      if (this.form.controls[key]) {
        this.setContollerValue(key, data[key]);
      }
    });
  }

  addControllerTOTheForm(name, data, validators = []) {
    this.form.addControl(name, new FormControl(data, validators));
  }

  getControllerValue(name) {
    if (this.form.controls[name]) {
      return this.form.controls[name].value;
    }
  }

  getFormFieldErrors(name) {
    const controlErrors: ValidationErrors = this.form.controls[name].errors;
    if (controlErrors != null) {
      Object.keys(controlErrors).forEach((keyError) => {
        console.log(
          'Key control: ' + name + ', keyError: ' + keyError + ', err value: ',
          controlErrors[keyError]
        );
      });
    }
    return controlErrors;
  }

  passwordValidator(
    passwordControlName: string,
    errorKey: string,
    confirmPassword: AbstractControl
  ) {
    if (confirmPassword.parent) {
      const parentForm = confirmPassword.parent as FormGroup;

      if (confirmPassword.value !== parentForm.get(passwordControlName).value) {
        return { [errorKey]: true };
      } else {
        return null;
      }
    }

    return null;
  }

  //  fromToDate(fromDateField: string, toDateField: string, errorName: string = 'fromToDate') {
  //         return (formGroup: AbstractControl): { [key: string]: boolean } | null => {
  //             const fromDate = formGroup.get(fromDateField).value;
  //             const toDate = formGroup.get(toDateField).value;
  //            // Ausing the fromDate and toDate are numbers. In not convert them first after null check
  //             if ((fromDate !== null && toDate !== null) && fromDate > toDate) {
  //                 return {[errorName]: true};
  //             }
  //             return null;
  //         };

  // }
  // custom validator to check that two fields match
  MustMatch(controlName: string, matchingControlName: string) {
    return (formGroup: FormGroup) => {
      const control = formGroup.controls[controlName];
      const matchingControl = formGroup.controls[matchingControlName];

      if (matchingControl.errors && !matchingControl.errors.mustMatch) {
        // return if another validator has already found an error on the matchingControl
        return;
      }

      // set error on matchingControl if validation fails
      if (control.value !== matchingControl.value) {
        matchingControl.setErrors({ mustMatch: true });
      } else {
        matchingControl.setErrors(null);
      }
    };
  }

  compareTwoTimes(startTimeControl, endTimeControl) {
    return (formGroup: FormGroup) => {
      const startTime = formGroup.controls[startTimeControl];
      const endTime = formGroup.controls[endTimeControl];
      // let start = new Date();
      // let time;
      // time = formGroup.controls[startTimeControl].value.split(":");
      // start.setHours((parseInt(time[0]) - 1 + 24) % 24);
      // start.setMinutes(parseInt(time[1]));

      // let end = new Date();
      // time = formGroup.controls[endTimeControl].value.split(":");
      // end.setHours((parseInt(time[0]) - 1 + 24) % 24);
      // end.setMinutes(parseInt(time[1]));
      if (startTime.value >= endTime.value) {
        endTime.setErrors({ invalidEndTime: true });
      } else {
        endTime.setErrors(null);
      }
    };
  }

  compareTwoDates(startDate, EndDate) {
    return (formGroup: FormGroup) => {
      const firstDate = formGroup.controls[startDate];
      const LastDate = formGroup.controls[EndDate];

      if (firstDate.value >= LastDate.value) {
        LastDate.setErrors({ invalidLastDate: true });
      } else {
        LastDate.setErrors(null);
      }
    };
  }
  getCurrentDate(day) {
    return (formGroup: FormGroup) => {
      const currentDate = new Date().toJSON().slice(0, 10).replace(/-/g, '-');
      const dayControls = formGroup.controls[day];
      if (dayControls.value < currentDate) {
        dayControls.setErrors({ invalidDate: true });
      } else {
        dayControls.setErrors(null);
      }
    };
  }
}
