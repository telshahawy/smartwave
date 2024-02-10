import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserPatientCreateComponent } from './user-patient-create.component';

describe('UserPatientCreateComponent', () => {
  let component: UserPatientCreateComponent;
  let fixture: ComponentFixture<UserPatientCreateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UserPatientCreateComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UserPatientCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
