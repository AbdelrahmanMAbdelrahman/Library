import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BorrowingRecordDetailPage } from './borrowing-record-detail-page';

describe('BorrowingRecordDetailPage', () => {
  let component: BorrowingRecordDetailPage;
  let fixture: ComponentFixture<BorrowingRecordDetailPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BorrowingRecordDetailPage],
    }).compileComponents();

    fixture = TestBed.createComponent(BorrowingRecordDetailPage);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
