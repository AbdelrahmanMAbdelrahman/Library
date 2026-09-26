import { Component, OnInit } from '@angular/core';
import { FinesDetailComponent } from '../../Components/fines-detail-component/fines-detail-component';
import { Observable } from 'rxjs';
import { FineRes } from '../../Models/FineRes';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { FineService } from '../../Services/FineService';
import { ActivatedRoute } from '@angular/router';
import { AsyncPipe } from '@angular/common';
import { Failure } from '../../../../Global/Failure';

@Component({
  selector: 'app-fines-detail-page',
  imports: [FinesDetailComponent,AsyncPipe],
  templateUrl: './fines-detail-page.html',
  styleUrl: './fines-detail-page.css',
})
export class FinesDetailPage  implements OnInit{
  Fine?:Observable<FineRes>;
  constructor(private fineService:FineService,private route:ActivatedRoute) {}
  ngOnInit(): void {
    this.Fine=this.fineService.GetFine(this.Id??"");
    console.log("fine",this.Fine)
  }
  get Id(){
    return this.route.snapshot.paramMap.get("id");
  }
  PayFine() {
  this.fineService.PayFine(this.Id??"").subscribe({
    error:(err:HttpErrorResponse)=>{
     let failure=err.error as Failure;
     console.log(failure)
    }
  });
  }
}
