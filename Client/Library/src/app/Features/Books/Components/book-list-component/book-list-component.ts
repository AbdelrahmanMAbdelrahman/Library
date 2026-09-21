import { Component, Input, input, OnInit } from '@angular/core';
import { PaginatedList } from '../../../../Global/PaginatedList';
import { BookRes } from '../../Models/BookRes';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { CopyRes } from '../../Models/CopyRes';

@Component({
  selector: 'app-book-list-component',
  imports: [CommonModule, RouterLink],
  templateUrl: './book-list-component.html',
  styleUrl: './book-list-component.css',
})
export class BookListComponent  implements OnInit{
  @Input()paginatedCopies?:PaginatedList<CopyRes>;
  filePath:string="https://localhost:7010/api/File";
  ngOnInit(): void {
    console.table(this.paginatedCopies?.items)
    
  }
  OnImageError(event: Event) {
  const img =event.target as HTMLImageElement;
  img.onerror=null;
  img.src="/Images/book.jpg"
  }
  // get PersonImage(){
  //   if(pagin)
  // }
}
