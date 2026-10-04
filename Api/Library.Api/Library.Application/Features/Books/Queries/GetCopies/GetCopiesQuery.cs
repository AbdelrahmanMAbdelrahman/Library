using Library.Application.Common.Models;
using Library.Domain.Copies.Enum;

namespace Library.Application.Features.Books.Queries.GetBooks;

public sealed record GetCopiesQuery(
    int PageNumber = 1, int PageSize = 10, CopyStatus Status = CopyStatus.Available,
    string SortColumn = "Title", string SortDirection = "Desc",
    string? Title = null, string? ISBN = null, string? Genere = null,
    DateTime? PublicationDateFrom = null, DateTime? PublicationDateTo = null
    ) : ICachedQuery<Result<PaginatedList<CopyDto>>>
{
    public string Key =>
        $"$p:{PageNumber} - ps:{PageSize} - st{Status}" +
        $"sc:{SortColumn} - sd:{SortDirection}" +
        $"ti:{Title ?? "-"}" +
        $"is:{ISBN ?? "-"}" +
        $"ge:{Genere ?? "-"}" +
        $"pdf:{PublicationDateFrom?.ToString("yyyyMMdd") ?? "-"}" +
        $"pdt:{PublicationDateTo?.ToString("yyyyMMdd") ?? "-"}";

    public string[] Tags => ["Copies"];

    public TimeSpan Expiration =>TimeSpan.FromMinutes(10) ;
}
