namespace Library.Application.Common.Interfaces;

public interface ICopyNotifier
{
    Task NotifyCopyReturned(string UserId,Guid CopyId,CancellationToken ct);
}
