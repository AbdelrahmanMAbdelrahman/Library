import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BorrowingRecordListPage } from './borrowing-record-list-page';

describe('BorrowingRecordListPage', () => {
  let component: BorrowingRecordListPage;
  let fixture: ComponentFixture<BorrowingRecordListPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BorrowingRecordListPage],
    }).compileComponents();

    fixture = TestBed.createComponent(BorrowingRecordListPage);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
