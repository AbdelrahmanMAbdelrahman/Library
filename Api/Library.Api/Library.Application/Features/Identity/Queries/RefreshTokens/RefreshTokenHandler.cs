
using Library.Application.Common.Errors;
using Library.Domain.Identity.RefreshTokens;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.Features.Identity.Queries.RefreshTokens
{
    public sealed class RefreshTokenHandler 
        (ILogger<RefreshTokenHandler>logger,IIdentityService identityService,
        IAppDbContext context,ITokenProvider provider
        ): IRequestHandler<RefreshTokenQuery, Result<TokenResponse>>
    {
        public async Task<Result<TokenResponse>> Handle(RefreshTokenQuery request, CancellationToken cancellationToken)
        {
            ClaimsPrincipal claimsPrincipal = provider.GetPrincipalFromExpiredToken(request.ExpiredAccessToken);
            if(claimsPrincipal is null)
            {
                logger.LogError("invalid Expired access Token");
                return ApplicationErrors.InvalidExpireAccessToken;
            }
            string? userId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId)) {
                logger.LogError("no user found ");
                return ApplicationErrors.UserNotFound;
            }

           Result<AppUserDto> getUserResult =await identityService.GetUserByIdAsync(userId);
            if (getUserResult.IsError)
            {
                logger.LogError("no user found ");
                return getUserResult.Errors;
            }

            RefreshToken? refreshToken = await context.RefreshTokens
                .FirstOrDefaultAsync(rt => rt.UserId == userId && rt.ExpireOn < DateTime.UtcNow);
            if (refreshToken is null) {
                logger.LogError("Can't find Refresh Token");
                return ApplicationErrors.RefreshTokenNotFound;
            }
            Result<TokenResponse> GenerateTokenResult =await provider.GenerateJwtToken(getUserResult.Value,cancellationToken);
            if (GenerateTokenResult.IsError)
            {
                logger.LogError("can't generate token");
                return GenerateTokenResult.Errors;
            }
            
            return GenerateTokenResult.Value;
        }
    }
}
