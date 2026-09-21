namespace Library.Application.Features.BorrowingRecords.Queries.GetBorrowingRecords;

public sealed record GetBorrowingRecordsQuery(
    int PageNumber=1,int PageSize=10,string SortColumn="BorrowingDate",
    string SortDirection="Desc",Guid? CopyId=null,DateTime? BorrowingDateFrom=null,
    DateTime? BorrowingDateTo = null, DateTime?DueDateFrom=null, DateTime? DueDateTo = null
    ) :IRequest<Result<PaginatedList<BorrowingRecordDto>>>;
