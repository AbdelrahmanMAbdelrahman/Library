namespace Library.Application.Features.Reservations.Queries.GetReservations;

public sealed record GetReservationQuery(Guid ReservationId)
    :IRequest<Result<PaginatedList< ReservationDto>>>;
