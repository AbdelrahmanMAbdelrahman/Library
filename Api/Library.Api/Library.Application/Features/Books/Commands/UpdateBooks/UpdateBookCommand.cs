namespace Library.Application.Features.Books.Commands.UpdateBooks;

public sealed record UpdateBookCommand(Guid CopyId, string Title, string ISBN, string Genere,
    string AdditionalDetails, DateTime PublicationDate, Stream? Image,
    string? FileName, string? ContentType) :IRequest<Result<Updated>>;
