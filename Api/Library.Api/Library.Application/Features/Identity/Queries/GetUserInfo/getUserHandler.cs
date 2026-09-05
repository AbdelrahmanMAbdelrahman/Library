namespace Library.Application.Features.Identity.Queries.GetUserInfo;

public sealed class getUserHandler : IRequestHandler<getUserQuery, Result<AppUserDto>>
{
    public Task<Result<AppUserDto>> Handle(getUserQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
