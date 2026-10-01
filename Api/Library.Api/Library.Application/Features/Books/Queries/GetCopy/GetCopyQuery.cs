namespace Library.Application.Features.Books.Queries.GetCopy;

public sealed record GetCopyQuery(Guid Id) : ICachedQuery<Result<CopyDto>>
{
    public string Key =>$"Copy-{Id}";

    public string[] Tags => ["Copy"];

    public TimeSpan Expiration =>TimeSpan.FromMinutes(10);

   
}
