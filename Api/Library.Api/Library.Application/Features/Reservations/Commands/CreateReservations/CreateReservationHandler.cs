using Library.Application.Features.Reservations.Mappers;
using Library.Domain.Copies.Enum;
using Microsoft.Extensions.Caching.Hybrid;

namespace Library.Application.Features.Reservations.Commands.CreateReservations
{
    public sealed class CreateReservationHandler(
        ILogger<CreateReservationHandler>logger,
        IAppDbContext context,
        UserManager<AppUser> userManager,
        HybridCache hybridCache,
        IUser user) : IRequestHandler<CreateReservationCommand, Result<ReservationDto>>
    {
        public async Task<Result<ReservationDto>> Handle(
            CreateReservationCommand request, CancellationToken cancellationToken)
        {
            Copy? copy = await context.Copies
                .FirstOrDefaultAsync(c=>c.Id== request.CopyId,cancellationToken);
            if(copy is null)
            {
                logger.LogError("book with id = {CopyId} is not found",request.CopyId);
                return ApplicationErrors.CopyNotFound(request.CopyId);
            }
            if (copy.Status==CopyStatus.Available)
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
            Result<Updated> setReservedResult = copy.SetReserved();
            if (setReservedResult.IsError) {
                logger.LogError(string.Join(" - ", setReservedResult.Errors));
                return setReservedResult.Errors;
            }
            await context.Reservations.AddAsync(createReservationResult.Value,cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            await hybridCache.RemoveByTagAsync("Reservations", cancellationToken);
            createReservationResult.Value.Copy = copy;//await context.Copies!.AsNoTracking()
               // .Include(c=>c.Book)
              //  .Where(c => c.Id == request.CopyId)
                //.Select(c=>new Copy(c.Id,c.BookId,c.Available))
              //  .FirstOrDefaultAsync(cancellationToken)!;
            createReservationResult.Value.User =await userManager.FindByIdAsync(user.Id);
            return createReservationResult.Value.ToDto();
        }
    }
}
