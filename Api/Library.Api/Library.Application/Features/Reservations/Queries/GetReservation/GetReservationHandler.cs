namespace Library.Application.Features.Reservations.Queries.GetReservations;

public sealed class GetReservationHandler (
    IAppDbContext context,ILogger<GetReservationHandler>logger,
    IUser user,IIdentityService identity
    ): IRequestHandler<GetReservationQuery, Result<ReservationDto>>
{
    public async Task<Result<ReservationDto>> Handle(GetReservationQuery request,
        CancellationToken cancellationToken)
    {
        IQueryable<Reservation> query =  context.Reservations.AsNoTracking()
            .Include(r=>r.User)
            .Include(r=>r.Copy)
              .ThenInclude(c=>c.Book).Where(r => r.Id == request.Id);
        if (!await identity.IsInRoleAsync(user.Id??"", nameof(Role.Admin)))
            query = query.Where(r=>r.UserId==user.Id);
        Reservation?reservation= await query.FirstOrDefaultAsync(cancellationToken);

        if(reservation is null)
        {
            logger.LogError("reservation with id = {Id} is not found ",request.Id);
            return ApplicationErrors.ReservationNotFound(request.Id);
        }
        return reservation.ToDto();
    }
}
