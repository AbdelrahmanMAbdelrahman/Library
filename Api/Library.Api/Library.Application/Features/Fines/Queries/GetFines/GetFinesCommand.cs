namespace Library.Application.Features.Fines.Queries.GetFines;

public sealed record GetFinesCommand(
    int PageNumber=1,
    int PageSize=10,
    string SortDirection="Desc",
    string SortColumn="NumberOfLateDays",
    int? NumberOfLateDays=null,
    decimal? FineAmount =null,
    Guid? BorrowingRecordId=null,
    PaymentStatus?PaymentStatus=PaymentStatus.UnPaid
    ):IRequest<Result<PaginatedList<FineDto>>>;
