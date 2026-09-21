export interface BorrowingRecordSearch{
    sortDirection: string ;
    sortColumn: string;
    pageSize:   number;
    pageNumber: number;
    userName:string,
    title:string,
    genere:string,
    fromBorrowingDate:string,
    toBorrowingDate:string,
    fromReturnDate:string,
    toReturnDate:string,
    fromActualReturnDate:string,
    toActualReturnDate:string
}