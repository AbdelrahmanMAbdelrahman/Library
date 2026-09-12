namespace Library.Application.Features.Books.Queries.GetBook;

public sealed class GetBookValidator:AbstractValidator<GetBookQuery>
{
    public GetBookValidator()
    {
        RuleFor(b => b.Id).NotEmpty().WithMessage("Must provide '{PropertyName}'");
    }
}
