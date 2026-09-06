namespace Library.Application.Features.Identity.Queries.SignIn;

public sealed class SignInHandler
    (IIdentityService identityService,ITokenProvider tokenProvider,ILogger<SignInHandler> logger):
    IRequestHandler<SignInQuery, Result<TokenResponse>>
{
    public async Task<Result<TokenResponse>> Handle(SignInQuery request, CancellationToken cancellationToken)
    {
       Result<AppUserDto>authenticateResult=await identityService.AuthenticateAsync(request.Email,request.Password);
        if (authenticateResult.IsError)
        {
            logger.LogError("Authentication failed");
            return authenticateResult.Errors;
        }
        Result<TokenResponse> tokenResult =await tokenProvider.GenerateJwtToken(authenticateResult.Value,cancellationToken);
        if (tokenResult.IsError) {
            logger.LogError("Failed to generate token");
            return tokenResult.Errors;
        }
        return tokenResult.Value;
    }
}
