import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { PaginatedList } from "../../../Global/PaginatedList";
import { BorrowingRecordRes } from "../Models/BorrowingRecordRes";
import { BorrowingRecordReq } from "../Models/BorrowingRecordReq";
import { BorrowingRecordSearch } from "../Models/BorrowingRecordSearch";

@Injectable({providedIn:'root'})
export class BorrowingRecordService{

  private  url:string="https://localhost:7010/api/BorrowingRecord";
constructor(private http:HttpClient) {}

 getBorrowingRecords(search:BorrowingRecordSearch):Observable<PaginatedList<BorrowingRecordRes>>{
debugger;
    let params=new HttpParams().set('pageNumber',search.pageNumber)
.set('pageSize',search.pageSize);
if(search.fromActualReturnDate)
    params=params.set('fromActualReturnDate',search.fromActualReturnDate);
if(search.toActualReturnDate)
    params=params.set('toActualReturnDate',search.toActualReturnDate);
if(search.fromDueDate)
    params=params.set('fromDueDate',search.fromDueDate);
if(search.toDueDate)
    params=params.set('toDueDate',search.toDueDate);
if(search.fromBorrowingDate)
    params=params.set('fromBorrowingDate',search.fromBorrowingDate);
if(search.toBorrowingDate)
    params=params.set('toBorrowingDate',search.toBorrowingDate);
if(search.title)
    params=params.set('title',search.title);
if(search.genere)
    params=params.set('genere',search.genere);
if(search.userName)
    params=params.set('userName',search.userName);
if(search.sortColumn)
    params=params.set('sortColumn',search.sortColumn);
if(search.sortDirection)
    params=params.set('sortDirection',search.sortDirection);

    return this.http.get<PaginatedList<BorrowingRecordRes>>(this.url,{params});
 }

 createBorrowingRecord(req:BorrowingRecordReq):Observable<BorrowingRecordRes>{
    debugger;
    return this.http.post<BorrowingRecordRes>(this.url,req);
 }
 getBorrowingRecord(Id:string):Observable<BorrowingRecordRes>{
    console.log('getBorrowingRecord CALLED');
  console.log('Id =', Id);
  console.log('URL =', `${this.url}/${Id}`);
    return this.http.get<BorrowingRecordRes>(`${this.url}/${Id}`);
 }
}