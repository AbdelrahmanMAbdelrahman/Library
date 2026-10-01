using Microsoft.Extensions.Caching.Hybrid;

namespace Library.Application.Features.BorrowingRecords.Commands.CreateBorrowingRecords;

public sealed class CreateBorrowingRecordHandler(IUser user,
    ILogger<CreateBorrowingRecordHandler>logger,
    HybridCache hybridCache,
    IAppDbContext context,UserManager<AppUser> userManager) :
    IRequestHandler<CreateBorrowingRecordCommand, Result<BorrowingRecordDto>>
{
    public async Task<Result<BorrowingRecordDto>> Handle(CreateBorrowingRecordCommand request, CancellationToken cancellationToken)
    {
        Copy? copy =await context.Copies.Include(c=>c.Book).FirstOrDefaultAsync(c=>c.Id==request.CopyId);
        if (copy is null || !copy.Available) {
            logger.LogError($"No copy found for this id {request.CopyId}");
            return ApplicationErrors.CopyNotFound(request.CopyId); }

        bool hasNotReturnedCopy=
            await context.BorrowingRecords.Where(br=>br.AppUserId==user.Id)
            .AnyAsync(br=>br.ActualReturnDate==null);
        if(hasNotReturnedCopy)
        {
            logger.LogError($"there are one or more borrowing record not returned");
            return ApplicationErrors.RecordAlreadyBorrowed;
        }

        DateTime DueDate = request.BorrowingDate.AddDays(LibrarySettings.DefaultBorrowingDays);
        Result<BorrowingRecord> BorrowingRecordResult = BorrowingRecord
            .Create(request.CopyId,user.Id,DueDate,request.BorrowingDate,null);

        if (BorrowingRecordResult.IsError) {
            logger.LogError(string.Join(" - ",BorrowingRecordResult.Errors));
            return BorrowingRecordResult.Errors; }
        BorrowingRecord borrowingRecord = BorrowingRecordResult.Value;
       Result<Updated> UpdateResult= copy.SetUnAvailable();

        if (UpdateResult.IsError)
        {
            logger.LogError(string.Join(" - ",UpdateResult.Errors));
            return UpdateResult.Errors;
        }

        await context.BorrowingRecords.AddAsync(borrowingRecord,cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        await hybridCache.RemoveByTagAsync("BorrowingRecords", cancellationToken);
        borrowingRecord.AppUser=await userManager.FindByIdAsync(user.Id);
        borrowingRecord.Copy=copy;
        return BorrowingRecordResult.Value.ToDto();
    }
}
