using Library.Domain.Reservations;

namespace Library.Application.Features.Reservations.Mappers;

public static class ReservationMapper
{
    public static ReservationDto ToDto(this Reservation reservation)
    {
        return new ReservationDto(reservation.ReservationDate,
            reservation.User.ToDto(),
            reservation.Copy.ToDto());
    }
    public static IQueryable<ReservationDto> ToDto(this IQueryable<Reservation> reservations)
    {
        return reservations.Select(r => new ReservationDto(r.ReservationDate,
            r.User.ToDto(),
            r.Copy.ToDto()));
            
           
    }
}
