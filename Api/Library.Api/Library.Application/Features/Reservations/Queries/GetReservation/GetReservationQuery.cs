namespace Library.Application.Features.Reservations.Queries.GetReservations;

public sealed record GetReservationQuery(Guid Id)
    :IRequest<Result< ReservationDto>>;
