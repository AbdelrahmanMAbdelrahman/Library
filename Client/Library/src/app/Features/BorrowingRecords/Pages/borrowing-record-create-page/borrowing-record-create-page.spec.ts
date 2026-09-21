import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BorrowingRecordCreatePage } from './borrowing-record-create-page';

describe('BorrowingRecordCreatePage', () => {
  let component: BorrowingRecordCreatePage;
  let fixture: ComponentFixture<BorrowingRecordCreatePage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BorrowingRecordCreatePage],
    }).compileComponents();

    fixture = TestBed.createComponent(BorrowingRecordCreatePage);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
