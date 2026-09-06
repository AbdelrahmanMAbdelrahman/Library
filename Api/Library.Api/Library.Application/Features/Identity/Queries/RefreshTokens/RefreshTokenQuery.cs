namespace Library.Application.Features.Identity.Queries.RefreshTokens;

public sealed record RefreshTokenQuery(string RefreshToken,string ExpiredAccessToken):
    IRequest<Result<TokenResponse>>;
