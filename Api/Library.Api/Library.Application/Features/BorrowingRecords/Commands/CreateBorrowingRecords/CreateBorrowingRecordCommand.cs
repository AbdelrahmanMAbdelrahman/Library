using Library.Application.Features.BorrowingRecords.Dtos;

namespace Library.Application.Features.BorrowingRecords.Commands.CreateBorrowingRecords;

public sealed record CreateBorrowingRecordCommand(
    Guid CopyId,DateTime BorrowingDate):IRequest<Result<BorrowingRecordDto>>;
