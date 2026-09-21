import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BorrowingRecordPage } from './borrowing-record-page';

describe('BorrowingRecordPage', () => {
  let component: BorrowingRecordPage;
  let fixture: ComponentFixture<BorrowingRecordPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BorrowingRecordPage],
    }).compileComponents();

    fixture = TestBed.createComponent(BorrowingRecordPage);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
