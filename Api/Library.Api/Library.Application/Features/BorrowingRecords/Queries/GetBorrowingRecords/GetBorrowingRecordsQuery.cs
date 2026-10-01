namespace Library.Application.Features.BorrowingRecords.Queries.GetBorrowingRecords;

public sealed record GetBorrowingRecordsQuery(
    int PageNumber = 1, int PageSize = 10, string SortColumn = "BorrowingDate",
    string SortDirection = "Desc", Guid? CopyId = null, DateTime? FromBorrowingDate = null,
    string UserName = null, string Title = null, string Genere = null,
    DateTime? ToBorrowingDate = null, DateTime? FromDueDate = null, DateTime? ToDueDate = null,
    DateTime? FromActualReturnDate = null, DateTime? ToActualReturnDate = null
    ) : ICachedQuery<Result<PaginatedList<BorrowingRecordDto>>>
{
    public string Key =>
        $"P:{PageNumber}-" +
        $"ps:{PageSize}-" +
        $"sc:{SortColumn}-" +
        $"sd:{SortDirection}-" +
        $"cid:{CopyId.ToString() ?? "-"}-" +
        $"fbd:{FromBorrowingDate?.ToString("yyyyMMdd") ?? "-"}-" +
        $"tbd:{ToBorrowingDate?.ToString("yyyyMMdd") ?? "-"}-" +
        $"u:{UserName ?? "-"}-" +
        $"t:{Title ?? "-"}-" +
        $"g:{Genere ?? "-"}-" +
        $"fdd:{FromDueDate?.ToString("yyyyMMdd") ?? "-"}-" +
        $"tdd:{ToDueDate?.ToString("yyyyMMdd") ?? "-"}-" +
        $"fard:{FromActualReturnDate?.ToString("yyyyMMdd") ?? "-"}-" +
        $"tard:{ToActualReturnDate?.ToString("yyyyMMdd") ?? "-"}-";

    public string[] Tags => ["BorrowingRecords"];

    public TimeSpan Expiration => TimeSpan.FromMinutes(10);
}
