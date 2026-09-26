import { PaymentStatus } from "../Enum/PaymentStatus";

export interface FineSearch{
      pageNumber:number,
      pageSize:number,
      sortDirection?:string,
      sortColumn?:string,
      NumberOfLateDays?:number,
      FineAmount? :number,
      FromBorrowingDate?:string,
      ToBorrowingDate?:string,
      FromDueDate?:string,
      ToDueDate?:string,
      UserName?:string,
      Title?:string,
      PaymentStatus?:PaymentStatus
}