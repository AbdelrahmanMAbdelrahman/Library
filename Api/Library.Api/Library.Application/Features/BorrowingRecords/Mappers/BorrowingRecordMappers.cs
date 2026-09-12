using Library.Application.Features.Books.Mapper;
using Library.Application.Features.Identity.Mappers;

namespace Library.Application.Features.BorrowingRecords.Mappers;

public static class BorrowingRecordMappers
{
    public static BorrowingRecordDto ToDto(this BorrowingRecord borrowingRecord)
    {
        return new BorrowingRecordDto(borrowingRecord.Id,borrowingRecord.BorrowingDate,
            borrowingRecord.DueDate,borrowingRecord.ActualReturnDate,
            borrowingRecord.Copy.Book.ToDto(),borrowingRecord.AppUser.ToDto());
    }

    public static IQueryable<BorrowingRecordDto> ToDto(this IQueryable<BorrowingRecord> borrowingRecords)
    {
        return borrowingRecords.Select(b=>b.ToDto());
    }

    
}
