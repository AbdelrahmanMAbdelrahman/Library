import { Component, Input } from '@angular/core';
import { BookRes } from '../../Models/BookRes';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-book-detail-component',
  imports: [RouterLink],
  templateUrl: './book-detail-component.html',
  styleUrl: './book-detail-component.css',
})
export class BookDetailComponent {
  imageUrl:string="https://localhost:7010/api/File";
  @Input()book?:BookRes;
  handleImageError(event: Event) {
  let image=event.target as HTMLImageElement;
  image.onerror=null;
  image.src="/Images/book.jpg"
  }
}
