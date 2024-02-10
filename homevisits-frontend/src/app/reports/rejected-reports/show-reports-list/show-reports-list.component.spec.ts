import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowReportsListComponent } from './show-reports-list.component';

describe('ShowReportsListComponent', () => {
  let component: ShowReportsListComponent;
  let fixture: ComponentFixture<ShowReportsListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShowReportsListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ShowReportsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
