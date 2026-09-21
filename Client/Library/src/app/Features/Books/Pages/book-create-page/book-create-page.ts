import { Component, OnInit } from '@angular/core';
import { BookCreateComponent } from '../../Components/book-create-component/book-create-component';
import { BookReq } from '../../Models/BookReq';
import { BookService } from '../../Services/BookService';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { Failure } from '../../../../Global/Failure';
import { BookRes } from '../../Models/BookRes';
import { observableToBeFn } from 'rxjs/internal/testing/TestScheduler';
import { Observable } from 'rxjs';
import { asyncWrapProviders } from 'async_hooks';
import { AsyncPipe } from '@angular/common';
import { CopyRes } from '../../Models/CopyRes';

@Component({
  selector: 'app-book-create-page',
  imports: [BookCreateComponent,AsyncPipe],
  templateUrl: './book-create-page.html',
  styleUrl: './book-create-page.css',
})
export class BookCreatePage implements OnInit{
  copy?:Observable< CopyRes>;
  constructor(private bookService:BookService,private route:ActivatedRoute,private router:Router) {
  }
  ngOnInit(): void {
    debugger;
    if(this.CopyId){
this.copy= this.bookService.GetCopy(this.CopyId)

  }}
SaveBook(req: BookReq) {
if(this.CopyId==="0")
  this.Createbook(req);
else
  this.UpdateBook(req);
}
  UpdateBook(req: BookReq) {
    this.bookService.UpdateBook(req,this.CopyId).subscribe({
      next:()=>{this.router.navigate(['/BookPage','BookDetailPage',this.CopyId])},
      error:(err:HttpErrorResponse)=>{
        let failure=err.error as Failure ;
        console.log("fail",failure)
      }
    });
  }
  Createbook(req: BookReq) {
    
   this.bookService.CreateBook(req).subscribe({
    next:(res:BookRes)=>{
this.router.navigate(['/BookPage','BookDetailPage',res.id]);
    },
    error:(err:HttpErrorResponse)=>{
      const failure=err.error as Failure;
      console.log(failure.status,failure.title);
    }
   });
  }
get CopyId(){return this.route.snapshot.paramMap.get('id')??"0"}

}
