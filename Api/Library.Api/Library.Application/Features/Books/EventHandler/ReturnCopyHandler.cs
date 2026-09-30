
using Library.Domain.Copies.Events;

namespace Library.Application.Features.Books.EventHandler;

public sealed class ReturnCopyHandler(
    ICopyNotifier notifier,
    IAppDbContext context,
    ILogger<ReturnCopyHandler> logger)
    : INotificationHandler<CopyReturned>
{
    public async Task Handle(
        CopyReturned notification,
        CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "========== CopyReturned Handler START ==========");

        logger.LogInformation(
            "CopyId = {CopyId}",
            notification.CopyId);

        var reservation = await context.Reservations
            .FirstOrDefaultAsync(
                x => x.CopyId == notification.CopyId,
                cancellationToken);

        if (reservation is null)
        {
            logger.LogInformation(
                "No reservation found for CopyId = {CopyId}",
                notification.CopyId);

            return;
        }

        logger.LogInformation(
            "Reservation found. ReservationId = {ReservationId}, UserId = {UserId}, CopyId = {CopyId}",
            reservation.Id,
            reservation.UserId,
            reservation.CopyId);

        await notifier.NotifyCopyReturned(
            reservation.UserId,
            reservation.CopyId,
            cancellationToken);

        logger.LogInformation(
            "========== CopyReturned Handler END ==========");
    }
}