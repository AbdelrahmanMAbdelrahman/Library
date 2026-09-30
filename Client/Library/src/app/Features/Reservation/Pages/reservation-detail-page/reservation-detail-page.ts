import { Component, OnInit } from '@angular/core';
import { ReservationDetailComponent } from '../../Components/reservation-detail-component/reservation-detail-component';
import { Observable } from 'rxjs';
import { CopyRes } from '../../../Books/Models/CopyRes';
import { ReservationService } from '../../Services/ReservationService';
import { ActivatedRoute, Router } from '@angular/router';
import { BookService } from '../../../Books/Services/BookService';
import { BookRes } from '../../../Books/Models/BookRes';
import { AsyncPipe } from '@angular/common';
import { ReservationRes } from '../../Models/ReservationRes';
import { Failure } from '../../../../Global/Failure';
import { fail } from 'assert';


@Component({
  selector: 'app-reservation-detail-page',
  imports: [ReservationDetailComponent,AsyncPipe],
  templateUrl: './reservation-detail-page.html',
  styleUrl: './reservation-detail-page.css',
})
export class ReservationDetailPage implements OnInit{
  copy?:Observable<CopyRes>;

  constructor(
    private reservationService:ReservationService,
    private route:ActivatedRoute,
  private bookService:BookService,
private router:Router) {}
  ngOnInit(): void {
    this.copy=this.bookService.GetCopy(this.Id)
  }
Reserve() {
this.reservationService.Reserve(this.Id).subscribe({
  next:(res:ReservationRes)=>{
this.router.navigate(['ReservationPage','ReservationListPage'])
  },
  error:(err)=>{
let failure=err.error as Failure;
console.log(failure);
  }
});
}
get Id(){
return this.route.snapshot.paramMap.get('id')??"0";
}
}
