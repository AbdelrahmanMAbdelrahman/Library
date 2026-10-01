using Microsoft.AspNetCore.SignalR;

namespace Library.Infrastructure.RealTime;

public sealed class CopyReturnHub:Hub
{
    public static string URL = "/Hubs/CopyNotifier";
    public override async Task OnConnectedAsync()
    {
        Console.WriteLine($"ConnectionId: {Context.ConnectionId}");
        Console.WriteLine($"UserIdentifier: {Context.UserIdentifier}");
        Console.WriteLine($"Authenticated: {Context.User?.Identity?.IsAuthenticated}");

        await base.OnConnectedAsync();
    }
}
