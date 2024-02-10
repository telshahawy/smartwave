import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserPatientPhoneCreateComponent } from './user-patient-phone-create.component';

describe('UserPatientPhoneCreateComponent', () => {
  let component: UserPatientPhoneCreateComponent;
  let fixture: ComponentFixture<UserPatientPhoneCreateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UserPatientPhoneCreateComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UserPatientPhoneCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
