namespace Library.Application.Features.Fines.Commands.UpdateFines;

public sealed record UpdateFineCommand(Guid Id, decimal FineAmount, int NumberOfLateDays)
    :IRequest<Result<Updated>>;
