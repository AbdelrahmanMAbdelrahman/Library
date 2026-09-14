import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { PaginatedList } from "../../../Global/PaginatedList";
import { BookRes } from "../Models/BookRes";
import { BookFilter } from "../Models/BookFilter";


@Injectable({
    providedIn:'root'
})
export class BookService{
    baseUrl:string="https://localhost:7010/api/Book";
    constructor(private http:HttpClient) {
}

GetBooks(bookFilter:BookFilter):Observable<PaginatedList<BookRes>>{
    debugger;
    let params=new HttpParams()
    .set("PageNumber",bookFilter.PageNumber)
    .set("PageSize",bookFilter.PageSize);
    if(bookFilter.SortColumn)
        params=params.set("SortColumn",bookFilter.SortColumn!);
if(bookFilter.SortDirection)
params=params.set("SortDirection",bookFilter.SortDirection!);
if(bookFilter.Title)
    params=params.set("Title",bookFilter.Title!);
if(bookFilter.Genere)
    params= params.set("Genere",bookFilter.Genere!);
if(bookFilter.PublicationDateFrom)
    params=params.set("PublicationDateFrom",bookFilter.PublicationDateFrom!.toString());
if(bookFilter.PublicationDateTo)
    params=params.set("PublicationDateTo",bookFilter.PublicationDateTo!.toString());

return this.http.get<PaginatedList<BookRes>>(this.baseUrl,{params});
}
GetBook(Id: string): Observable<BookRes> {
 return this.http.get<BookRes>(`${this.baseUrl}/${Id}`);
}
}