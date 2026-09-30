import { Component, EventEmitter, Input, Output } from '@angular/core';
import { PaginatedList } from '../../../../Global/PaginatedList';
import { CopyRes } from '../../../Books/Models/CopyRes';
import { BookFilter } from '../../../Books/Models/BookFilter';
import { DefaultSettings } from '../../../../Global/Consts/DefaultConsts';
import { RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-reservation-list-component',
  imports: [RouterLink,CommonModule],
  templateUrl: './reservation-list-component.html',
  styleUrl: './reservation-list-component.css',
})
export class ReservationListComponent {
  @Input()paginatedCopies?:PaginatedList<CopyRes>;
  @Output()OnPaginate=new EventEmitter<BookFilter>();
  filePath:string="https://localhost:7010/api/Files";
  
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
}
