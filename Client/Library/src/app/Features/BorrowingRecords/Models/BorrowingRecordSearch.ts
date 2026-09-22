export interface BorrowingRecordSearch{
    sortDirection: string ,
    sortColumn: string,
    pageSize:   number,
    pageNumber: number,
    userName:string,
    title:string,
    genere:string,
    fromBorrowingDate:string,
    toBorrowingDate:string,
    fromDueDate:string,
    toDueDate:string,
    fromActualReturnDate:string,
    toActualReturnDate:string
}