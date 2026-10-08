using Library.Application.Features.BorrowingRecords.Queries.GetBorrowingRecord;
using Library.Application.Features.Reservations.Commands.CreateReservations;
using Library.Application.Features.Reservations.Queries.GetReservations;
using Library.Application.SubCutaneousTests.Common;
using MediatR;

namespace Library.Application.SubCutaneousTests.Features.ReservationTest.Command.CreateReservationTest
{
    [Collection(WebAppFactoryCollection.CollectionName)]
    public class CreateReservationHandlerTest(WebAppFactory factory)
    {
        private readonly IMediator mediator = factory.CreateMediator();
        [Fact]
        public async Task Handle_ShouldPass_ForValidData()
        {
            var command = new CreateReservationCommand(Guid.Parse("6ac73dd6-739c-83e8-b06e-d37f9e8da782"));
            var result = await mediator.Send(command);
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value.Copy);
            Assert.NotNull(result.Value.UserInfo);
        }
    }
}
