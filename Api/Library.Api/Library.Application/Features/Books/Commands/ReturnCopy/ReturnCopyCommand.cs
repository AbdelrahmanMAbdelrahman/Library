namespace Library.Application.Features.Books.Commands.ReturnCopy;

public sealed record ReturnCopyCommand(Guid CopyId):IRequest<Result<Updated>>;
