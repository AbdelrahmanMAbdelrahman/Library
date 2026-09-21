import { Component, OnInit } from '@angular/core';
import { BookDetailComponent } from '../../Components/book-detail-component/book-detail-component';
import { Observable } from 'rxjs';
import { BookRes } from '../../Models/BookRes';
import { BookService } from '../../Services/BookService';
import { ActivatedRoute, Router } from '@angular/router';
import { AsyncPipe } from '@angular/common';
import { BorrowingRecordService } from '../../../BorrowingRecords/Servicies/BorrowingRecordService';
import { BorrowingRecordReq } from '../../../BorrowingRecords/Models/BorrowingRecordReq';

import { HttpErrorResponse } from '@angular/common/http';
import { Failure } from '../../../../Global/Failure';
import { CopyRes } from '../../Models/CopyRes';
import { BorrowingRecordRes } from '../../../BorrowingRecords/Models/BorrowingRecordRes';
import { fail } from 'assert';
import { observableToBeFn } from 'rxjs/internal/testing/TestScheduler';


@Component({
  selector: 'app-book-detail-page',
  imports: [BookDetailComponent,AsyncPipe],
  templateUrl: './book-detail-page.html',
  styleUrl: './book-detail-page.css',
})
export class BookDetailPage implements OnInit {
  copy?:Observable< CopyRes>;
  
  constructor(private bookService:BookService,
    private route:ActivatedRoute,
    private borrowingService:BorrowingRecordService,
  private router:Router) {}
    
    ngOnInit(): void {
      this.getBook();
      
    }
  getBook() {
   this.copy= this.bookService.GetCopy(this.Id);
  }
    borrowBook() {
     debugger;
      let req:BorrowingRecordReq={
        borrowingDate: new Date().toISOString(),
        copyId:this.Id
      }
    this.borrowingService.createBorrowingRecord(req).subscribe({
      next:(record:BorrowingRecordRes)=>{
this.router.navigate(['/BorrowingRecordPage','BorrowingRecordListPage'])
      },
      error:(err:HttpErrorResponse)=>{
        let failure=err.error as Failure;
        console.log(failure);
      }
    });
    }
get Id(){
return this.route.snapshot.paramMap.get("id")??"";
}
}
