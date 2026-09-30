
using Library.Application.Features.Reservations.Mappers;

namespace Library.Application.Features.Reservations.Queries.GetReservations;

public sealed class GetReservationHandler (
    IAppDbContext context,ILogger<GetReservationHandler>logger
    ): IRequestHandler<GetReservationQuery, Result<ReservationDto>>
{
    public async Task<Result<ReservationDto>> Handle(GetReservationQuery request, CancellationToken cancellationToken)
    {
        Reservation? reservation = await context.Reservations.AsNoTracking()
            .Include(r=>r.User)
            .Include(r=>r.Copy)
              .ThenInclude(c=>c.Book).FirstOrDefaultAsync(r => r.Id == request.Id);
        if(reservation is null)
        {
            logger.LogError("reservation with id = {Id} is not found ",request.Id);
            return ApplicationErrors.ReservationNotFound(request.Id);
        }
        return reservation.ToDto();
    }
}
