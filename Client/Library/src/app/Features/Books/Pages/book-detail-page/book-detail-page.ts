import { Component, OnInit } from '@angular/core';
import { BookDetailComponent } from '../../Components/book-detail-component/book-detail-component';
import { Observable } from 'rxjs';
import { BookRes } from '../../Models/BookRes';
import { BookService } from '../../Services/BookService';
import { ActivatedRoute, Router } from '@angular/router';
import { AsyncPipe } from '@angular/common';


@Component({
  selector: 'app-book-detail-page',
  imports: [BookDetailComponent,AsyncPipe],
  templateUrl: './book-detail-page.html',
  styleUrl: './book-detail-page.css',
})
export class BookDetailPage implements OnInit {
  book?:Observable<BookRes>;

  constructor(private bookService:BookService,private route:ActivatedRoute) {
  }
  ngOnInit(): void {
    this.book=this.bookService.GetBook(this.Id);
  }
get Id(){
return this.route.snapshot.paramMap.get("id")??"";
}
}
