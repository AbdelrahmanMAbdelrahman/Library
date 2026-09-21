import { Component, Input, OnInit } from '@angular/core';
import { PaginatedList } from '../../../../Global/PaginatedList';
import { BorrowingRecordRes } from '../../Models/BorrowingRecordRes';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-borrowing-record-list-component',
  imports: [CommonModule, RouterLink],
  templateUrl: './borrowing-record-list-component.html',
  styleUrl: './borrowing-record-list-component.css',
})
export class BorrowingRecordListComponent  {
  @Input()PaginatedBorrowingRecords?:PaginatedList<BorrowingRecordRes>;

}
