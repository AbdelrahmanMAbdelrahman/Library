using FluentValidation;

namespace Library.Application.Features.Identity.Queries.SignIn;

public sealed class SignInValidator:AbstractValidator<SignInQuery>
{
    public SignInValidator()
    {
        RuleFor(s => s.Email).NotEmpty().WithMessage("Must Provide '{PropertyName}'")
            .Length(10,50).WithMessage("'{PropertyName}' Length between '{MinLength}' , '{MaxLength}'");

        RuleFor(s => s.Password).NotEmpty().WithMessage("Must Provide '{PropertyName}'")
        .Length(6, 50).WithMessage("'{PropertyName}' Length between '{MinLength}' , '{MaxLength}'");
    }
}
