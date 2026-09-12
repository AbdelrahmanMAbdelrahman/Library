namespace Library.Application.Features.Books.Commands.DeleteBooks;

public sealed class DeleteBookValidator:AbstractValidator<DeleteBookCommand>
{
    public DeleteBookValidator()
    {
        RuleFor(b => b.Id).NotEmpty().WithMessage("Must provide '{PropertyName}'");
    }
}
