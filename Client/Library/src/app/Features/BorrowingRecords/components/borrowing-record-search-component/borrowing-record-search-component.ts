import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { BorrowingRecordSearch } from '../../Models/BorrowingRecordSearch';
import { DefaultSettings } from '../../../../Global/Consts/DefaultConsts';

@Component({
  selector: 'app-borrowing-record-search-component',
  imports: [ReactiveFormsModule],
  templateUrl: './borrowing-record-search-component.html',
  styleUrl: './borrowing-record-search-component.css',
})
export class BorrowingRecordSearchComponent implements OnInit {
  searchForm?:FormGroup;
  @Output() OnSearch=new EventEmitter<BorrowingRecordSearch>();
  ngOnInit(): void {
    this.initForm();
  }
  initForm() {
    this.searchForm=new FormGroup({
      'userName':new FormControl(null),
      'title':new FormControl(null),
      'genere':new FormControl(null),
      'fromBorrowingDate':new FormControl(null),
      'toBorrowingDate':new FormControl(null),
      'fromDueDate':new FormControl(null),
      'toDueDate':new FormControl(null),
      'fromActualReturnDate':new FormControl(null),
      'toActualReturnDate':new FormControl(null)
    });
  }
  clearForm() {
  this.searchForm?.reset();
  }
  search() {

 let recordReq:BorrowingRecordSearch={
   sortDirection: 'desc',
   sortColumn: 'title',
   pageSize: DefaultSettings.PageSize,
   pageNumber: 1,
   userName: this.searchForm?.get('userName')?.value,
   title: this.searchForm?.get('title')?.value,
   genere: this.searchForm?.get('genere')?.value,
   fromBorrowingDate: this.searchForm?.get('fromBorrowingDate')?.value,
   toBorrowingDate: this.searchForm?.get('toBorrowingDate')?.value,
   fromDueDate: this.searchForm?.get('fromDueDate')?.value,
   toDueDate: this.searchForm?.get('toDueDate')?.value,
   fromActualReturnDate: this.searchForm?.get('fromActualReturnDate')?.value,
   toActualReturnDate: this.searchForm?.get('toActualReturnDate')?.value
 }
this.OnSearch.emit(recordReq);
  }

}
