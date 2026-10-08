using Library.Application.Features.Reservations.Queries.GetReservations;
namespace Library.Application.SubCutaneousTests.Features.ReservationTest.Queries.GetReservationsTest
{
    public class GetReservationsValidatorTest
    {
        private readonly GetReservationsValidator validator = new();
        [Fact]
        public void Validate_ShouldPass_ForValidData()
        {
            var query = new GetReservationsQuery(PageNumber: 1, PageSize: 10);
            var result = validator.Validate(query);
            Assert.True(result.IsValid);
        }
        [Theory]
        [InlineData(0, 10)]
        [InlineData(10, 0)]
        [InlineData(-1, 10)]
        [InlineData(1, -10)]
        public void Validate_ShouldFail_ForInValidData(int pageNumber, int pageSize)
        {
            var query = new GetReservationsQuery(PageNumber: pageNumber, PageSize: pageSize);
            var result = validator.Validate(query);
            Assert.False(result.IsValid);
        }
    }
}
