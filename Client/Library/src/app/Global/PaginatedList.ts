export interface PaginatedList<T>{
    items:Array<T>,
    hasNextPage: boolean,
    hasPreviousPage: false,
    pageNumber: number,
    totalPages: number
}