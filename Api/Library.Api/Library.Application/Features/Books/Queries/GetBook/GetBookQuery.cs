namespace Library.Application.Features.Books.Queries.GetBook;

public sealed record GetBookQuery(Guid Id):IRequest<Result<BookDto>>;
