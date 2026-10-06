using Library.Domain.BorrowingRecords;
using Library.Domain.Common.Results;

namespace Library.Tests.Common.BorrowingRecords;

public class BorrowingRecordFactory
{
    public static Result<BorrowingRecord> CreateBorrowingRecord(
        DateTime? actualReturnDate, DateTime? borrowingDate, 
        DateTime? dueDate, string? appUserId, Guid? copyId)
    {
        return BorrowingRecord.Create(
            copyId??Guid.NewGuid(),
            appUserId??Guid.NewGuid().ToString(),
            dueDate??DateTime.UtcNow,
            borrowingDate?? DateTimeOffset.UtcNow.LocalDateTime,
            actualReturnDate
            );
    }
}
