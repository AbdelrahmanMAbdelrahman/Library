using Library.Application.Features.Books.Queries.GetCopy;
using Library.Application.SubCutaneousTests.Common;
using MediatR;
namespace Library.Application.SubCutaneousTests.Features.Books.Queries.GetCopy;

public class GetCopyHandlerTests(WebAppFactory factory)
{
    private readonly IMediator mediator=factory.CreateMediator();
    [Fact]
    public async Task GetCopy_ShouldPass_ForValidData() {
        var query =new GetCopyQuery(Guid.Parse("6ac73dd6-739c-83e8-b06e-d37f9e8da782"));
        var result =await mediator.Send(query);
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
    }
}
