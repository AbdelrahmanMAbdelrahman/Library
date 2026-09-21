import { Component, OnInit, ɵDEFER_BLOCK_CONFIG } from '@angular/core';
import { BookService } from '../../Services/BookService';
import { BookListComponent } from '../../Components/book-list-component/book-list-component';
import { Observable } from 'rxjs';
import { PaginatedList } from '../../../../Global/PaginatedList';
import { BookRes } from '../../Models/BookRes';
import { BookFilter } from '../../Models/BookFilter';
import { AsyncPipe } from '@angular/common';
import { BookSearch } from '../../Components/book-search/book-search';
import { CopyRes } from '../../Models/CopyRes';

@Component({
  selector: 'app-book-list-page',
  imports: [BookListComponent, AsyncPipe, BookSearch],
  templateUrl: './book-list-page.html',
  styleUrl: './book-list-page.css',
})
export class BookListPage implements OnInit {
  paginatedCopies?:Observable<PaginatedList<CopyRes>>;
  constructor(private bookService:BookService) {}
  ngOnInit(): void {
    debugger;
    console.log("called");
    let bookFilter:BookFilter={
      pageNumber: 1,
      pageSize: 10
    }
    this.paginatedCopies=this.bookService.GetBooks(bookFilter);
  }
  search(book: BookFilter) {
  let bookFilter:BookFilter={
      pageNumber: book.pageNumber,
      pageSize: book.pageSize,
      title:book.title,
      genere:book.genere,
      publicationDateFrom:book.publicationDateFrom,
      publicationDateTo:book.publicationDateTo
    }
    this.paginatedCopies=this.bookService.GetBooks(bookFilter);
  }

}
