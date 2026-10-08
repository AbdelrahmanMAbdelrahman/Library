using Library.Application.Features.BorrowingRecords.Commands.CreateBorrowingRecords;
using Library.Application.Features.Reservations.Commands.CreateReservations;
namespace Library.Application.SubCutaneousTests.Features.ReservationTest.Command.CreateReservationTest
{
    public class CreateBorrowingRecordValidatorTests
    {
        private readonly CreateBorrowingRecordValidator _validator = new();
        [Fact]
        public void ValidateBorrowingRecord_ShouldPass_ForValidData()
        {
            var command = new CreateBorrowingRecordCommand(
                Guid.Parse("6ac73dd6-739c-83e8-b06e-d37f9e8da782"),
                DateTime.UtcNow);
            var result = _validator.Validate(command);
            Assert.True(result.IsValid);
        }
        [Fact]
        public void ValidateBorrowingRecord_ShouldFail_ForInValidCopyId()
        {
            var command = new CreateBorrowingRecordCommand(Guid.TryParse(
                "6ac73dd6", out Guid id) ? id : Guid.Empty,
                DateTime.UtcNow);
            var result = _validator.Validate(command);
            Assert.False(result.IsValid);
        }
        [Fact]
        public void ValidateBorrowingRecord_ShouldFail_ForInValidDate()
        {
            var command = new CreateBorrowingRecordCommand(
                Guid.Parse("6ac73dd6-739c-83e8-b06e-d37f9e8da782"), DateTime.UtcNow.AddDays(1));
            var result = _validator.Validate(command);
            Assert.False(result.IsValid);
        }
    }
}
