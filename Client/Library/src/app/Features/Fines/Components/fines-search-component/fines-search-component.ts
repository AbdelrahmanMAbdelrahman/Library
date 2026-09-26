import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FineSearch } from '../../Models/FineSearch';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { DefaultSettings } from '../../../../Global/Consts/DefaultConsts';
import { FineRes } from '../../Models/FineRes';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-fines-search-component',
  imports: [ReactiveFormsModule,DatePipe],
  templateUrl: './fines-search-component.html',
  styleUrl: './fines-search-component.css',
})
export class FinesSearchComponent implements OnInit {
  
  @Output() OnSearch=new EventEmitter<FineSearch>();
  FineForm?:FormGroup;
  
  ngOnInit(): void {
    this.InitForm();
  }
  InitForm() {
    this.FineForm=new FormGroup({
      'userName':new FormControl(null),
      'title':new FormControl(null),
      'lateDays':new FormControl(null),
      'fineAmount':new FormControl(null),
      'fromBorrowingDate':new FormControl(null),
      'toBorrowingDate':new FormControl(null),
      'fromDueDate':new FormControl(null),
      'toDueDate':new FormControl(null),
      'paymentStatus':new FormControl(null),
    });
  }
  Clear() {
  this.FineForm?.reset();
  }
  Search() {
    debugger;
    let search:FineSearch={
      pageNumber: 1,
      pageSize: DefaultSettings.PageSize,
      UserName:this.FineForm?.get('userName')?.value,
      Title:this.FineForm?.get('title')?.value,
      FromBorrowingDate:this.FineForm?.get('fromBorrowingDate')?.value,
      ToBorrowingDate:this.FineForm?.get('toBorrowingDate')?.value,
      FromDueDate:this.FineForm?.get('fromDueDate')?.value,
      ToDueDate:this.FineForm?.get('toDueDate')?.value,
      PaymentStatus:this.FineForm?.get('paymentStatus')?.value,
      NumberOfLateDays:this.FineForm?.get('lateDays')?.value,
      FineAmount:this.FineForm?.get('fineAmount')?.value,
    };
  this.OnSearch.emit(search);
  }
}
