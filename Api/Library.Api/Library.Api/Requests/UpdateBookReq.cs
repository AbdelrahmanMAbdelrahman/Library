namespace Library.Api.Requests;

public sealed record UpdateBookReq(string Title, string ISBN, string Genere,
string AdditionalDetails, DateTime PublicationDate, IFormFile? Image);
