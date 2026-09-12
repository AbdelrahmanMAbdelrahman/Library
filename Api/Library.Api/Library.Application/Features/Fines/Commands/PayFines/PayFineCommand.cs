namespace Library.Application.Features.Fines.Commands.PayFines;

public sealed record PayFineCommand(Guid Id)
    :IRequest<Result<Updated>>;
