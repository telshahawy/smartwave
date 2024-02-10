import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowChemistScheduleComponent } from './show-chemist-schedule.component';

describe('ShowChemistScheduleComponent', () => {
  let component: ShowChemistScheduleComponent;
  let fixture: ComponentFixture<ShowChemistScheduleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShowChemistScheduleComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ShowChemistScheduleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
