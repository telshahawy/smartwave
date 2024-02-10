import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateChemistsPermitsComponent } from './create-chemists-permits.component';

describe('CreateChemistsPermitsComponent', () => {
  let component: CreateChemistsPermitsComponent;
  let fixture: ComponentFixture<CreateChemistsPermitsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateChemistsPermitsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateChemistsPermitsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
