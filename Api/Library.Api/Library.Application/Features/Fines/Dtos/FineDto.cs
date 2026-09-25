using Library.Domain.Fines.Enums;

namespace Library.Application.Features.Fines.Dtos;

public sealed record FineDto(Guid Id,UserInfoDto UserInfoDto,BorrowingRecordDto BorrowingRecordDto,Guid BorrowingRecordId,
    double NumberOfLateDays,double FineAmount,PaymentStatus PaymentStatus);
