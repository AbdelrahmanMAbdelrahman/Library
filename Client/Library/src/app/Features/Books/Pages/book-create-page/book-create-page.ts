import { Component } from '@angular/core';
import { BookCreateComponent } from '../../Components/book-create-component/book-create-component';
import { BookReq } from '../../Models/BookReq';
import { BookService } from '../../Services/BookService';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-book-create-page',
  imports: [BookCreateComponent],
  templateUrl: './book-create-page.html',
  styleUrl: './book-create-page.css',
})
export class BookCreatePage {
  constructor(private bookService:BookService,private route:ActivatedRoute) {

  }
SaveBook(req: BookReq) {
if(this.BookId==="0")
  this.Createbook(req);
else
  this.UpdateBook(req);
}
  UpdateBook(req: BookReq) {
    throw new Error('Method not implemented.');
  }
  Createbook(req: BookReq) {
    throw new Error('Method not implemented.');
  }
get BookId(){return this.route.snapshot.paramMap.get('id')??"0"}

}
