import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BorrowingRecordDetailComponent } from './borrowing-record-detail-component';

describe('BorrowingRecordDetailComponent', () => {
  let component: BorrowingRecordDetailComponent;
  let fixture: ComponentFixture<BorrowingRecordDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BorrowingRecordDetailComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(BorrowingRecordDetailComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
