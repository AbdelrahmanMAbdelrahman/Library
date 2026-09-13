export interface BookFilter{
PageNumber:number,
PageSize:number,
SortColumn?:string,
SortDirection?:string,
Title?:string,
ISBN?:string,
Genere?:string,
PublicationDateFrom?:Date,
PublicationDateTo?:Date
}