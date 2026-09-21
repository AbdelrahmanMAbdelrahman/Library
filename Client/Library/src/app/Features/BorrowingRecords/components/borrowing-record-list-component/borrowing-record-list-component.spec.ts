import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BorrowingRecordListComponent } from './borrowing-record-list-component';

describe('BorrowingRecordListComponent', () => {
  let component: BorrowingRecordListComponent;
  let fixture: ComponentFixture<BorrowingRecordListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BorrowingRecordListComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(BorrowingRecordListComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
