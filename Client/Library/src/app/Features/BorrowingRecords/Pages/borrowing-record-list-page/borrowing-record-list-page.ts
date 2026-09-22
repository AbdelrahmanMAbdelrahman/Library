import { Component, OnInit } from '@angular/core';
import { BorrowingRecordSearchComponent } from '../../components/borrowing-record-search-component/borrowing-record-search-component';
import { BorrowingRecordListComponent } from '../../components/borrowing-record-list-component/borrowing-record-list-component';
import { Observable } from 'rxjs';
import { PaginatedList } from '../../../../Global/PaginatedList';
import { BorrowingRecordRes } from '../../Models/BorrowingRecordRes';
import { BorrowingRecordService } from '../../Servicies/BorrowingRecordService';
import { BorrowingRecordSearch } from '../../Models/BorrowingRecordSearch';
import { HttpErrorResponse } from '@angular/common/http';
import { Failure } from '../../../../Global/Failure';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-borrowing-record-list-page',
  imports: [BorrowingRecordSearchComponent, BorrowingRecordListComponent,AsyncPipe],
  templateUrl: './borrowing-record-list-page.html',
  styleUrl: './borrowing-record-list-page.css',
})
export class BorrowingRecordListPage implements OnInit{
  paginatedRecords?:Observable<PaginatedList<BorrowingRecordRes>>;
  constructor(private borrowingService:BorrowingRecordService) {}
  ngOnInit(): void {
    this.Search(null);
  }
  Search(record: BorrowingRecordSearch|null) {
    debugger;
    let searchReq:BorrowingRecordSearch={
      sortDirection: record?.sortDirection??'',
      sortColumn: record?.sortColumn??'',
      pageSize:record?.pageSize?? 10,
      pageNumber: record?.pageNumber??1,
      userName: record?.userName??'',
      title: record?.title??'',
      genere: record?.genere??'',
      fromBorrowingDate: record?.fromBorrowingDate??'',
      toBorrowingDate: record?.toBorrowingDate??'',
      fromDueDate: record?.fromDueDate??'',
      toDueDate: record?.toDueDate??'',
      fromActualReturnDate: record?.fromActualReturnDate??'',
      toActualReturnDate:record?.toActualReturnDate?? ''
    };
    this.paginatedRecords=
    this.borrowingService.getBorrowingRecords(searchReq);
  }
  
  Paginate(searchReq: BorrowingRecordSearch) {
  this.paginatedRecords=this.borrowingService.getBorrowingRecords(searchReq);
  }
}
