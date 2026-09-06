namespace Library.Application.Features.Identity.Queries.GetUserInfo;
public sealed class getUserHandler
    (ILogger<getUserHandler> logger,IIdentityService service): IRequestHandler<getUserQuery, Result<AppUserDto>>
{
    public async Task<Result<AppUserDto>> Handle(getUserQuery request, CancellationToken cancellationToken)
    {
        Result<AppUserDto> getuserResult=await service.GetUserByIdAsync(request.Id??"");
        if (getuserResult.IsError)
        {
            logger.LogError("no user found ");
            return getuserResult.Errors;
        }
        return getuserResult.Value;
    }
}
