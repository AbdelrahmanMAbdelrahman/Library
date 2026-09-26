namespace Library.Application.Features.Fines.Queries.GetFines;

public sealed record GetFinesQuery(
    int PageNumber=1,
    int PageSize=10,
    string SortDirection="Desc",
    string SortColumn="NumberOfLateDays",
    double? NumberOfLateDays=null,
    double? FineAmount =null,
    Guid? BorrowingRecordId=null,
    DateTime? FromBorrowingDate=null,
    DateTime? ToBorrowingDate=null,
    DateTime? FromDueDate =null,
    DateTime? ToDueDate=null,
    string? UserName=null,
    string?Title=null,
    PaymentStatus?PaymentStatus=null
    ):IRequest<Result<PaginatedList<FineDto>>>;
