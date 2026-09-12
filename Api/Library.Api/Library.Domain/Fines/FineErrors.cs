namespace Library.Domain.Fines;

internal sealed class FineErrors
{
    internal static Error InvalidBorrowingRecordId => 
        Error.Validation("FineErrors.InvalidBorrowingRecordId", "provide a valid Invalid Borrowing Record Id");
    internal static Error InvalidUserId => 
        Error.Validation("FineErrors.InvalidUserId", "provide a valid Invalid User Id");
    internal static Error InvalidNumberOfLateDays => 
        Error.Validation("FineErrors.InvalidNumberOfLateDays", "provide a valid Invalid Number Of Late Days");
    internal static Error InvalidFineAmount => 
        Error.Validation("FineErrors.InvalidFineAmount", "provide a valid Invalid Fine Amount");
    internal static Error InvalidPaymentStatus =>
        Error.Validation("FineErrors.InvalidPaymentStatus", "provide a valid Invalid Payment Status");
}
