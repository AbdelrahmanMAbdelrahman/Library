import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReservationListPage } from './reservation-list-page';

describe('ReservationListPage', () => {
  let component: ReservationListPage;
  let fixture: ComponentFixture<ReservationListPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ReservationListPage],
    }).compileComponents();

    fixture = TestBed.createComponent(ReservationListPage);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
