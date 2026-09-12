namespace Library.Application.Features.Books.Commands.DeleteBooks;

public sealed record DeleteBookCommand(Guid Id):IRequest<Result<Deleted>>;
