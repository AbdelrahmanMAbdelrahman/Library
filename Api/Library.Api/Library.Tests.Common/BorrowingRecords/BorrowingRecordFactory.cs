using Library.Domain.BorrowingRecords;
using Library.Domain.Common.Results;

namespace Library.Tests.Common.BorrowingRecords;

public class BorrowingRecordFactory
{
    public static Result<BorrowingRecord> CreateBorrowingRecord(
        DateTime? actualReturnDate=null, DateTime? borrowingDate = null, 
        DateTime? dueDate=null, string? appUserId=null, Guid? copyId = null)
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
