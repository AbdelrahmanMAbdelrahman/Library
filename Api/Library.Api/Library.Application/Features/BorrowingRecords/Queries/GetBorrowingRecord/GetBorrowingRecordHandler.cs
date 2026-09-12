namespace Library.Application.Features.BorrowingRecords.Queries.GetBorrowingRecord;

public sealed class GetBorrowingRecordHandler(
    ILogger<GetBorrowingRecordHandler>logger,
    IAppDbContext context) : IRequestHandler<GetBorrowingRecordCommand, Result<BorrowingRecordDto>>
{
    public async Task<Result<BorrowingRecordDto>> Handle(GetBorrowingRecordCommand request, CancellationToken cancellationToken)
    {
        BorrowingRecord? borrowingRecord = await context.BorrowingRecords
           .Include(b => b.AppUser)
           .Include(b => b.Copy)
             .ThenInclude(c => c.Book)
             .FirstOrDefaultAsync(b=>b.Id==request.Id);

        if(borrowingRecord is null)
        {
            logger.LogError($"No Borrowing record for this id {request.Id}");
            return ApplicationErrors.BorrowingRecordNotFound(request.Id);
        }

        return new BorrowingRecordDto(borrowingRecord.Id,borrowingRecord.BorrowingDate,borrowingRecord.DueDate,
            borrowingRecord.ActualReturnDate,borrowingRecord.Copy.Book.ToDto(),
            borrowingRecord.AppUser.ToDto());
    }
}
