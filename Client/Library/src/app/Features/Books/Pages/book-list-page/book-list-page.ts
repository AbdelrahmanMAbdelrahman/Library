import { Component, OnInit, ɵDEFER_BLOCK_CONFIG } from '@angular/core';
import { BookService } from '../../Services/BookService';
import { BookListComponent } from '../../Components/book-list-component/book-list-component';
import { Observable } from 'rxjs';
import { PaginatedList } from '../../../../Global/PaginatedList';
import { BookRes } from '../../Models/BookRes';
import { BookFilter } from '../../Models/BookFilter';
import { AsyncPipe } from '@angular/common';
import { BookSearch } from '../../Components/book-search/book-search';

@Component({
  selector: 'app-book-list-page',
  imports: [BookListComponent, AsyncPipe, BookSearch],
  templateUrl: './book-list-page.html',
  styleUrl: './book-list-page.css',
})
export class BookListPage implements OnInit {
  paginatedBooks?:Observable<PaginatedList<BookRes>>;
  constructor(private bookService:BookService) {}
  ngOnInit(): void {
    debugger;
    console.log("called");
let bookFilter:BookFilter={
  PageNumber: 1,
  PageSize: 10
}
     this.paginatedBooks=this.bookService.GetBooks(bookFilter);
  }

}
