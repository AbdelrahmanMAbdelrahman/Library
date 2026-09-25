namespace Library.Application.Features.Fines.Queries.GetFineById;

public sealed record GetFineQuery(Guid Id):IRequest<Result<FineDto>>;
