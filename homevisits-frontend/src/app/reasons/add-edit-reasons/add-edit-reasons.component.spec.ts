import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddEditReasonsComponent } from './add-edit-reasons.component';

describe('AddEditReasonsComponent', () => {
  let component: AddEditReasonsComponent;
  let fixture: ComponentFixture<AddEditReasonsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddEditReasonsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddEditReasonsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
