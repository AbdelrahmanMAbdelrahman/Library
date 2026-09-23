import { Component, EventEmitter, Input, Output } from '@angular/core';
import { BookRes } from '../../Models/BookRes';
import { RouterLink } from '@angular/router';
import { BorrowingRecordReq } from '../../../BorrowingRecords/Models/BorrowingRecordReq';
import { CopyRes } from '../../Models/CopyRes';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-book-detail-component',
  imports: [RouterLink,DatePipe],
  templateUrl: './book-detail-component.html',
  styleUrl: './book-detail-component.css',
})
export class BookDetailComponent {
  imageUrl:string="https://localhost:7010/api/File";
  @Input()copy?:CopyRes;
  @Output()OnSaveBorrowingRecord=new EventEmitter();
  handleImageError(event: Event) {
    let image=event.target as HTMLImageElement;
    image.onerror=null;
    image.src="/Images/book.jpg"
  }
  BorrowBook() {
   
  this.OnSaveBorrowingRecord.emit();
  }
}
