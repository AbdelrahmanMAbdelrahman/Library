import { Component, EventEmitter, Input, input, OnInit, Output } from '@angular/core';
import { PaginatedList } from '../../../../Global/PaginatedList';
import { FineRes } from '../../Models/FineRes';
import { CommonModule } from '@angular/common';
import { PaymentStatus } from '../../Enum/PaymentStatus';
import { FineSearch } from '../../Models/FineSearch';
import { FormControl, FormGroup } from '@angular/forms';
import { DefaultSettings } from '../../../../Global/Consts/DefaultConsts';
import { RouterLink } from '@angular/router';
@Component({
  selector: 'app-fines-list-component',
  imports: [CommonModule, RouterLink],
  templateUrl: './fines-list-component.html',
  styleUrl: './fines-list-component.css',
})
export class FinesListComponent {
  @Input() paginatedFines?:PaginatedList<FineRes>;
  @Output() OnPaginate=new EventEmitter<FineSearch>();
  
  GetPaymentStatusText(status:PaymentStatus){
    return PaymentStatus[status];
  }
  getPrev() {
    let search:FineSearch={
      pageNumber: this.paginatedFines?.pageNumber??1,
      pageSize: DefaultSettings.PageSize
    }
    --search.pageNumber;
  this.OnPaginate.emit(search);
  }
  getNext() {
    let search:FineSearch={
      pageNumber: this.paginatedFines?.pageNumber??1,
      pageSize: DefaultSettings.PageSize
    }
    ++search.pageNumber;
  this.OnPaginate.emit(search);
  }
}
