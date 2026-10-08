using Library.Application.Features.BorrowingRecords.Queries.GetBorrowingRecord;

namespace Library.Application.SubCutaneousTests.Features.BorrowingRecords.Queries.GetBorrowingRecord;

public class GetBorrowingRecordValidatorTests
{
    private readonly GetBorrowingRecordValidator _validator=new();
    [Fact]
    public void Validate_ShouldPass_ForValidData()
    {
        var query =new  GetBorrowingRecordQuery(Guid.Parse("6ac73dd6-739c-83e8-b06e-d37f9e8da782"));
        var result=_validator.Validate(query);
        Assert.True(result.IsValid);
    }
    [Fact]
    public void Validate_ShouldFail_ForInValidId()
    {
        var query =new  GetBorrowingRecordQuery(Guid.TryParse("6ac73dd6",
            out Guid id)?id:Guid.Empty);
        var result=_validator.Validate(query);
        Assert.False(result.IsValid);
    }
}
