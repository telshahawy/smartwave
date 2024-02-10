import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserPatientAddressCreateComponent } from './user-patient-address-create.component';

describe('UserPatientAddressCreateComponent', () => {
  let component: UserPatientAddressCreateComponent;
  let fixture: ComponentFixture<UserPatientAddressCreateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UserPatientAddressCreateComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UserPatientAddressCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
