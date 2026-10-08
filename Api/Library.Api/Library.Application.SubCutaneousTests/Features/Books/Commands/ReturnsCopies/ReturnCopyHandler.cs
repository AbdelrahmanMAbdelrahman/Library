using Library.Application.Common.Interfaces;
using Library.Application.Features.Books.Commands.DeleteBooks;
using Library.Application.Features.Books.Commands.ReturnCopy;
using Library.Application.SubCutaneousTests.Common;
using MediatR;
using System.Threading.Tasks;

namespace Library.Application.SubCutaneousTests.Features.Books.Commands.ReturnsCopies;

public class ReturnCopyHandler(WebAppFactory factory)
{
    private readonly IMediator mediator = factory.CreateMediator();
    private readonly IAppDbContext context =factory.CreateDbContext();
    [Fact]
    public async Task Handle_ShouldPass_ForValidData()
    {
        ReturnCopyCommand command =
            new ReturnCopyCommand(Guid.TryParse("6ac73dd6-739c-83e8-b06e-d37f9e8da782", out Guid id)
            ? id : Guid.Empty);
        var result = await mediator.Send(command);
        Assert.True(result.IsSuccess);
    }
    [Fact]
    public async Task Handle_ShouldFail_ForInValidData()
    {
        ReturnCopyCommand command =
            new ReturnCopyCommand(Guid.TryParse("6ac73dd6", out Guid id)
            ? id : Guid.Empty);
        var result = await mediator.Send(command);
        Assert.False(result.IsSuccess);
    }
}
