import { Component, OnInit } from '@angular/core';
import { ReservationListComponent } from '../../Components/reservation-list-component/reservation-list-component';
import { BookSearch } from '../../../Books/Components/book-search/book-search';
import { Observable } from 'rxjs';
import { PaginatedList } from '../../../../Global/PaginatedList';
import { ReservationRes } from '../../Models/ReservationRes';
import { ReservationService } from '../../Services/ReservationService';
import { BookService } from '../../../Books/Services/BookService';
import { BookRes } from '../../../Books/Models/BookRes';
import { BookReq } from '../../../Books/Models/BookReq';
import { BookFilter } from '../../../Books/Models/BookFilter';
import { CopyRes } from '../../../Books/Models/CopyRes';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-reservation-list-page',
  imports: [ReservationListComponent, BookSearch,AsyncPipe],
  templateUrl: './reservation-list-page.html',
  styleUrl: './reservation-list-page.css',
})
export class ReservationListPage implements OnInit {
  paginatedReservations?:Observable<PaginatedList<ReservationRes>>;
  paginatedBooks?:Observable<PaginatedList<CopyRes>>;
  constructor(private reservationService:ReservationService,private bookService:BookService) {
    
  }
  ngOnInit(): void {
    let req:BookFilter={
      pageNumber: 1,
      pageSize: 10
    }
   this.paginatedBooks=this.bookService.GetUnAvailableBooks(req);
  }
}
