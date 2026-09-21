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
    let searchReq:BorrowingRecordSearch={
      sortDirection: '',
      sortColumn: '',
      pageSize: 10,
      pageNumber: 1,
      userName: '',
      title: '',
      genere: '',
      fromBorrowingDate: '',
      toBorrowingDate: '',
      fromReturnDate: '',
      toReturnDate: '',
      fromActualReturnDate: '',
      toActualReturnDate: ''
    };
   this.paginatedRecords=
   this.borrowingService.getBorrowingRecords(searchReq)
  //  .subscribe(
  //   {
  //   next:(res:PaginatedList< BorrowingRecordRes>)=>{
  //     console.log("response",res);
  //   },
  //   error:(err:HttpErrorResponse)=>{
  //     let failure=err.error as Failure;
  //     console.log(failure);
  //   }
  //  }
  // );
  }

}
