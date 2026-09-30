import { Component, EventEmitter, Input, Output } from '@angular/core';
import { RouterLink } from '@angular/router';
import { CopyRes } from '../../../Books/Models/CopyRes';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-reservation-detail-component',
  imports: [RouterLink,DatePipe],
  templateUrl: './reservation-detail-component.html',
  styleUrl: './reservation-detail-component.css',
})
export class ReservationDetailComponent {
  imageUrl:string="https://localhost:7010/api/Files";
  @Input()copy?:CopyRes;
  @Output()OnReserve=new EventEmitter();
  handleImageError(event: Event) {
    let image=event.target as HTMLImageElement;
    image.onerror=null;
    image.src="/Images/book.jpg"
  }
  Reserve() {
  this.OnReserve.emit(this.copy?.id);
  }

}
