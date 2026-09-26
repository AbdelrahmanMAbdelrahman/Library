import { Injectable } from "@angular/core";
import { FineSearch } from "../Models/FineSearch";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { PaginatedList } from "../../../Global/PaginatedList";
import { FineRes } from "../Models/FineRes";

@Injectable({providedIn:'root'})
export class FineService{
    private url:string="https://localhost:7010/api/Fine";
    /**
     *
    */
   constructor(private http:HttpClient) {}
   PayFine(Id: string ) :Observable<any>{
     return this.http.put(`${this.url}/${Id}`,null);
   }
   GetFines(search:FineSearch):Observable<PaginatedList<FineRes>>{
       debugger;
       let params=new HttpParams()
       .set("pageNumber",search.pageNumber)
    .set("pageSize",search.pageSize)
    .set("sortDirection",search.sortDirection??"Desc")
    .set("sortColumn",search.sortColumn??"title");
    if(search.FromBorrowingDate)params= params.set("fromBorrowingDate",search.FromBorrowingDate??"");
    if(search.ToBorrowingDate)params= params.set("toBorrowingDate",search.ToBorrowingDate);
    if(search.NumberOfLateDays)params= params.set("numberOfLateDays",search.NumberOfLateDays);
    if(search.FineAmount)params= params.set("fineAmount",search.FineAmount);
    if(search.Title)params= params.set("title",search.Title);
    if(search.UserName)params= params.set("userName",search.UserName);
    if(search.PaymentStatus)params= params.set("paymentStatus",search.PaymentStatus);
    return this.http.get<PaginatedList<FineRes>>(this.url,{params});
}

GetFine(Id: string): Observable<FineRes> {
  return this.http.get<FineRes>(`${this.url}/${Id}`);
}
}