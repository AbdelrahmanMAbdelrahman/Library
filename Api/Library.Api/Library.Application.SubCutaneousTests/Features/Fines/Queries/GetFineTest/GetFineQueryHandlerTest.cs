

using Docker.DotNet.Models;
using Library.Application.Features.BorrowingRecords.Queries.GetBorrowingRecord;
using Library.Application.Features.Fines.Queries.GetFineById;
using Library.Application.SubCutaneousTests.Common;
using MediatR;

namespace Library.Application.SubCutaneousTests.Features.Fines.Queries.GetFineQueryTest;

public class GetFineQueryHandlerTest(WebAppFactory factory)
{
    private readonly IMediator mediator = factory.CreateMediator();
    [Fact]
    public async Task Handle_ShouldPass_ForValidData()
    {
        var query = new GetFineQuery(Guid.Parse("6ac73dd6-739c-83e8-b06e-d37f9e8da782"));
        var result = await mediator.Send(query);
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value.BorrowingRecordDto);
        Assert.NotNull(result.Value.BorrowingRecordDto.UserInfo);
        Assert.NotNull(result.Value.BorrowingRecordDto.copy);
        Assert.NotNull(result.Value.BorrowingRecordDto.copy.Book);
    }
}
