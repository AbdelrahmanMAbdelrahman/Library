import { Component, OnInit } from '@angular/core';
import { BorrowingRecordDetailComponent } from '../../components/borrowing-record-detail-component/borrowing-record-detail-component';
import { BorrowingRecordRes } from '../../Models/BorrowingRecordRes';
import { Observable } from 'rxjs';
import { BorrowingRecordService } from '../../Servicies/BorrowingRecordService';
import { ActivatedRoute, Router } from '@angular/router';
import { BookService } from '../../../Books/Services/BookService';
import { AsyncPipe } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';
import { Failure } from '../../../../Global/Failure';
import { fail } from 'assert';

@Component({
  selector: 'app-borrowing-record-detail-page',
  imports: [BorrowingRecordDetailComponent,AsyncPipe],
  templateUrl: './borrowing-record-detail-page.html',
  styleUrl: './borrowing-record-detail-page.css',
})
export class BorrowingRecordDetailPage implements OnInit{
  record?:Observable< BorrowingRecordRes>;
 
  constructor(private recordService:BorrowingRecordService,private bookService:BookService,
    private router:Router,private route:ActivatedRoute) {
    
  }
  ngOnInit(): void {
    debugger;
    if(this.Id)
    this.record=this.recordService.getBorrowingRecord(this.Id);
  }
  ReturnCopy(copyId:string) {
  this.bookService.ReturnCopy(copyId)
  .subscribe({
    next:()=>{
this.router.navigate(['/BookPage','BookListPage']);
    },
    error:(err:HttpErrorResponse)=>{
      let failure=err.error as Failure;
      console.log(failure);
    }
  });
  }
  get Id(){
    return this.route.snapshot.paramMap.get('id');
  }
}
