namespace Library.Application.Features.Fines.Queries.GetFineById;

public sealed record GetFineCommand(Guid Id):IRequest<Result<FineDto>>;
