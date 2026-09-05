using FluentValidation;

namespace Library.Application.Features.Identity.Requests.SignUp;

public sealed class SignUpValidator:AbstractValidator<SignUpCommand>
{
    public SignUpValidator()
    {
        RuleFor(s => s.Email).NotEmpty().WithMessage("Must Provide '{PropertyName}'")
            .Length(10, 50).WithMessage("'{PropertyName}' Length between '{MinLength}' , '{MaxLength}'");

        RuleFor(s => s.Password).NotEmpty().WithMessage("Must Provide '{PropertyName}'")
        .Length(6, 50).WithMessage("'{PropertyName}' Length between '{MinLength}' , '{MaxLength}'");

        RuleFor(s => s.Name).NotEmpty().WithMessage("Must Provide '{PropertyName}'")
            .Length(10, 50).WithMessage("'{PropertyName}' Length between '{MinLength}' , '{MaxLength}'");

        RuleFor(s => s.UserName).NotEmpty().WithMessage("Must Provide '{PropertyName}'")
        .Length(10, 50).WithMessage("'{PropertyName}' Length between '{MinLength}' , '{MaxLength}'");
        RuleFor(s => s.Phone).NotEmpty().WithMessage("Must Provide '{PropertyName}'")
            .Length(11, 15).WithMessage("'{PropertyName}' Length between '{MinLength}' , '{MaxLength}'");

    }
}
