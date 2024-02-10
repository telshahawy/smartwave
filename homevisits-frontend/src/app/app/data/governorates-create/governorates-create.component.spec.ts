import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GovernoratesCreateComponent } from './governorates-create.component';

describe('GovernoratesCreateComponent', () => {
  let component: GovernoratesCreateComponent;
  let fixture: ComponentFixture<GovernoratesCreateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GovernoratesCreateComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GovernoratesCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
