using Docker.DotNet.Models;
using Library.Application.Features.BorrowingRecords.Queries.GetBorrowingRecords;
using Library.Application.Features.Fines.Queries.GetFines;
using Library.Application.SubCutaneousTests.Common;
using MediatR;

namespace Library.Application.SubCutaneousTests.Features.Fines.Queries.GetFinesTest;

public class GetFinesHandlerTest(WebAppFactory factory)
{
    private readonly IMediator mediator = factory.CreateMediator();
    [Fact]
    public async Task Handle_ShouldPass_ForValidData()
    {
        var query = new GetFinesQuery(PageNumber: 1, PageSize: 10);
        var result = await mediator.Send(query);
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.NotNull(result.Value.Items);
        Assert.True(result.Value.Items.Count == 10);
    }
}
