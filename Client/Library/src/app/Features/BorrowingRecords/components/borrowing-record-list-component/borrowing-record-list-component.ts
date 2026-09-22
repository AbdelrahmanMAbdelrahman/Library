import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { PaginatedList } from '../../../../Global/PaginatedList';
import { BorrowingRecordRes } from '../../Models/BorrowingRecordRes';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { BorrowingRecordSearch } from '../../Models/BorrowingRecordSearch';
import { DefaultSettings } from '../../../../Global/Consts/DefaultConsts';


@Component({
  selector: 'app-borrowing-record-list-component',
  imports: [CommonModule, RouterLink],
  templateUrl: './borrowing-record-list-component.html',
  styleUrl: './borrowing-record-list-component.css',
})
export class BorrowingRecordListComponent  {
  @Input()PaginatedBorrowingRecords?:PaginatedList<BorrowingRecordRes>;
  @Output()OnPaginate=new EventEmitter<BorrowingRecordSearch>();
  
  getPrev() {
  let searchReq=this.getRecordSearch(); 
  --searchReq.pageNumber;
  this.OnPaginate.emit(searchReq);
}
  getRecordSearch() {
    let searchReq:BorrowingRecordSearch={
      sortDirection: '',
      sortColumn: '',
      pageSize: DefaultSettings.PageSize,
      pageNumber: this.PaginatedBorrowingRecords?.pageNumber??1,
      userName: '',
      title: '',
      genere: '',
      fromBorrowingDate: '',
      toBorrowingDate: '',
      fromDueDate: '',
      toDueDate: '',
      fromActualReturnDate: '',
      toActualReturnDate: ''
    }
    return searchReq;
  }
  getNext() {
    let searchReq=this.getRecordSearch();
    ++searchReq.pageNumber;
    this.OnPaginate.emit(searchReq);
  }
  sort(event: Event) {
    debugger;
 let select=event.target as HTMLSelectElement;
 let searchReq=this.getRecordSearch(); 
 searchReq.sortColumn=select.value;
 this.OnPaginate.emit(searchReq);
  }

}
