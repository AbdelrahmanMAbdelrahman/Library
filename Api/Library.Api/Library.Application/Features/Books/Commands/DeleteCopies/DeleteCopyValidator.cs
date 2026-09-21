namespace Library.Application.Features.Books.Commands.DeleteBooks;

public sealed class DeleteCopyValidator:AbstractValidator<DeleteCopyCommand>
{
    public DeleteCopyValidator()
    {
        RuleFor(b => b.CopyId).NotEmpty().WithMessage("Must provide '{PropertyName}'");
    }
}
