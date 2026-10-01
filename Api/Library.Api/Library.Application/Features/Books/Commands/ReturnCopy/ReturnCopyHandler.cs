using Library.Domain.BorrowingRecords;
using Library.Domain.Copies.Events;
using Microsoft.Extensions.Caching.Hybrid;

namespace Library.Application.Features.Books.Commands.ReturnCopy;

public sealed class ReturnCopyHandler 
    (IAppDbContext context,IUser user,
    HybridCache hybridCache,ILogger<ReturnCopyHandler>logger)
    : IRequestHandler<ReturnCopyCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(ReturnCopyCommand request, CancellationToken cancellationToken)
    {
       
        BorrowingRecord? borrowingRecord = await context.BorrowingRecords
            .Include(br => br.Copy)
            .FirstOrDefaultAsync(br => br.CopyId == request.CopyId, cancellationToken);
        if (borrowingRecord is null)
        {
            logger.LogError($"No borrowing record found for Copy Id = ${request.CopyId}");
            return ApplicationErrors.BorrowingRecordNotFound(borrowingRecord.Id);
        }
        //Copy? copy = await context.Copies.FindAsync(request.CopyId,cancellationToken);
        //if (copy is null)
        //{
        //    logger.LogError($"No copy Found for  id = {request.CopyId}");
        //    return ApplicationErrors.CopyNotFound(request.CopyId);
        //}
        if (borrowingRecord.Copy.Available)
        {
            logger.LogError("copy with id {Id} already avaiable",borrowingRecord.CopyId);
            return ApplicationErrors.CopyAlreadyAvailable(borrowingRecord.CopyId);
        }
        Fine? fine = await context.Fines.FirstOrDefaultAsync(
            f=>f.BorrowingRecord.CopyId==borrowingRecord.CopyId,cancellationToken);
        if(fine is not null)
        {
            logger.LogError("must pay fine with id = {Id} first", fine.Id);
            return ApplicationErrors.FineExist(fine.Id);
        }
      Result<Updated> UpdateAvailabilityResult=  borrowingRecord.Copy!.SetAvailable();
        if (UpdateAvailabilityResult.IsError)
        {
            logger.LogError(string.Join(" - ", UpdateAvailabilityResult.Errors));
            return UpdateAvailabilityResult.Errors;
        }
        Result<Updated> ReturnCopyResult = borrowingRecord.ReturnCopy();
        if (ReturnCopyResult.IsError)
        {
            logger.LogError(string.Join(" - ", ReturnCopyResult.Errors));
            return ReturnCopyResult.Errors;
        }
        borrowingRecord.Copy.DomainEvents.Add(new CopyReturned(borrowingRecord.CopyId));
        logger.LogInformation(
    "Copy {CopyId} has {Count} domain events",
    borrowingRecord.CopyId,
    borrowingRecord.Copy.DomainEvents.Count);
        await context.SaveChangesAsync(cancellationToken);
        await hybridCache.RemoveByTagAsync("Copies", cancellationToken);
        return Result.Updated;
    }
}
