namespace Library.Application.Features.BorrowingRecords.Queries.GetBorrowingRecords;

public sealed record GetBorrowingRecordsQuery(
    int PageNumber=1,int PageSize=10,string SortColumn="BorrowingDate",
    string SortDirection="Desc",Guid? CopyId=null,DateTime? FromBorrowingDate = null,
    string UserName=null,string Title=null,string Genere=null,
    DateTime? ToBorrowingDate = null, DateTime? FromDueDate = null, DateTime? ToDueDate = null,
    DateTime? FromActualReturnDate = null, DateTime? ToActualReturnDate = null
    ) :IRequest<Result<PaginatedList<BorrowingRecordDto>>>;
