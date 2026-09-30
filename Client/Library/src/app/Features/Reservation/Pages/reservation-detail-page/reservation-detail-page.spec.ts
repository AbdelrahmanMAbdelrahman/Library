import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReservationDetailPage } from './reservation-detail-page';

describe('ReservationDetailPage', () => {
  let component: ReservationDetailPage;
  let fixture: ComponentFixture<ReservationDetailPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ReservationDetailPage],
    }).compileComponents();

    fixture = TestBed.createComponent(ReservationDetailPage);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
