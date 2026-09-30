import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReservationDetailComponent } from './reservation-detail-component';

describe('ReservationDetailComponent', () => {
  let component: ReservationDetailComponent;
  let fixture: ComponentFixture<ReservationDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ReservationDetailComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(ReservationDetailComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
