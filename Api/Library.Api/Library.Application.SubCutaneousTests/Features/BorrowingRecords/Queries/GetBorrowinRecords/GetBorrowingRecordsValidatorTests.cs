using Library.Application.Features.Books.Queries.GetBooks;
using Library.Application.Features.Books.Queries.GetCopies;
using Library.Application.Features.BorrowingRecords.Queries.GetBorrowingRecord;
using Library.Application.Features.BorrowingRecords.Queries.GetBorrowingRecords;

namespace Library.Application.SubCutaneousTests.Features.BorrowingRecords.Queries.GetBorrowinRecords;

public class GetBorrowingRecordsValidatorTests
{
    private readonly GetBorrowingRecordsValidator validator = new();
    [Fact]
    public void Validate_ShouldPass_ForValidData()
    {
        var query = new GetBorrowingRecordsQuery(PageNumber: 1, PageSize: 10);
        var result = validator.Validate(query);
        Assert.True(result.IsValid);
    }
    [Theory]
    [InlineData(0, 10)]
    [InlineData(10, 0)]
    [InlineData(-1, 10)]
    [InlineData(1, -10)]
    public void Validate_ShouldFail_ForInValidData(int pageNumber, int pageSize)
    {
        var query = new GetBorrowingRecordsQuery(PageNumber: pageNumber, PageSize: pageSize);
        var result = validator.Validate(query);
        Assert.False(result.IsValid);
    }
}
