
using Library.Domain.Copies.Events;
using Microsoft.Extensions.Caching.Hybrid;

namespace Library.Application.Features.Fines.Commands.PayFines;

public sealed class PayFineHandler (IAppDbContext context,HybridCache hybridCache,ILogger<PayFineHandler>logger) 
    : IRequestHandler<PayFineCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(PayFineCommand request, CancellationToken cancellationToken)
    {
        Fine? fine = await context.Fines.FindAsync(request.Id,cancellationToken);
        if(fine is null)
        {
            logger.LogError($"no fine found for id = {request.Id}");
            return ApplicationErrors.FineNotFound(request.Id);
        }
        if (fine.PaymentStatus == PaymentStatus.Paid)
        {
            logger.LogError("Payment already paid");
            return ApplicationErrors.FineAlreadyPaid;
        }
        BorrowingRecord? borrowingRecord = await context.BorrowingRecords
            .Include(br=>br.Copy)
            .FirstOrDefaultAsync(br=>br.Id==fine.BorrowingRecordId,cancellationToken);
        if (borrowingRecord is null) {
            logger.LogError($"No borrowing record found for id = ${fine.BorrowingRecordId}");
            return ApplicationErrors.BorrowingRecordNotFound(fine.BorrowingRecordId);
        }
        if (borrowingRecord.ActualReturnDate != null)
        {
            logger.LogError($"book already returned for borrowing record with id = {fine.BorrowingRecordId}");
            return ApplicationErrors.BookAlreadyReturned(borrowingRecord.ActualReturnDate);
        }
        Result<Updated> ReturnCopyResult = borrowingRecord.ReturnCopy();
        if (ReturnCopyResult.IsError)
        {
            logger.LogError(string.Join(" - ", ReturnCopyResult.Errors));
            return ReturnCopyResult.Errors;
        }
        Result<Updated> SetAvailableResult = borrowingRecord.Copy.SetAvailable();
        if (SetAvailableResult.IsError)
        {
            logger.LogError(string.Join(" - ", SetAvailableResult.Errors));
            return SetAvailableResult.Errors;
        }
        Result<Updated>PayFineResult= fine.PayFine();
        if (PayFineResult.IsError)
        {
            logger.LogError(string.Join(" - ",PayFineResult.Errors));
            return PayFineResult.Errors;
        }
        fine.DomainEvents.Add(new CopyReturned(borrowingRecord.CopyId));
        await context.SaveChangesAsync(cancellationToken);
        await hybridCache.RemoveByTagAsync("Copies", cancellationToken);
        await hybridCache.RemoveByTagAsync("Fines", cancellationToken);
        return Result.Updated;
    }
}
