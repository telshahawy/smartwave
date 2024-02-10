import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AgeSegmentAddEditComponent } from './age-segment-add-edit.component';

describe('AgeSegmentAddEditComponent', () => {
  let component: AgeSegmentAddEditComponent;
  let fixture: ComponentFixture<AgeSegmentAddEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AgeSegmentAddEditComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AgeSegmentAddEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
