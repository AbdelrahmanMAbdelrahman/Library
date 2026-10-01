using Library.Application.Common.Models;

namespace Library.Application.Features.Books.Queries.GetBooks;

public sealed record GetCopiesQuery(
    int PageNumber = 1, int PageSize = 10, bool Available = true,
    string SortColumn = "Title", string SortDirection = "Desc",
    string? Title = null, string? ISBN = null, string? Genere = null,
    DateTime? PublicationDateFrom = null, DateTime? PublicationDateTo = null
    ) : ICachedQuery<Result<PaginatedList<CopyDto>>>
{
    public string Key =>
        $"$p:{PageNumber} - ps:{PageSize} - av{Available}" +
        $"sc:{SortColumn} - sd:{SortDirection}" +
        $"ti:{Title ?? "-"}" +
        $"is:{ISBN ?? "-"}" +
        $"ge:{Genere ?? "-"}" +
        $"pdf:{PublicationDateFrom?.ToString("yyyyMMdd") ?? "-"}" +
        $"pdt:{PublicationDateTo?.ToString("yyyyMMdd") ?? "-"}";

    public string[] Tags => ["Copies"];

    public TimeSpan Expiration =>TimeSpan.FromMinutes(10) ;
}
