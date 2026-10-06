using Library.Domain.Common.Results;
using Library.Domain.Reservations;
using Library.Tests.Common;
using Library.Tests.Common.Reservations;

namespace Library.Domain.Tests.Reservations;

public class ReservationTests
{
   [Fact]
   public void CreateReservation_ShouldPass_WithValidData()
    {
        FakeTimeProvider timeProvider = new FakeTimeProvider();
        var time = timeProvider.GetUtcNow().LocalDateTime;
        Guid copyId= Guid.NewGuid();
        string userId = Guid.NewGuid().ToString();

        Result< Reservation> reservationRes = ReservationFactory.Create(time,copyId,userId);
        Assert.True(reservationRes.IsSuccess);
        Assert.Equal(time,reservationRes.Value.ReservationDate);
        Assert.Equal(userId,reservationRes.Value.UserId);
        Assert.Equal(copyId,reservationRes.Value.CopyId);
    }
   [Fact]
   public void CreateReservation_ShouldFail_WithInvalidDate()
    {
        FakeTimeProvider timeProvider = new FakeTimeProvider();
        var time = timeProvider.GetUtcNow().LocalDateTime;
        Guid copyId= Guid.NewGuid();
        string userId = Guid.NewGuid().ToString();

        Result< Reservation> reservationRes = ReservationFactory.Create(time.AddDays(-1), copyId,userId);
        Assert.False(reservationRes.IsSuccess);
         
    }
   [Fact]
   public void CreateReservation_ShouldFail_WithInvalidUserId()
    {
        FakeTimeProvider timeProvider = new FakeTimeProvider();
        var time = timeProvider.GetUtcNow().LocalDateTime.AddDays(1);
        Guid copyId= Guid.NewGuid();
        string userId = "";

        Result< Reservation> reservationRes = ReservationFactory.Create(time,copyId,userId);
        Assert.False(reservationRes.IsSuccess);
         
    }
   [Fact]
   public void CreateReservation_ShouldFail_WithInvalidCopyId()
    {
        FakeTimeProvider timeProvider = new FakeTimeProvider();
        var time = timeProvider.GetUtcNow().LocalDateTime.AddDays(1);
        Guid copyId= Guid.Empty;
        string userId = Guid.NewGuid().ToString();

        Result< Reservation> reservationRes = ReservationFactory.Create(time,copyId,userId);
        Assert.False(reservationRes.IsSuccess);
         
    }
   
}
