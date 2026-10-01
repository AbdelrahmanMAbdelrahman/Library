namespace Library.Application.Features.Reservations.Queries.GetReservations;

public sealed record GetReservationQuery(Guid Id)
    : ICachedQuery<Result<ReservationDto>>
{
    public string Key => $"Reservation-{Id}";

    public string[] Tags => ["Reservation"];

    public TimeSpan Expiration => TimeSpan.FromMinutes(10);
}
