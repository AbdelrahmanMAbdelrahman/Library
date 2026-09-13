import { Component, Input, input, OnInit } from '@angular/core';
import { PaginatedList } from '../../../../Global/PaginatedList';
import { BookRes } from '../../Models/BookRes';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-book-list-component',
  imports: [CommonModule],
  templateUrl: './book-list-component.html',
  styleUrl: './book-list-component.css',
})
export class BookListComponent  implements OnInit{
  @Input()paginatedBooks?:PaginatedList<BookRes>;
  ngOnInit(): void {
        console.table(this.paginatedBooks?.items)
  
  }
}
