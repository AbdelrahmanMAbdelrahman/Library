namespace Library.Api.Services;


public sealed class CurrentUser(IHttpContextAccessor accessor) : IUser
{
    public string? Id
    {
        get
        {
            return accessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //var user = accessor.HttpContext?.User;

            //if (user?.Identity?.IsAuthenticated != true)
            //    throw new UnauthorizedAccessException("User is not authenticated.");

            //var id = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            //if (string.IsNullOrWhiteSpace(id))
            //    throw new UnauthorizedAccessException(
            //        "User identifier claim was not found.");

            //return id;
        }
    }
}
