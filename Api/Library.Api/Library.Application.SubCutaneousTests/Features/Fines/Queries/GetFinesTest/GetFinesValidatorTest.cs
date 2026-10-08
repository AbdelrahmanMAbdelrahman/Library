using Library.Application.Features.BorrowingRecords.Queries.GetBorrowingRecords;
using Library.Application.Features.Fines.Queries.GetFineById;
using Library.Application.Features.Fines.Queries.GetFines;
namespace Library.Application.SubCutaneousTests.Features.Fines.Queries.GetFinesTest;

public class GetFinesValidatorTest
{
    private readonly GetFinesValidator validator = new();
    [Fact]
    public void Validate_ShouldPass_ForValidData()
    {
        var query = new GetFinesQuery(PageNumber: 1, PageSize: 10);
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
        var query = new GetFinesQuery(PageNumber: pageNumber, PageSize: pageSize);
        var result = validator.Validate(query);
        Assert.False(result.IsValid);
    }
}
