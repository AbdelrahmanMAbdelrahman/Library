import { Component, EventEmitter, Input, input, OnInit, Output } from '@angular/core';
import { PaginatedList } from '../../../../Global/PaginatedList';
import { BookRes } from '../../Models/BookRes';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { CopyRes } from '../../Models/CopyRes';

import { BookSearch } from '../book-search/book-search';
import { BookFilter } from '../../Models/BookFilter';
import { DefaultSettings } from '../../../../Global/Consts/DefaultConsts';

@Component({
  selector: 'app-book-list-component',
  imports: [CommonModule, RouterLink],
  templateUrl: './book-list-component.html',
  styleUrl: './book-list-component.css',
})
export class BookListComponent  {
  @Input()paginatedCopies?:PaginatedList<CopyRes>;
  @Output()OnPaginate=new EventEmitter<BookFilter>();
  filePath:string="https://localhost:7010/api/File";
  
  OnImageError(event: Event) {
    const img =event.target as HTMLImageElement;
    img.onerror=null;
    img.src="/Images/book.jpg"
  }
  getNext() {
    debugger;
    let filterReq:BookFilter=this.getBookSearchReq();
  ++filterReq.pageNumber;
this.OnPaginate.emit(filterReq);
  }
  getPrev() {
  let filterReq:BookFilter=this.getBookSearchReq();
  --filterReq.pageNumber;
this.OnPaginate.emit(filterReq);
  }
  getBookSearchReq(){
    let searchReq:BookFilter={
      pageNumber: this.paginatedCopies?.pageNumber??1,
      pageSize: DefaultSettings.PageSize
    }
    return searchReq;
  }
  // get PersonImage(){
  //   if(pagin)
  // }
}
