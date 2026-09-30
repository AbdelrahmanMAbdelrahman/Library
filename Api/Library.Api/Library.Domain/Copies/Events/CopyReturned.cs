namespace Library.Domain.Copies.Events;

public sealed record CopyReturned(Guid CopyId):DomainEvent;
