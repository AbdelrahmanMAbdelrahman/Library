

namespace Library.Application.Common.Interfaces;

public interface ITokenProvider
{
    Task<Result<TokenResponse>> GenerateJwtToken(AppUserDto appUser, CancellationToken ct);
    ClaimsPrincipal GetPrincipalFromExpiredToken(string Token);
}
