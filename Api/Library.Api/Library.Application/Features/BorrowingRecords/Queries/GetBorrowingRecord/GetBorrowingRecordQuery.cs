namespace Library.Application.Features.BorrowingRecords.Queries.GetBorrowingRecord;

public sealed record GetBorrowingRecordQuery(Guid Id):IRequest<Result<BorrowingRecordDto>>;
