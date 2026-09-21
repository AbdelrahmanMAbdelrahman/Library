namespace Library.Application.Features.BorrowingRecords.Queries.GetBorrowingRecords;

public sealed class getBorrowingRecordsHandler(
    ILogger<getBorrowingRecordsHandler>logger,
    IAppDbContext context
    )
    : IRequestHandler<GetBorrowingRecordsQuery, Result<PaginatedList<BorrowingRecordDto>>>
{
    public async Task<Result<PaginatedList<BorrowingRecordDto>>> Handle(GetBorrowingRecordsQuery request,
        CancellationToken cancellationToken)
    {
        IQueryable<BorrowingRecord> borrowingRecords= context.BorrowingRecords
            .Include(br=>br.AppUser)
            .Include(br=>br.Copy)
              .ThenInclude(c=>c.Book)
            .AsNoTracking().AsQueryable();
        borrowingRecords = ApplySort(borrowingRecords,request.SortColumn, request.SortDirection);
        borrowingRecords = ApplySearch(borrowingRecords,request.CopyId,
            request.BorrowingDateFrom,request.BorrowingDateTo,
            request.DueDateFrom,request.DueDateTo);
        Result<PaginatedList<BorrowingRecordDto>> PaginatedBorrowingRecordsResult =
            await PaginatedList<BorrowingRecordDto>
            .Create(borrowingRecords.ToDto(),request.PageNumber,request.PageSize);
        if (PaginatedBorrowingRecordsResult.IsError)
        {
            logger.LogError(string.Join(" - ", PaginatedBorrowingRecordsResult.Errors));
            return PaginatedBorrowingRecordsResult.Errors;
        }
        return PaginatedBorrowingRecordsResult.Value;
    }

    private IQueryable<BorrowingRecord> ApplySearch(
        IQueryable<BorrowingRecord> borrowingRecords, Guid? copyId,
        DateTime? borrowingDateFrom,DateTime? BorrowingDateTo,
        DateTime?  dueDateFrom,DateTime? dueDateTo)
    {
        if (copyId!=null&&copyId != Guid.Empty) borrowingRecords = borrowingRecords.Where(b => b.CopyId == copyId);
        if (borrowingDateFrom.HasValue) borrowingRecords = borrowingRecords
                .Where(b=>b.BorrowingDate>= borrowingDateFrom );
        if (BorrowingDateTo.HasValue) borrowingRecords = borrowingRecords
                .Where(b=>b.BorrowingDate<= BorrowingDateTo );
        if (dueDateFrom.HasValue) borrowingRecords = borrowingRecords
                .Where(b=>b.DueDate>= dueDateFrom );
        if (dueDateTo.HasValue) borrowingRecords = borrowingRecords
                .Where(b=>b.DueDate<= dueDateTo );

        return borrowingRecords;
    }

    private IQueryable<BorrowingRecord> ApplySort(
        IQueryable<BorrowingRecord> borrowingRecords, string SortColumn, string SortDirection)
    {
        bool Desc = SortDirection.Equals("Desc", StringComparison.CurrentCultureIgnoreCase);
        return SortColumn switch
        {
            "DueDate" => Desc ? borrowingRecords.OrderBy(b => b.DueDate) :
            borrowingRecords.OrderByDescending(b => b.DueDate),
            "BorrowingDate" => Desc ? borrowingRecords.OrderBy(b => b.BorrowingDate) :
            borrowingRecords.OrderByDescending(b => b.BorrowingDate),
            _ => Desc ? borrowingRecords.OrderBy(b => b.BorrowingDate) :
            borrowingRecords.OrderByDescending(b => b.BorrowingDate)
        };
    }
}
