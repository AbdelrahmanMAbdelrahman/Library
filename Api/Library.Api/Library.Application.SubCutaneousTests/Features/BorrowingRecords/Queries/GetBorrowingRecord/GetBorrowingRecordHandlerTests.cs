using Library.Application.Features.BorrowingRecords.Queries.GetBorrowingRecord;
using Library.Application.SubCutaneousTests.Common;
using MediatR;


namespace Library.Application.SubCutaneousTests.Features.BorrowingRecords.Queries.GetBorrowingRecord;

[Collection(WebAppFactoryCollection.CollectionName)]
public class GetBorrowingRecordHandlerTests(WebAppFactory factory)
{
    private readonly IMediator mediator = factory.CreateMediator();
    [Fact]
    public async Task Handle_ShouldPass_ForValidData()
    {
        var query = new GetBorrowingRecordQuery(Guid.Parse("6ac73dd6-739c-83e8-b06e-d37f9e8da782"));
        var result =await mediator.Send(query);
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value.copy);
        Assert.NotNull(result.Value.UserInfo);
    }
}
