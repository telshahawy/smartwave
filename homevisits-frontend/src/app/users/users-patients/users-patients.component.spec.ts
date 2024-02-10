import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UsersPatientsComponent } from './users-patients.component';

describe('UsersPatientsComponent', () => {
  let component: UsersPatientsComponent;
  let fixture: ComponentFixture<UsersPatientsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UsersPatientsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UsersPatientsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
