import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AgeSegmentListComponent } from './age-segment-list.component';

describe('AgeSegmentListComponent', () => {
  let component: AgeSegmentListComponent;
  let fixture: ComponentFixture<AgeSegmentListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AgeSegmentListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AgeSegmentListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
