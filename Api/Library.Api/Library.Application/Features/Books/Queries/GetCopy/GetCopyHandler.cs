
namespace Library.Application.Features.Books.Queries.GetCopy;

public sealed class GetCopyHandler (ILogger<GetCopyHandler>logger,
    IAppDbContext context,IUser user): IRequestHandler<GetCopyQuery, Result<CopyDto>>
{
    public async Task<Result<CopyDto>> Handle(GetCopyQuery request, CancellationToken cancellationToken)
    {
        Copy? copy = await context.Copies
            .Include(c=>c.Book)
            .AsNoTracking().FirstOrDefaultAsync(c=>c.Id==request.Id);
        if (copy is null)
        {
            logger.LogError($"invalid copy with id = {request.Id}");
            return ApplicationErrors.CopyNotFound(request.Id);
        }
        
        return copy.ToDto();
    }
}
