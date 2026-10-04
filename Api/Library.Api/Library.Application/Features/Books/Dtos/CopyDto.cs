using Library.Domain.Copies.Enum;

namespace Library.Application.Features.Books.Dtos;

public sealed record CopyDto(Guid Id,BookDto Book,CopyStatus Status);
