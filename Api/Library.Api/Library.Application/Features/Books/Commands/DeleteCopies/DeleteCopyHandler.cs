
namespace Library.Application.Features.Books.Commands.DeleteBooks;

public sealed class DeleteCopyHandler(ILogger<DeleteCopyHandler>logger,IAppDbContext context,IUser user) 
    : IRequestHandler<DeleteCopyCommand, Result<Deleted>>
{
    public async Task<Result<Deleted>> Handle(DeleteCopyCommand request, CancellationToken cancellationToken)
    {
        Copy? copy = await context.Copies.AsNoTracking().FirstOrDefaultAsync(
            c=>c.Id==request.CopyId);
        if (copy is null)
        {
            logger.LogError($"no copy found for id = {request.CopyId}");
            return ApplicationErrors.BookNotFound(request.CopyId);
        }
        BorrowingRecord? borrowingRecord = await context.BorrowingRecords.AsNoTracking()
            .FirstOrDefaultAsync(br => br.AppUserId == user.Id && br.CopyId == request.CopyId);
        if (borrowingRecord is null)
        {
            logger.LogError($"no copy found for you with id = {request.CopyId}" );
            return ApplicationErrors.BorrowingRecordNotFound(request.CopyId);
        }

        context.Copies.Remove(copy);

        return Result.Deleted;

    }
}
