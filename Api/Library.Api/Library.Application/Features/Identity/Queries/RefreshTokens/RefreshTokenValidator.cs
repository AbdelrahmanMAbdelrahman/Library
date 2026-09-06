namespace Library.Application.Features.Identity.Queries.RefreshTokens;

public sealed class RefreshTokenValidator:AbstractValidator<RefreshTokenQuery>
{
    public RefreshTokenValidator()
    {
        RuleFor(rt=>rt.ExpiredAccessToken).NotEmpty().WithMessage("Must Provide '{PropertyName}'");
        RuleFor(rt=>rt.RefreshToken).NotEmpty().WithMessage("Must Provide '{PropertyName}'");
    }
}
