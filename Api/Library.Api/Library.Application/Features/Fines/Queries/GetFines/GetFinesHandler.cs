
using Microsoft.EntityFrameworkCore.Storage;

namespace Library.Application.Features.Fines.Queries.GetFines;

public sealed class GetFinesHandler(
    ILogger<GetFinesHandler>logger,IAppDbContext context
    ) : IRequestHandler<GetFinesQuery, Result<PaginatedList<FineDto>>>
{
    public async Task<Result<PaginatedList<FineDto>>> Handle(GetFinesQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Fine> FinesQuery = context.Fines
            .Include(f=>f.AppUser)
            .Include(f=>f.BorrowingRecord)
              .ThenInclude(br=>br.AppUser)
            .Include(f=>f.BorrowingRecord)
              .ThenInclude(br=>br.Copy)
                .ThenInclude(c=>c.Book)
            .AsNoTracking();
        FinesQuery = ApplyFilter(FinesQuery,request.NumberOfLateDays,request.FineAmount,
            request.UserName,request.Title,request.FromBorrowingDate,
            request.ToBorrowingDate,request.FromDueDate,request.ToDueDate,request.PaymentStatus);
        
        FinesQuery = ApplySorting(FinesQuery, request.SortColumn, request.SortDirection);
        Result<PaginatedList<FineDto>> fines =await PaginatedList<FineDto>
            .Create(FinesQuery.ToDto(),request.PageNumber,request.PageSize);

        return fines.Value;
    }
    private IQueryable<Fine> ApplyFilter(IQueryable<Fine> finesQuery, double? numberOfLateDays, 
        double? fineAmount,string? UserName,string? Title,DateTime? FromBorrowingDate,
        DateTime? ToBorrowingDate,DateTime? FromDueDate,DateTime? ToDueDate,PaymentStatus? paymentStatus)
    {
        if (numberOfLateDays.HasValue)
            finesQuery = finesQuery.Where(f => f.NumberOfLateDays == numberOfLateDays);

        if(fineAmount.HasValue)
            finesQuery=finesQuery.Where(f=>f.FineAmount == fineAmount);
        if(!string.IsNullOrEmpty(UserName))
            finesQuery=finesQuery.Where(f=>f.AppUser.Name!.Contains(UserName));
        if(!string.IsNullOrEmpty(Title))
            finesQuery=finesQuery.Where(f=>f.BorrowingRecord.Copy.Book.Title.Contains(Title));
        if(paymentStatus.HasValue)
            finesQuery=finesQuery.Where(f=>f.PaymentStatus==paymentStatus);
        if(FromBorrowingDate.HasValue)
            finesQuery=finesQuery.Where(f=>f.BorrowingRecord.BorrowingDate>=FromBorrowingDate);
        if(ToBorrowingDate.HasValue)
            finesQuery=finesQuery.Where(f=>f.BorrowingRecord.BorrowingDate<=ToBorrowingDate);
        if(FromDueDate.HasValue)
            finesQuery=finesQuery.Where(f=>f.BorrowingRecord.DueDate>=FromDueDate);
        if(ToDueDate.HasValue)
            finesQuery=finesQuery.Where(f=>f.BorrowingRecord.DueDate<=ToDueDate);

        return finesQuery;
    }
    private IQueryable<Fine> ApplySorting(IQueryable<Fine> finesQuery, string sortColumn, string sortDirection)
    {
        bool Desc = sortDirection.Equals("Desc", StringComparison.CurrentCultureIgnoreCase);
        return sortColumn switch
        {
            "NumberOfLateDays" => Desc ? finesQuery.OrderByDescending(f => f.NumberOfLateDays):
            finesQuery.OrderBy(f=>f.NumberOfLateDays),
            "FineAmount"=> Desc ? finesQuery.OrderByDescending(f => f.FineAmount) :
            finesQuery.OrderBy(f => f.FineAmount),
            "Title"=> Desc ? finesQuery.OrderByDescending(f => f.BorrowingRecord.Copy.Book.Title) :
            finesQuery.OrderBy(f => f.BorrowingRecord.Copy.Book.Title),
            "UserName"=> Desc ? finesQuery.OrderByDescending(f => f.AppUser.Name) :
            finesQuery.OrderBy(f => f.AppUser.Name),
            "BorrowingDate"=> Desc ? finesQuery.OrderByDescending(f => f.BorrowingRecord.BorrowingDate) :
            finesQuery.OrderBy(f => f.BorrowingRecord.BorrowingDate),
            "DueDate"=> Desc ? finesQuery.OrderByDescending(f => f.BorrowingRecord.DueDate) :
            finesQuery.OrderBy(f => f.BorrowingRecord.DueDate),
            _=> Desc ? finesQuery.OrderByDescending(f => f.NumberOfLateDays) :
            finesQuery.OrderBy(f => f.NumberOfLateDays)
        };
    }
}
