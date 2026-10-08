using Library.Application.Features.Books.Queries.GetBooks;

namespace Library.Application.Features.Books.Queries.GetCopies;

public class GetCopiesValidator:AbstractValidator<GetCopiesQuery>
{
    public GetCopiesValidator()
    {
        RuleFor(c => c.PageNumber).Must(p => p > 0 && p < 1000)// small library
            .WithMessage("'{PropertyName}' must be in range '{MinValue}' : '{MaxValue}'");
        RuleFor(c => c.PageSize).Must(p => p > 0 && p < 20)
            .WithMessage("'{PropertyName}' must be in range '{MinValue}' : '{MaxValue}'");
    }
}
