namespace Library.Application.Features.Books.Dtos;

public sealed record CopyDto(Guid Id,BookDto book,bool IsAvailable);
