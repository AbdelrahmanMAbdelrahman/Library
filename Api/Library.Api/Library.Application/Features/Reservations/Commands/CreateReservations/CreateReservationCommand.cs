namespace Library.Application.Features.Reservations.Commands.CreateReservations;

public record CreateReservationCommand(Guid CopyId) :IRequest<Result<ReservationDto>>;
