
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
        await notifier.NotifyCopyReturned(
            reservation.UserId,
            reservation.CopyId,
            cancellationToken);

    }
}