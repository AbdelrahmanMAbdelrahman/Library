import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BorrowingRecordCreateComponent } from './borrowing-record-create-component';

describe('BorrowingRecordCreateComponent', () => {
  let component: BorrowingRecordCreateComponent;
  let fixture: ComponentFixture<BorrowingRecordCreateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BorrowingRecordCreateComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(BorrowingRecordCreateComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
