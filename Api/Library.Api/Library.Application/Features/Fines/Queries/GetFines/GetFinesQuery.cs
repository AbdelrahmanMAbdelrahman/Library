namespace Library.Application.Features.Fines.Queries.GetFines;

public sealed record GetFinesQuery(
    int PageNumber = 1,
    int PageSize = 10,
    string SortDirection = "Desc",
    string SortColumn = "NumberOfLateDays",
    double? NumberOfLateDays = null,
    double? FineAmount = null,
    DateTime? FromBorrowingDate = null,
    DateTime? ToBorrowingDate = null,
    DateTime? FromDueDate = null,
    DateTime? ToDueDate = null,
    string? UserName = null,
    string? Title = null,
    PaymentStatus? PaymentStatus = null
    ) : ICachedQuery<Result<PaginatedList<FineDto>>>
{
    public string Key =>
        $"p:{PageNumber}-" +
        $"ps:{PageSize}-" +
        $"sc:{SortColumn}-" +
        $"sd:{SortDirection}-" +
        $"nold:{NumberOfLateDays.ToString() ?? "-"}-" +
        $"fa:{FineAmount.ToString() ?? "-"}-" +
        $"fbd:{FromBorrowingDate?.ToString("yyyyMMdd") ?? "-"}-" +
        $"tbd:{ToBorrowingDate?.ToString("yyyyMMdd") ?? "-"}-" +
        $"fdd:{FromDueDate?.ToString("yyyyMMdd") ?? "-"}-" +
        $"tdd:{ToDueDate?.ToString("yyyyMMdd") ?? "-"}-" +
        $"u:{UserName ?? "-"}-" +
        $"t:{Title ?? "-"}-" +
        $"pst:{PaymentStatus.ToString() ?? "-"}-";

    public string[] Tags => ["Fines"];

    public TimeSpan Expiration => TimeSpan.FromMinutes(10);
}
