using Library.Domain.Common.Results;
using Library.Domain.Fines;
using Library.Domain.Fines.Enums;

namespace Library.Tests.Common.Fines;

public class FineFactory
{
    public static Result<Fine> CreateFine(
        PaymentStatus? paymentStatus=null, double? fineAmount=null, Guid? borrowingRecordId = null,
        int? numberOfLateDays = null, string? userId = null)
    {
        return Fine.Create(borrowingRecordId??Guid.NewGuid(),userId??Guid.NewGuid().ToString(),numberOfLateDays??1,
            fineAmount??1,paymentStatus??PaymentStatus.UnPaid);
    }
}
