import { Component, EventEmitter, Input, Output } from '@angular/core';
import { BookDetailComponent } from '../../../Books/Components/book-detail-component/book-detail-component';
import { RouterLink } from '@angular/router';
import { CopyRes } from '../../../Books/Models/CopyRes';
import { BorrowingRecordRes } from '../../Models/BorrowingRecordRes';
import { DatePipe } from '@angular/common';


@Component({
  selector: 'app-borrowing-record-detail-component',
  imports: [BookDetailComponent, RouterLink,DatePipe],
  templateUrl: './borrowing-record-detail-component.html',
  styleUrl: './borrowing-record-detail-component.css',
})
export class BorrowingRecordDetailComponent {
  @Output() OnReturnCopy=new EventEmitter<string>;
  @Input() borrowingRecord?:BorrowingRecordRes;
ReturnCopy() {
this.OnReturnCopy.emit(this.borrowingRecord?.copy.id);
}
}
