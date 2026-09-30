import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { ReservationRes } from "../Models/ReservationRes";

@Injectable({
    providedIn:'root'
})
export class ReservationService{
  url:string="https://localhost:7010/api/Reservations";
  constructor(private http:HttpClient) {
    
  }
  Reserve(Id: string):Observable<ReservationRes> {
    return this.http.post<ReservationRes>(this.url,{copyId:Id});
  }
    
}