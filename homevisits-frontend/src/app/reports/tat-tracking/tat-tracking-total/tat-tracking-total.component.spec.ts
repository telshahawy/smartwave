import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TatTrackingTotalComponent } from './tat-tracking-total.component';

describe('TatTrackingTotalComponent', () => {
  let component: TatTrackingTotalComponent;
  let fixture: ComponentFixture<TatTrackingTotalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TatTrackingTotalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TatTrackingTotalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
