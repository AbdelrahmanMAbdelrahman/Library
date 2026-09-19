import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { outputToObservable } from '@angular/core/rxjs-interop';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { url } from 'inspector';

import { BookReq } from '../../Models/BookReq';

@Component({
  selector: 'app-book-create-component',
  imports: [ReactiveFormsModule],
  templateUrl: './book-create-component.html',
  styleUrl: './book-create-component.css',
})
export class BookCreateComponent implements OnInit {
  bookForm?:FormGroup;
  @Output()OnSaveBook=new EventEmitter<BookReq>(); 
  
  constructor() {
  }
  
  ngOnInit(): void {
    this.bookForm=new FormGroup({
      'title':new FormControl(null,Validators.required),
      'isbn':new FormControl(null,Validators.required),
      'genere':new FormControl(null,Validators.required),
      'additionalDetails':new FormControl(null,Validators.required),
      'publicationDate':new FormControl(null,Validators.required),
      'numberOfCopies':new FormControl(null,Validators.required),
      'image':new FormControl<File|null>(null),
    });
  }
  selectImage(event: Event) {
    let input=event.target as HTMLInputElement;
    if(!input||input.files?.length===0)return;
  const image=input.files![0];
  this.bookForm?.patchValue({image:image});;
  
}
clearImage(image: HTMLInputElement) {
  console.log("remove image");
  image.value='';
  this.bookForm?.patchValue({image:null});
}
SaveBook() {
let req:BookReq=this.fillObject();
this.OnSaveBook.emit(req);
}

fillObject(): BookReq {
let req: BookReq={
 title: this.bookForm?.get('title')?.value??'',
 isbn: this.bookForm?.get('isbn')?.value??'',
 genere: this.bookForm?.get('genere')?.value??'',
 additionalDetails: this.bookForm?.get('additionalDetails')?.value??'',
 publicationDate: this.bookForm?.get('publicationDate')?.value??'',
 numberOfCopies: this.bookForm?.get('numberOfCopies')?.value??'',
 image: this.bookForm?.get('image')?.value??''
}
return req;
}
}

