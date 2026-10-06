using Library.Domain.Common.Results;
using Library.Domain.Fines;
using Library.Domain.Fines.Enums;

namespace Library.Tests.Common.Fines;

public class FineFactory
{
    public static Result<Fine> CreateFine(
        PaymentStatus? paymentStatus, double? fineAmount, Guid? borrowingRecordId,
        int? numberOfLateDays, string? userId)
    {
        return Fine.Create(borrowingRecordId??Guid.NewGuid(),userId??Guid.NewGuid().ToString(),numberOfLateDays??1,
            fineAmount??1,paymentStatus??PaymentStatus.UnPaid);
    }
}
