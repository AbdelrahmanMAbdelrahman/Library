namespace Library.Application.Features.Books.Commands.DeleteBooks;

public sealed record DeleteCopyCommand(Guid CopyId):IRequest<Result<Deleted>>;
