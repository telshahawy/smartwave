import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditChemistsPermitsComponent } from './edit-chemists-permits.component';

describe('EditChemistsPermitsComponent', () => {
  let component: EditChemistsPermitsComponent;
  let fixture: ComponentFixture<EditChemistsPermitsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditChemistsPermitsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EditChemistsPermitsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
