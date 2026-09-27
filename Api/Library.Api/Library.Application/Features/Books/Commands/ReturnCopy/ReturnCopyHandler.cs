namespace Library.Application.Features.Books.Commands.ReturnCopy;

public sealed class ReturnCopyHandler 
    (IAppDbContext context,IUser user,ILogger<ReturnCopyHandler>logger)
    : IRequestHandler<ReturnCopyCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(ReturnCopyCommand request, CancellationToken cancellationToken)
    {
       
        Copy? copy = await context.Copies.FindAsync(request.CopyId,cancellationToken);
        if (copy is null)
        {
            logger.LogError($"No copy Found for  id = {request.CopyId}");
            return ApplicationErrors.CopyNotFound(request.CopyId);
        }
        if (copy.Available)
        {
            logger.LogError("copy with id {Id} already avaiable",copy.Id);
            return ApplicationErrors.CopyAlreadyAvailable(copy.Id);
        }
        Fine? fine = await context.Fines.FirstOrDefaultAsync(f=>f.BorrowingRecord.CopyId==copy.Id,cancellationToken);
        if(fine is not null)
        {
            logger.LogError("must pay fine with id = {Id} first", fine.Id);
            return ApplicationErrors.FineExist(fine.Id);
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
