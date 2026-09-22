namespace Library.Application.Features.Books.Commands.ReturnCopy;

public sealed class ReturnCopyHandler 
    (IAppDbContext context,IUser user,ILogger<ReturnCopyHandler>logger)
    : IRequestHandler<ReturnCopyCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(ReturnCopyCommand request, CancellationToken cancellationToken)
    {
        //BorrowingRecord? borrowingRecord =await context.BorrowingRecords.AsNoTracking()
        //    .FirstOrDefaultAsync(br=>br.CopyId==request.CopyId&&br.AppUserId==request.UserId,cancellationToken);
        //if(borrowingRecord is null)
        //{
        //    logger.LogError($"No Borrowing Record Found for user with id = {request.UserId} and copy with id = {request.CopyId}");
        //    return ApplicationErrors.CopyNotFound(request.CopyId);
        //}
        Copy? copy = await context.Copies.FindAsync(request.CopyId,cancellationToken);
        if (copy is null)
        {
            logger.LogError($"No copy Found for  id = {request.CopyId}");
            return ApplicationErrors.CopyNotFound(request.CopyId);
        }
      Result<Updated> UpdateAvailabilityResult=  copy!.SetAvailable();
        if (UpdateAvailabilityResult.IsError)
        {
            logger.LogError(string.Join(" - ", UpdateAvailabilityResult.Errors));
            return UpdateAvailabilityResult.Errors;
        }
        await context.SaveChangesAsync(cancellationToken);
        return Result.Updated;
    }
}
