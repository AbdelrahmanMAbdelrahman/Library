using Microsoft.AspNetCore.SignalR;

namespace Library.Infrastructure.RealTime;

public sealed class CopyReturnHub:Hub
{
    public static string URL = "/Hubs/CopyNotifier";
}
