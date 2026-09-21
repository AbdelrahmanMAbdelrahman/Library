using Library.Application.Common.Models;

namespace Library.Application.Features.Books.Queries.GetBooks;

public sealed record GetCopiesQuery(
    int PageNumber=1,int PageSize=10,
    string SortColumn="Title",string SortDirection="Desc",
    string?Title=null,string? ISBN=null,string?Genere=null,
    DateTime?PublicationDateFrom=null, DateTime? PublicationDateTo = null
    ):IRequest<Result<PaginatedList<CopyDto>>>;
