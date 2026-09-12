namespace Library.Application.Features.BorrowingRecords.Queries.GetBorrowingRecord;

public sealed record GetBorrowingRecordCommand(Guid Id):IRequest<Result<BorrowingRecordDto>>;
