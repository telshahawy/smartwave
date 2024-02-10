import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserPatientDataComponent } from './user-patient-data.component';

describe('UserPatientDataComponent', () => {
  let component: UserPatientDataComponent;
  let fixture: ComponentFixture<UserPatientDataComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UserPatientDataComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UserPatientDataComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
