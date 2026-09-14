import { Component } from '@angular/core';
import { BookCreateComponent } from '../../Components/book-create-component/book-create-component';

@Component({
  selector: 'app-book-create-page',
  imports: [BookCreateComponent],
  templateUrl: './book-create-page.html',
  styleUrl: './book-create-page.css',
})
export class BookCreatePage {}
