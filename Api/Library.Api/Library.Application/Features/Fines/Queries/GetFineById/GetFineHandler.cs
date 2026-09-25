
namespace Library.Application.Features.Fines.Queries.GetFineById;

public sealed class GetFineHandler(ILogger<GetFineHandler>logger,IAppDbContext context) 
    : IRequestHandler<GetFineQuery, Result<FineDto>>
{
    public async Task<Result<FineDto>> Handle(GetFineQuery request, CancellationToken cancellationToken)
    {
        Fine? fine = await context.Fines
            .Include(f=>f.BorrowingRecord)
              .ThenInclude(br=>br.Copy)
                .ThenInclude(c=>c.Book)
            .Include(f=>f.AppUser)
            .FirstOrDefaultAsync(f=>f.Id==request.Id);
        if(fine is null)
        {
            logger.LogError($"no fine found for id = {request.Id}");
            return ApplicationErrors.FineNotFound(request.Id);
        }
        return fine.ToDto();

    }
}
