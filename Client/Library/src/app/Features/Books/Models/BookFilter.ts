export interface BookFilter{
pageNumber:number,
pageSize:number,
sortColumn?:string,
sortDirection?:string,
title?:string,
iSBN?:string,
genere?:string,
publicationDateFrom?:Date,
publicationDateTo?:Date
}