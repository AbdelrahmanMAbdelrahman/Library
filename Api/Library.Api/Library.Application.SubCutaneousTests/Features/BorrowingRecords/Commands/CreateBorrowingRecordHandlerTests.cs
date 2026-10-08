using Library.Application.Common.Interfaces;
using Library.Application.Features.Books.Commands.CreateBooks;
using Library.Application.Features.BorrowingRecords.Commands.CreateBorrowingRecords;
using Library.Application.SubCutaneousTests.Common;
using Library.Domain.Identity.Users;
using Library.Tests.Common.AppUsers;
using Library.Tests.Common.Books;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Library.Application.SubCutaneousTests.Features.BorrowingRecords.Commands;
[Collection(WebAppFactoryCollection.CollectionName)]
public class CreateBorrowingRecordHandlerTests(WebAppFactory factory)
{
    private readonly IMediator mediator = factory.CreateMediator();
    
  
    [Fact]
    public async Task Handle_ShouldPass_ForValidData()
    {
        var command = new CreateBorrowingRecordCommand(Guid.Parse("6ac73dd6-739c-83e8-b06e-d37f9e8da782"),
            DateTime.UtcNow);
        var result = await mediator.Send(command);
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value.copy);
        Assert.NotNull(result.Value.UserInfo);

          
    }
}
