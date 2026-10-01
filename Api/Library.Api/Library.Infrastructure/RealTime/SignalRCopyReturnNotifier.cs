using Microsoft.AspNetCore.SignalR;

namespace Library.Infrastructure.RealTime;

public sealed class SignalRCopyReturnNotifier(
    IHubContext<CopyReturnHub> hubContext,
    ILogger<SignalRCopyReturnNotifier> logger)
    : ICopyNotifier
{
  

    public async Task NotifyCopyReturned(
    string userId,
    Guid copyId,
    CancellationToken ct)
    {
        logger.LogInformation(
            "Sending CopyReturned to ALL clients.  CopyId={CopyId}",
            
            copyId);

        await hubContext.Clients.All.SendAsync(
            "CopyReturned",
            new
            {
                CopyId = copyId
            },
            ct);

        logger.LogInformation(
            "CopyReturned sent to ALL clients");
    }
}