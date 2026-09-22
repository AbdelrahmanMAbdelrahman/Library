import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { PaginatedList } from "../../../Global/PaginatedList";
import { BookRes } from "../Models/BookRes";
import { BookFilter } from "../Models/BookFilter";
import { BookReq } from "../Models/BookReq";
import { CopyRes } from "../Models/CopyRes";

@Injectable({
    providedIn:'root'
})
export class BookService{
    baseUrl:string="https://localhost:7010/api/Copies";
    constructor(private http:HttpClient) {
    }
    ReturnCopy(copyId: string):Observable<any> {
        
      return this.http.put<any>(`${this.baseUrl}/${copyId}/Return`,null);
    }
GetCopy(bookId:string):Observable<CopyRes>{
    
    return this.http.get<CopyRes>(`${this.baseUrl}/${bookId}`);
}
GetBooks(bookFilter:BookFilter):Observable<PaginatedList<CopyRes>>{
    debugger;
    let params=new HttpParams()
    .set("PageNumber",bookFilter.pageNumber)
    .set("PageSize",bookFilter.pageSize);
    if(bookFilter.sortColumn)
        params=params.set("SortColumn",bookFilter.sortColumn!);
if(bookFilter.sortDirection)
params=params.set("SortDirection",bookFilter.sortDirection!);
if(bookFilter.title)
    params=params.set("Title",bookFilter.title!);
if(bookFilter.genere)
    params= params.set("Genere",bookFilter.genere!);
if(bookFilter.publicationDateFrom)
    params=params.set("PublicationDateFrom",bookFilter.publicationDateFrom!.toString());
if(bookFilter.publicationDateTo)
    params=params.set("PublicationDateTo",bookFilter.publicationDateTo!.toString());

return this.http.get<PaginatedList<CopyRes>>(this.baseUrl,{params});
}
GetBook(Id: string): Observable<BookRes> {
 return this.http.get<BookRes>(`${this.baseUrl}/${Id}`);
}
CreateBook(req:BookReq):Observable<BookRes>{
    const formData=new FormData();
    formData.append('title',req.title);
    formData.append('genere',req.genere);
    formData.append('isbn',req.isbn);
    formData.append('additionalDetails',req.additionalDetails);
    formData.append('numberOfCopies',req.numberOfCopies.toString());
    formData.append('publicationDate',req.publicationDate);
    if(req.image)
    formData.append('image',req.image);
return this.http.post<BookRes>(this.baseUrl,formData)
}
UpdateBook(req:BookReq,id:string):Observable<any>{
    const formData=new FormData();
    formData.append('title',req.title);
    formData.append('genere',req.genere);
    formData.append('isbn',req.isbn);
    formData.append('additionalDetails',req.additionalDetails);
    formData.append('publicationDate',req.publicationDate);
return this.http.put(`${this.baseUrl}/${id}`,formData);
}
}