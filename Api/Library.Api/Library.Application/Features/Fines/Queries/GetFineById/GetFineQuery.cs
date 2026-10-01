namespace Library.Application.Features.Fines.Queries.GetFineById;

public sealed record GetFineQuery(Guid Id) : ICachedQuery<Result<FineDto>>
{
    public string Key => $"Fine-{Id}";

    public string[] Tags => ["Fine"];

    public TimeSpan Expiration => TimeSpan.FromMinutes(10);
}
