namespace Library.Application.Features.Identity.Queries.SignIn;

public sealed record SignInQuery(string Email,string Password):IRequest<Result<TokenResponse>>;
