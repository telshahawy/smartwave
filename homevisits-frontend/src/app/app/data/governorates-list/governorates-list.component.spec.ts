import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GovernoratesListComponent } from './governorates-list.component';

describe('GovernoratesListComponent', () => {
  let component: GovernoratesListComponent;
  let fixture: ComponentFixture<GovernoratesListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GovernoratesListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GovernoratesListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
