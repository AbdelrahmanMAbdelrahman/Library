export interface PaginatedList<T>{
    items:Array<T>,
    HasNextPage: boolean,
    HasPreviousPage: false,
    PageNumber: number,
    TotalPages: number
}