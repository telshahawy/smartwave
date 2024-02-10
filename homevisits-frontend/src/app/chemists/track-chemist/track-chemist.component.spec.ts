import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrackChemistComponent } from './track-chemist.component';

describe('TrackChemistComponent', () => {
  let component: TrackChemistComponent;
  let fixture: ComponentFixture<TrackChemistComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TrackChemistComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TrackChemistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
