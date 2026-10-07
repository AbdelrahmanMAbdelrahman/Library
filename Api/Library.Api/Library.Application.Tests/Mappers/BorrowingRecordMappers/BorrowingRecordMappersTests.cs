

using Library.Application.Features.BorrowingRecords.Mappers;
using Library.Domain.Books;
using Library.Domain.BorrowingRecords;
using Library.Tests.Common.AppUsers;
using Library.Tests.Common.Books;
using Library.Tests.Common.BorrowingRecords;

namespace Library.Application.Tests.Mappers.BorrowingRecordMappers;

public class BorrowingRecordMappersTests
{
    [Fact]
    public void ToDto_ShouldPass_ForValidData()
    {
        BorrowingRecord record = BorrowingRecordFactory.CreateBorrowingRecord(dueDate: DateTime.UtcNow.AddDays(5)).Value;
        record.AppUser = AppUserFactory.Create().Value;
        Book book=BookFactory.CreateBook().Value;
        record.Copy = new Domain.Copies.Copy(Guid.NewGuid(),book.Id,Domain.Copies.Enum.CopyStatus.Available);
        record.Copy.Book = book;
        var dto = record.ToDto();
        Assert.NotNull(dto);
        Assert.NotNull(dto.UserInfo);
        
    }
    [Fact]
    public void ToDtoList_ShouldPass_ForValidData()
    {
        BorrowingRecord record = BorrowingRecordFactory.CreateBorrowingRecord(dueDate:DateTime.UtcNow.AddDays(5)).Value;
        record.AppUser = AppUserFactory.Create().Value;
        Book book=BookFactory.CreateBook().Value;
        record.Copy = new Domain.Copies.Copy(Guid.NewGuid(),book.Id,Domain.Copies.Enum.CopyStatus.Available);
        record.Copy.Book = book;
        List<BorrowingRecord> borrowingRecords = [record];
        var dtos = borrowingRecords.ToDto();
        Assert.NotNull(dtos);
        Assert.NotNull(dtos[0].UserInfo);
        Assert.NotNull(dtos[0].copy);
        Assert.NotNull(dtos[0].copy.Book);
        
    }
}
