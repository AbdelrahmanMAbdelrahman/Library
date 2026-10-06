

using Library.Domain.Common.Results;
using Library.Domain.Reservations;

namespace Library.Tests.Common.Reservations;

public class ReservationFactory
{
    public static Result<Reservation> Create(DateTime? reservationDate, Guid? copyId, string? userId)
    {
        return Reservation.Create(copyId??Guid.NewGuid(),userId??Guid.NewGuid().ToString(),
            reservationDate??DateTime.UtcNow);
    }
}
