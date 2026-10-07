using Library.Domain.BorrowingRecords;
using Library.Domain.Common.Results;
using Library.Tests.Common;
using Library.Tests.Common.BorrowingRecords;
namespace Library.Domain.Tests.BorrowingRecords;

public class BorrowingRecordTest
{
    FakeTimeProvider FakeTimeProvider = new FakeTimeProvider();


    [Fact]
    public void Create_ShouldPass_WithValidData()
    {
        DateTime? ActualReturnDate = null;
        var date = FakeTimeProvider.GetUtcNow().LocalDateTime;
        DateTime borrowingDate = date;
        DateTime dueDate = date.AddDays(5);
        Guid appUserId = Guid.NewGuid();
        Guid copyId = Guid.NewGuid();
        BorrowingRecord borrowingRecord =
            BorrowingRecord.Create(copyId, appUserId.ToString(), dueDate, borrowingDate, ActualReturnDate).Value;
        Assert.NotNull(borrowingRecord);
        Assert.Equal(borrowingRecord.BorrowingDate, borrowingDate);
        Assert.Equal(borrowingRecord.DueDate, date.AddDays(5));
        Assert.False(appUserId==Guid.Empty);
        Assert.NotNull(copyId);
    }
    [Fact]
    public void Create_ShouldFail_WithCopyId()
    {
      
           Result < BorrowingRecord > borrowingRecordResult =
            BorrowingRecordFactory.CreateBorrowingRecord(
             DateTime.UtcNow, null, DateTime.UtcNow,
            Guid.NewGuid().ToString(), Guid.Empty
           );
        Assert.False(borrowingRecordResult.IsSuccess);
    }
    [Fact]
    public void Create_ShouldFail_WithUserIdId()
    {
      
           Result < BorrowingRecord > borrowingRecordResult =
            BorrowingRecordFactory.CreateBorrowingRecord(
             DateTime.UtcNow, null, DateTime.UtcNow,
            "", Guid.NewGuid()
           );
        Assert.False(borrowingRecordResult.IsSuccess);
    }
    [Fact]
    public void Create_ShouldFail_WithInvalidBorrowingDateData()
    {
        Result<BorrowingRecord> borrowingRecordResult =
            BorrowingRecordFactory.CreateBorrowingRecord(
             DateTime.UtcNow,DateTime.UtcNow.AddDays(-1),DateTime.UtcNow,
            Guid.NewGuid().ToString(), Guid.NewGuid()
           );
        Assert.False(borrowingRecordResult.IsSuccess);
    }
    [Fact]
    public void Create_ShouldFail_WithInvalidDueDateData()
    {
        Result<BorrowingRecord> borrowingRecordResult =
            BorrowingRecordFactory.CreateBorrowingRecord(
             DateTime.UtcNow,DateTime.UtcNow,DateTime.UtcNow,
            Guid.NewGuid().ToString(), Guid.NewGuid()
           );
        Assert.False(borrowingRecordResult.IsSuccess);
    }

}
