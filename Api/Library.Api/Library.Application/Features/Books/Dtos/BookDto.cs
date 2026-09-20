namespace Library.Application.Features.Books.Dtos;

public sealed record BookDto(Guid Id,string Title,string ISBN,string Genere,
    string AdditionalDetails,DateTime PublicationDate,Guid? FileId);

