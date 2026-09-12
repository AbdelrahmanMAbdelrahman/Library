using Library.Application.Common.Models;

namespace Library.Application.Features.BorrowingRecords.Queries.GetBorrowingRecords;

public sealed record GetBorrowingRecordsCommand(
    int PageNumber,int PageSize,string SortColumn="BorrowingDate",
    string SortDirection="Desc",Guid? CopyId=null,DateTime? BorrowingDateFrom=null,
    DateTime? BorrowingDateTo = null, DateTime?DueDateFrom=null, DateTime? DueDateTo = null
    ) :IRequest<Result<PaginatedList<BorrowingRecordDto>>>;
