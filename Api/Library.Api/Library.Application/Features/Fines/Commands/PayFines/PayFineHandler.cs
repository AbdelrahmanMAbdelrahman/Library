
namespace Library.Application.Features.Fines.Commands.PayFines;

public sealed class PayFineHandler (IAppDbContext context,ILogger<PayFineHandler>logger) : IRequestHandler<PayFineCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(PayFineCommand request, CancellationToken cancellationToken)
    {
        Fine? fine = await context.Fines.FindAsync(request.Id);
        if(fine is null)
        {
            logger.LogError($"no fine found for id = {request.Id}");
            return ApplicationErrors.FineNotFound(request.Id);
        }
        if (fine.PaymentStatus == PaymentStatus.Paid)
        {
            logger.LogError("Payment already paid");
            return ApplicationErrors.FineAlreadyPaid;
        }
       Result<Updated>PayFineResult= fine.PayFine();
        if (PayFineResult.IsError)
        {
            logger.LogError(string.Join(" - ",PayFineResult.Errors));
            return PayFineResult.Errors;
        }
        await context.SaveChangesAsync(cancellationToken);
        return Result.Updated;
    }
}
