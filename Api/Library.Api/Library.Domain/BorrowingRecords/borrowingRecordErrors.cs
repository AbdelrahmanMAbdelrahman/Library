
namespace Library.Domain.BorrowingRecords;

internal static class borrowingRecordErrors
{
    internal static Error InvalidCopyId=>Error.Validation("borrowingRecordErrors.InvalidCopyId",
        "Provide a valid copy id");
    internal static Error InvalidUserId => Error.Validation("borrowingRecordErrors.InvalidUserId", 
        "Provide a valid user id");
    internal static Error InvalidDueDate => Error.Validation("borrowingRecordErrors.InvalidDueDate", 
        "Provide a valid due date");
    internal static Error InalidBorrowingDate => Error.Validation("borrowingRecordErrors.InalidBorrowingDate", 
        "Provide a valid borrowing date");
    internal static Error InvalidActualReturnDate => 
        Error.Validation("borrowingRecordErrors.InvalidActualReturnDate", "Provide a valid actual return date");
}
