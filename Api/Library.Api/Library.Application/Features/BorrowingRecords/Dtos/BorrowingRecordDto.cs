namespace Library.Application.Features.BorrowingRecords.Dtos;

public sealed record BorrowingRecordDto(
    Guid Id,DateTime BorrowingDate,DateTime DueDate,DateTime? ActualReturnDate,
    CopyDto copy,UserInfoDto UserInfo);
