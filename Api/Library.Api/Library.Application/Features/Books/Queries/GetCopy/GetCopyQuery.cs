namespace Library.Application.Features.Books.Queries.GetCopy;

public sealed record GetCopyQuery(Guid Id):IRequest<Result<CopyDto>>;
