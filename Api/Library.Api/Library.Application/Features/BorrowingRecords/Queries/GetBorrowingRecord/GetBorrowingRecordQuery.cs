namespace Library.Application.Features.BorrowingRecords.Queries.GetBorrowingRecord;

public sealed record GetBorrowingRecordQuery(Guid Id) : ICachedQuery<Result<BorrowingRecordDto>>
{
    public string Key => $"BorrowingRecord-{Id}";

    public string[] Tags => ["BorrowingRecord"];

    public TimeSpan Expiration =>TimeSpan.FromMinutes(10);
}
