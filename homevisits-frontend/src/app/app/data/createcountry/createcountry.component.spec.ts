import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreatecountryComponent } from './createcountry.component';

describe('CreatecountryComponent', () => {
  let component: CreatecountryComponent;
  let fixture: ComponentFixture<CreatecountryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreatecountryComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CreatecountryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
