namespace Library.Application.Features.Books.Commands.UpdateBooks;

public sealed class UpdateBookValidator:AbstractValidator<UpdateBookCommand>
{
    public UpdateBookValidator()
    {

        RuleFor(b => b.Id).NotEmpty().WithMessage("Must Provide '{PropertyName}'");
    RuleFor(b => b.Title).NotEmpty().WithMessage("Must Provide '{PropertyName}'")
            .Length(3,255).WithMessage(
            "'{PropertyName}' Must Has At least '{MinLenth} chars , '{MaxLength}' chars at most ");
    RuleFor(b => b.ISBN).NotEmpty().WithMessage("Must Provide '{PropertyName}'")
            .Length(3,255).WithMessage(
            "'{PropertyName}' Must Has At least '{MinLenth} chars , '{MaxLength}' chars at most ");
    RuleFor(b => b.Genere).NotEmpty().WithMessage("Must Provide '{PropertyName}'")
            .Length(3,255).WithMessage(
            "'{PropertyName}' Must Has At least '{MinLenth} chars , '{MaxLength}' chars at most ");
    RuleFor(b => b.AdditionalDetails).NotEmpty().WithMessage("Must Provide '{PropertyName}'")
            .Length(3,1000).WithMessage(
            "'{PropertyName}' Must Has At least '{MinLenth} chars , '{MaxLength}' chars at most ");
    RuleFor(b => b.PublicationDate).Must(d => d <= DateTime.UtcNow)
            .WithMessage("Provide a valid '{PropertyName}");
    }
}
