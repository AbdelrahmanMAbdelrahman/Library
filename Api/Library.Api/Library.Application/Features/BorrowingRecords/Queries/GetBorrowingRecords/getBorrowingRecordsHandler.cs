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
            request.UserName,request.Title,request.Genere,
            request.FromBorrowingDate, request.ToBorrowingDate,
            request.FromDueDate,request.ToDueDate,
            request.FromActualReturnDate, request.ToActualReturnDate
            );
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
        string UserName,string Title,string Genere, DateTime? FromBorrowingDate,
        DateTime? ToBorrowingDate,
        DateTime?  FromDueDate,DateTime? ToDueDate,
        DateTime?FromActualReturnDate,DateTime? ToActualReturnDate)
    {
        if (copyId!=null&&copyId != Guid.Empty) borrowingRecords = borrowingRecords.Where(b => b.CopyId == copyId);
        if (!string.IsNullOrEmpty(UserName))
            borrowingRecords = borrowingRecords.Where(b=>b.AppUser.Name.Contains(UserName));
        if (!string.IsNullOrEmpty(Title))
            borrowingRecords = borrowingRecords.Where(b=>b.Copy.Book.Title.Contains(Title));
        if (!string.IsNullOrEmpty(Genere))
            borrowingRecords = borrowingRecords.Where(b=>b.Copy.Book.Genere.Contains( Genere));
        if (ToBorrowingDate.HasValue) borrowingRecords = borrowingRecords
                .Where(b=>b.BorrowingDate<= ToBorrowingDate );
        if (FromBorrowingDate.HasValue) borrowingRecords = borrowingRecords
                .Where(b=>b.BorrowingDate>= FromBorrowingDate );
        if (FromDueDate.HasValue) borrowingRecords = borrowingRecords
                .Where(b=>b.DueDate>= FromDueDate );
        if (ToDueDate.HasValue) borrowingRecords = borrowingRecords
                .Where(b=>b.DueDate<= ToDueDate);
        if (FromActualReturnDate.HasValue) borrowingRecords = borrowingRecords
                .Where(b=>b.ActualReturnDate >= FromActualReturnDate);
        if (ToActualReturnDate.HasValue) borrowingRecords = borrowingRecords
                .Where(b=>b.ActualReturnDate <= ToActualReturnDate);

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
            "title" => Desc ? borrowingRecords.OrderBy(b => b.Copy.Book.Title) :
            borrowingRecords.OrderByDescending(b => b.Copy.Book.Title),
            "genere" => Desc ? borrowingRecords.OrderBy(b => b.Copy.Book.Genere) :
            borrowingRecords.OrderByDescending(b => b.Copy.Book.Genere),
            "userName" => Desc ? borrowingRecords.OrderBy(b => b.AppUser.Name) :
            borrowingRecords.OrderByDescending(b => b.AppUser.Name),
            _ => Desc ? borrowingRecords.OrderBy(b => b.BorrowingDate) :
            borrowingRecords.OrderByDescending(b => b.BorrowingDate)
        };
    }
}
