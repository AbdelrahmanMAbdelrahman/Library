using Library.Application.Common.Interfaces;
using Library.Application.Features.Books.Queries.GetBooks;
using Library.Application.SubCutaneousTests.Common;
using MediatR;
using QuestPDF.Helpers;
using System.Threading.Tasks;

namespace Library.Application.SubCutaneousTests.Features.Books.Queries.GetCopies;

public class GetCopiesHandlerTests(WebAppFactory factory)
{
    private readonly IMediator mediator = factory.CreateMediator();
   
    [Fact]
    public async Task GetCopies_ShouldPass_ForValidData()
    {
        var query = new GetCopiesQuery(PageNumber:1,PageSize:10);
        var result = await mediator.Send(query);
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.NotNull(result.Value.Items);
        Assert.True(result.Value.Items.Count==10);
    }
}
