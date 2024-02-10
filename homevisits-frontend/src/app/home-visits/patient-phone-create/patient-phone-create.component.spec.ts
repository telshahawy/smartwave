import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PatientPhoneCreateComponent } from './patient-phone-create.component';

describe('PatientPhoneCreateComponent', () => {
  let component: PatientPhoneCreateComponent;
  let fixture: ComponentFixture<PatientPhoneCreateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PatientPhoneCreateComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PatientPhoneCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
