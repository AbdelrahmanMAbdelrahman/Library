namespace Library.Api.Requests;

public sealed record BookReq(string Title, string ISBN, string Genere,
string AdditionalDetails, DateTime PublicationDate, int NumberOfCopies,IFormFile? Image);
