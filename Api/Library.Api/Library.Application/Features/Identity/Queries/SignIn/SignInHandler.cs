
namespace Library.Application.Features.Identity.Queries.SignIn;

public sealed class SignInHandler : IRequestHandler<SignInQuery, Result<TokenResponse>>
{
    public Task<Result<TokenResponse>> Handle(SignInQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
