import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BorrowingRecordSearchComponent } from './borrowing-record-search-component';

describe('BorrowingRecordSearchComponent', () => {
  let component: BorrowingRecordSearchComponent;
  let fixture: ComponentFixture<BorrowingRecordSearchComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BorrowingRecordSearchComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(BorrowingRecordSearchComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
