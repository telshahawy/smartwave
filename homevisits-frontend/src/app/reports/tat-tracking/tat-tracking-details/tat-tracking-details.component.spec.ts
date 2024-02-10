import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TatTrackingDetailsComponent } from './tat-tracking-details.component';

describe('TatTrackingDetailsComponent', () => {
  let component: TatTrackingDetailsComponent;
  let fixture: ComponentFixture<TatTrackingDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TatTrackingDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TatTrackingDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
