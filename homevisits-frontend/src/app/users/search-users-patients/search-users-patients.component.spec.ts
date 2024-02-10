import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchUsersPatientsComponent } from './search-users-patients.component';

describe('SearchUsersPatientsComponent', () => {
  let component: SearchUsersPatientsComponent;
  let fixture: ComponentFixture<SearchUsersPatientsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SearchUsersPatientsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SearchUsersPatientsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
