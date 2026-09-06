namespace Library.Api.Services;

public sealed class CurrentUser(IHttpContextAccessor accessor) : IUser
{
    public string Id => accessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
}
