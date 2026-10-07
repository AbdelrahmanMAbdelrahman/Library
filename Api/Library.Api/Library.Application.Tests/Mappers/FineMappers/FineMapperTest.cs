using Library.Domain.Books;
using Library.Domain.BorrowingRecords;
using Library.Domain.Copies.Enum;
using Library.Domain.Fines;
using Library.Tests.Common.AppUsers;
using Library.Tests.Common.Books;
using Library.Tests.Common.BorrowingRecords;
using Library.Tests.Common.Fines;

namespace Library.Application.Tests.Mappers.FineMappers;

public class FineMapperTest
{
    [Fact]
    public void ToDto_ShouldPass_ForValidData()
    {
        Fine fine = FineFactory.CreateFine().Value;
        fine.BorrowingRecord = BorrowingRecordFactory.CreateBorrowingRecord(dueDate: DateTime.UtcNow.AddDays(5)).Value;
        Book book = BookFactory.CreateBook().Value;
        fine.BorrowingRecord.Copy=new Domain.Copies.Copy(Guid.NewGuid(),book.Id,CopyStatus.Available);
        fine.BorrowingRecord.Copy.Book = book;
        var user = AppUserFactory.Create().Value;
        fine.BorrowingRecord.AppUser = user;
        fine.AppUser = user;
        Assert.NotNull(fine);
        Assert.NotNull(fine.BorrowingRecord);
        Assert.NotNull(fine.AppUser);
        Assert.NotNull(fine.BorrowingRecord.Copy);
        Assert.NotNull(fine.BorrowingRecord.Copy.Book);

    }
    [Fact]
    public void ToDtoList_ShouldPass_ForValidData()
    {
        List<Fine> fines = [FineFactory.CreateFine().Value];
        var fine = fines[0];
        fine.BorrowingRecord = BorrowingRecordFactory.CreateBorrowingRecord(dueDate: DateTime.UtcNow.AddDays(5)).Value;
        Book book = BookFactory.CreateBook().Value;
        fine.BorrowingRecord.Copy=new Domain.Copies.Copy(Guid.NewGuid(),book.Id,CopyStatus.Available);
        fine.BorrowingRecord.Copy.Book = book;
        var user = AppUserFactory.Create().Value;
        fine.BorrowingRecord.AppUser = user;
        fine.AppUser = user;
        Assert.NotNull(fine);
        Assert.NotNull(fine.BorrowingRecord);
        Assert.NotNull(fine.AppUser);
        Assert.NotNull(fine.BorrowingRecord.Copy);
        Assert.NotNull(fine.BorrowingRecord.Copy.Book);

    }
}
