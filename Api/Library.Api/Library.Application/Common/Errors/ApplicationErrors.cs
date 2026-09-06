
namespace Library.Application.Common.Errors;

public sealed class ApplicationErrors
{
    internal static Result<TokenResponse> InvalidExpireAccessToken;
    internal static Result<TokenResponse> UserNotFound;
    internal static Result<TokenResponse> RefreshTokenNotFound;
}
