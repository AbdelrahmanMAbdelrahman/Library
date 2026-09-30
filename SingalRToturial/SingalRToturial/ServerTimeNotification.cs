
using Microsoft.AspNetCore.SignalR;
using System.Runtime.CompilerServices;

namespace SingalRToturial
{
    public class ServerTimeNotification(ILogger<ServerTimeNotification>logger,
        IHubContext<NotificationHub,IHubContext>context) : BackgroundService// cs notificationHub cs0246
    {
        private readonly TimeSpan timeSpan = TimeSpan.FromMinutes(10);
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            PeriodicTimer timer = new PeriodicTimer(timeSpan);
            while(stoppingToken.IsCancellationRequested&&await timer.WaitForNextTickAsync())
            {
                DateTime dateTime = DateTime.UtcNow;
                logger.LogInformation("working on {serverTimeNotification} , time ={dateTime}",
                    nameof(ServerTimeNotification),dateTime);
                await context.Clients.All.RecieveNotification(dateTime);
            }
        }
    }
}
