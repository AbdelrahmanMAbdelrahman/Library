

using Library.Domain.Common.Results;
using Library.Domain.Reservations;

namespace Library.Tests.Common.Reservations;

public class ReservationFactory
{
    public static Result<Reservation> Create(DateTime? reservationDate=null,
        Guid? copyId = null, string? userId=null)
    {
        return Reservation.Create(copyId??Guid.NewGuid(),userId??Guid.NewGuid().ToString(),
            reservationDate??DateTime.UtcNow);
    }
}
