
namespace Library.Application.Features.Reservations.Queries.GetReservations;

public sealed class GetReservationHandler : IRequestHandler<GetReservationQuery, Result<ReservationDto>>
{
    public Task<Result<ReservationDto>> Handle(GetReservationQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
