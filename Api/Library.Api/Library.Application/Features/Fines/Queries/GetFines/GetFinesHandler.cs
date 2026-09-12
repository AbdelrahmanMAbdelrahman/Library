
using Microsoft.EntityFrameworkCore.Storage;

namespace Library.Application.Features.Fines.Queries.GetFines;

public sealed class GetFinesHandler(
    ILogger<GetFinesHandler>logger,IAppDbContext context
    ) : IRequestHandler<GetFinesCommand, Result<PaginatedList<FineDto>>>
{
    public async Task<Result<PaginatedList<FineDto>>> Handle(GetFinesCommand request, CancellationToken cancellationToken)
    {
        IQueryable<Fine> FinesQuery = context.Fines.AsNoTracking();
        FinesQuery = ApplyFilter(FinesQuery,request.NumberOfLateDays,request.FineAmount,
            request.BorrowingRecordId);
        FinesQuery = ApplySorting(FinesQuery, request.SortColumn, request.SortDirection);
        Result<PaginatedList<FineDto>> fines =await PaginatedList<FineDto>
            .Create(FinesQuery.ToDto(),request.PageNumber,request.PageSize);

        return fines.Value;
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
            _=> Desc ? finesQuery.OrderByDescending(f => f.NumberOfLateDays) :
            finesQuery.OrderBy(f => f.NumberOfLateDays)
        };
    }

    private IQueryable<Fine> ApplyFilter(IQueryable<Fine> finesQuery, int? numberOfLateDays, 
        decimal? fineAmount, Guid? borrowingRecordId)
    {
        if (numberOfLateDays.HasValue)
            finesQuery = finesQuery.Where(f => f.NumberOfLateDays == numberOfLateDays);

        if(fineAmount.HasValue)
            finesQuery=finesQuery.Where(f=>f.FineAmount == fineAmount);

        if(borrowingRecordId.HasValue)
            finesQuery=finesQuery.Where(f=>f.BorrowingRecordId==borrowingRecordId);

        return finesQuery;
    }
}
