namespace Library.Application.Features.Fines.Commands.CreateFines;

public sealed class CreateFinesHandler 
    (ILogger<CreateFinesHandler>logger,IAppDbContext context,IUser user)
    : IRequestHandler<CreateFinesCommand, Result<FineDto>>
{
    public async Task<Result<FineDto>> Handle(CreateFinesCommand request, CancellationToken cancellationToken)
    {
        BorrowingRecord? borrowingRecord = await context.BorrowingRecords.FindAsync(request.BorrowingRecordId);
        if (borrowingRecord is null) {
            logger.LogError($"no borrowing record found for id = {request.BorrowingRecordId}");
            return ApplicationErrors.BorrowingRecordNotFound(request.BorrowingRecordId); }

        Result<Fine> AddFineResult = Fine.Create(request.BorrowingRecordId,
            user.Id,request.NumberOfLateDays,request.FineAmount,request.PaymentStatus);
        if (AddFineResult.IsError)
        {
            logger.LogError(string.Join(" - ",AddFineResult.Errors));
            return AddFineResult.Errors;
        }

        await context.Fines.AddAsync(AddFineResult.Value,cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return AddFineResult.Value.ToDto();
    }
}
