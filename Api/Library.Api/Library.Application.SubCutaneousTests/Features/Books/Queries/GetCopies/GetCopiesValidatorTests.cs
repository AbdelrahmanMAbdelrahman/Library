using Library.Application.Features.Books.Queries.GetBooks;
using Library.Application.Features.Books.Queries.GetCopies;

namespace Library.Application.SubCutaneousTests.Features.Books.Queries.GetCopies;

public class GetCopiesValidatorTests
{
    private readonly GetCopiesValidator validator = new();
    [Fact]
    public void Validate_ShouldPass_ForValidData()
    {
        var query = new GetCopiesQuery(PageNumber:1,PageSize:10);
        var result = validator.Validate(query);
        Assert.True(result.IsValid);
    }
    [Theory]
    [InlineData(0,10)]
    [InlineData(10,0)]
    [InlineData(-1,10)]
    [InlineData(1,-10)]
    public void Validate_ShouldFail_ForInValidData(int pageNumber,int pageSize)
    {
        var query = new GetCopiesQuery(PageNumber:pageNumber,PageSize:pageSize);
        var result = validator.Validate(query);
        Assert.False(result.IsValid);
    }
}
