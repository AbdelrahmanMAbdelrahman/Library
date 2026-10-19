namespace Library.Application.Features.Fines.Queries.GetFineById;

public sealed class GetFineHandler(
    ILogger<GetFineHandler>logger,IAppDbContext context,
    IUser user,IIdentityService identity) 
    : IRequestHandler<GetFineQuery, Result<FineDto>>
{
    public async Task<Result<FineDto>> Handle(GetFineQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Fine> query =  context.Fines
            .Include(f=>f.BorrowingRecord)
              .ThenInclude(br=>br.Copy)
                .ThenInclude(c=>c.Book)
            .Include(f=>f.AppUser)
            .Where(f=>f.Id==request.Id);
        if (!await identity.IsInRoleAsync(user.Id ?? "", nameof(Role.Admin), cancellationToken))
            query = query.Where(f => f.AppUserId == user.Id);
        Fine? fine = await query.FirstOrDefaultAsync(cancellationToken);
        if(fine is null)
        {
            logger.LogError($"no fine found for id = {request.Id}");
            return ApplicationErrors.FineNotFound(request.Id);
        }
        return fine.ToDto();

    }
}
