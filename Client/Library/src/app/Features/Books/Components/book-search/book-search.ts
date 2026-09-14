import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

import { BookFilter } from '../../Models/BookFilter';
import { DefaultSettings } from '../../../../Global/Consts/DefaultConsts';

@Component({
  selector: 'app-book-search',
  imports: [ReactiveFormsModule],
  templateUrl: './book-search.html',
  styleUrl: './book-search.css',
})
export class BookSearch implements OnInit {
  @Output()OnSearch=new EventEmitter<BookFilter>();
  searchForm?:FormGroup;
  ngOnInit(): void {
    this.searchForm=new FormGroup({
      'title':new FormControl(null),
      'genere':new FormControl(null),
      'publicationDateFrom':new FormControl(null),
      'publicationDateTo':new FormControl(null),
    })
  }
  Search() {
    debugger;
  let bookFilter:BookFilter={
    PageNumber: 1,
    PageSize: DefaultSettings.PageSize,
    Title:this.searchForm?.get("title")?.value,
    Genere:this.searchForm?.get("genere")?.value,
    PublicationDateFrom:this.searchForm?.get("publicationDateFrom")?.value,
    PublicationDateTo:this.searchForm?.get("publicationDateTo")?.value
  }
  this.OnSearch.emit(bookFilter);
  }

}
