namespace Library.Application.Features.Books.Commands.CreateBooks;

public sealed record CreateBookCommand(string Title,string ISBN,string Genere,
    string AdditionalDetails,DateTime PublicationDate,int NumberOfCopies,Stream Image,
    string FileName,string ContentType):IRequest<Result<BookDto>>;
