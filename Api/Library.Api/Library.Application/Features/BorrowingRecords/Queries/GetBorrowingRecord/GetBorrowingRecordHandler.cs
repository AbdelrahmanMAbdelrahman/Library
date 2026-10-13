namespace Library.Application.Features.BorrowingRecords.Queries.GetBorrowingRecord;

public sealed class GetBorrowingRecordHandler(
    ILogger<GetBorrowingRecordHandler>logger,
    IIdentityService identity,
    IUser user,
    IAppDbContext context) : IRequestHandler<GetBorrowingRecordQuery, Result<BorrowingRecordDto>>
{
    public async Task<Result<BorrowingRecordDto>> Handle(GetBorrowingRecordQuery request, CancellationToken cancellationToken)
    {
        IQueryable<BorrowingRecord> query =  context.BorrowingRecords
           .Include(b => b.AppUser)
           .Include(b => b.Copy)
             .ThenInclude(c => c.Book)
             .Where(b=>b.Id==request.Id);
        if (!await identity.IsInRoleAsync(user.Id??"", "Admin")) {
           query= query.Where(br => br.AppUserId == user.Id);
        }
        BorrowingRecord? borrowingRecord=await query.FirstOrDefaultAsync(cancellationToken);
        if(borrowingRecord is null)
        {
            logger.LogError($"No Borrowing record for this id {request.Id}");
            return ApplicationErrors.BorrowingRecordNotFound(request.Id);
        }

        return new BorrowingRecordDto(borrowingRecord.Id,borrowingRecord.BorrowingDate,borrowingRecord.DueDate,
            borrowingRecord.ActualReturnDate,borrowingRecord.Copy.ToDto(),
            borrowingRecord.AppUser.ToDto());
    }
}
