import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChemistschedulesComponent } from './chemistschedules.component';

describe('ChemistschedulesComponent', () => {
  let component: ChemistschedulesComponent;
  let fixture: ComponentFixture<ChemistschedulesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ChemistschedulesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ChemistschedulesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
