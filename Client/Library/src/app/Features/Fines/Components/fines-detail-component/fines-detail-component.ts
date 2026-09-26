import { Component, EventEmitter, Input, Output } from '@angular/core';
import { RouterLink } from '@angular/router';
import { FineRes } from '../../Models/FineRes';
import { DatePipe } from '@angular/common';
import { PaymentStatus } from '../../Enum/PaymentStatus';


@Component({
  selector: 'app-fines-detail-component',
  imports: [RouterLink,DatePipe],
  templateUrl: './fines-detail-component.html',
  styleUrl: './fines-detail-component.css',
})
export class FinesDetailComponent {
  @Input() fine?:FineRes;
  @Output() OnPay=new EventEmitter();
paymentStatus=PaymentStatus;
PayFine() {
this.OnPay.emit();
}

}
