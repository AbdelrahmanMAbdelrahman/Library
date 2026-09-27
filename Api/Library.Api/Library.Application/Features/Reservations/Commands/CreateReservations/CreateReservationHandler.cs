using Library.Application.Features.Reservations.Mappers;

namespace Library.Application.Features.Reservations.Commands.CreateReservations
{
    public sealed class CreateReservationHandler(
        ILogger<CreateReservationHandler>logger,
        IAppDbContext context,
        IUser user) : IRequestHandler<CreateReservationCommand, Result<ReservationDto>>
    {
        public async Task<Result<ReservationDto>> Handle(
            CreateReservationCommand request, CancellationToken cancellationToken)
        {
            Copy? copy = await context.Copies.AsNoTracking()
                .FirstOrDefaultAsync(c=>c.Id== request.CopyId,cancellationToken);
            if(copy is null)
            {
                logger.LogError("book with id = {CopyId} is not found",request.CopyId);
                return ApplicationErrors.CopyNotFound(request.CopyId);
            }
            if (copy.Available)
            {
                logger.LogError("book with id = {CopyId} is Available now ", request.CopyId);
                return ApplicationErrors.CopyAlreadyAvailable(request.CopyId);
            }
             if(user is null)
            {
                logger.LogError("user is not found ");
                return ApplicationErrors.UserNotFound("empty");
            }
            Reservation? reservation = await context.Reservations.AsNoTracking()
                .FirstOrDefaultAsync(r=>r.CopyId==request.CopyId&&r.UserId==user.Id,cancellationToken);
            if(reservation is not null)
            {
                logger.LogError("reservation with id = {Id} is Already exist", reservation.Id);
                return ApplicationErrors.ReservationAlreadyExist(reservation.Id);
            }
            Result<Reservation> createReservationResult = Reservation.Create(request.CopyId,user.Id,DateTime.UtcNow);
            if (createReservationResult.IsError)
            {
                logger.LogError(string.Join(" - ",createReservationResult.Errors));
                return createReservationResult.Errors;
            }
            await context.SaveChangesAsync(cancellationToken);
            return createReservationResult.Value.ToDto();
        }
    }
}
