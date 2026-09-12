
namespace Library.Application.Features.Fines.Commands.PayFines;

public sealed class PayFineHandler : IRequestHandler<PayFineCommand, Result<Updated>>
{
    public Task<Result<Updated>> Handle(PayFineCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
