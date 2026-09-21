namespace Library.Application.Features.BorrowingRecords.Commands.CreateBorrowingRecords;

public sealed class CreateBorrowingRecordHandler(IUser user,
    ILogger<CreateBorrowingRecordHandler>logger,
    IAppDbContext context,UserManager<AppUser> userManager) :
    IRequestHandler<CreateBorrowingRecordCommand, Result<BorrowingRecordDto>>
{
    public async Task<Result<BorrowingRecordDto>> Handle(CreateBorrowingRecordCommand request, CancellationToken cancellationToken)
    {
        Copy? copy =await context.Copies.Include(c=>c.Book).FirstOrDefaultAsync(c=>c.Id==request.CopyId);
        if (copy is null || !copy.Available) {
            logger.LogError($"No copy found for this id {request.CopyId}");
            return ApplicationErrors.CopyNotFound(request.CopyId); }

        BorrowingRecord? existingborrowingRecord =
            await context.BorrowingRecords.FirstOrDefaultAsync(br=>br.AppUserId==user.Id,cancellationToken);

        if(existingborrowingRecord!=null &&existingborrowingRecord!.ActualReturnDate is null)
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
        borrowingRecord.AppUser=await userManager.FindByIdAsync(user.Id);
        borrowingRecord.Copy=copy;
        return BorrowingRecordResult.Value.ToDto();
    }
}
