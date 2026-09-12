namespace Library.Application.Features.Fines.Commands.CreateFines;

public sealed record CreateFinesCommand(
    Guid BorrowingRecordId,decimal FineAmount,int NumberOfLateDays,PaymentStatus PaymentStatus)
    :IRequest<Result<FineDto>>;
