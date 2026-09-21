
namespace Library.Application.Features.Books.Queries.GetBooks;

public sealed class GetCopiesHandler(ILogger<GetCopiesHandler>logger,IAppDbContext context) :
    IRequestHandler<GetCopiesQuery, Result<PaginatedList<CopyDto>>>
{
    public async Task<Result<PaginatedList<CopyDto>>> Handle(GetCopiesQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Copy> CopiesQuery = context.Books.Where(
            b=>b.Copies.Any(c=>c.Available))
            .Select(b=>b.Copies.First());
            
        CopiesQuery = ApplyFilter(
            CopiesQuery,request.Title,request.ISBN,request.PublicationDateFrom,request.PublicationDateTo,request.Genere);
        CopiesQuery = ApplySort(CopiesQuery,request.SortColumn,request.SortDirection);
        //CopiesQuery=CopiesQuery.GroupBy(c => c.BookId)
        //    .Select(c => c.First());
        Result<PaginatedList<CopyDto>> Copies =await PaginatedList<CopyDto>
            .Create(CopiesQuery.ToDto(),request.PageNumber,request.PageSize);
        if (Copies.IsError)
        {
            logger.LogError(string.Join(" - ",Copies.Errors));
            return Copies.Errors;
        }
        return Copies.Value;
    }

    private IQueryable<Copy> ApplySort(IQueryable<Copy> CopiesQuery, string sortColumn, string sortDirection)
    {
        bool desc = sortDirection.Equals("Desc",StringComparison.CurrentCultureIgnoreCase);
        return sortColumn switch
        {
            "Title" => desc ? CopiesQuery.OrderByDescending(b => b.Book.Title) : CopiesQuery.OrderBy(b => b.Book.Title),
            "ISBN" => desc ? CopiesQuery.OrderByDescending(b => b.Book.ISBN) : CopiesQuery.OrderBy(b => b.Book.ISBN),
            "Genere" => desc ? CopiesQuery.OrderByDescending(b => b.Book.Genere) : CopiesQuery.OrderBy(b => b.Book.Genere),
            "PublicationDate" => desc ? CopiesQuery.OrderByDescending(b => b.Book.PublicationDate) : CopiesQuery.OrderBy(b => b.Book.PublicationDate),
            _ => desc ? CopiesQuery.OrderByDescending(b => b.Book.Title) : CopiesQuery.OrderBy(b => b.Book.Title)
        };
    }

    private IQueryable<Copy> ApplyFilter(IQueryable<Copy> CopiesQuery, string? title, string? iSBN,
        DateTime? publicationDateFrom, DateTime? publicationDateTo, string? genere)
    {
        
        if (!string.IsNullOrEmpty(title))
        {
            CopiesQuery = CopiesQuery.Where(b => b.Book.Title.StartsWith( title));
        }
        if (!string.IsNullOrEmpty(iSBN))
        {
            CopiesQuery = CopiesQuery.Where(b => b.Book. ISBN.StartsWith( iSBN));
        }
        if (publicationDateFrom.HasValue)
        {
            CopiesQuery = CopiesQuery.Where(b => b.Book.PublicationDate >= publicationDateFrom);
        }
        if (publicationDateTo.HasValue)
        {
            CopiesQuery = CopiesQuery.Where(b => b  .Book.PublicationDate <= publicationDateTo);
        }
        if (!string.IsNullOrEmpty(genere))
        {
            CopiesQuery = CopiesQuery.Where(b => b.Book.Genere.StartsWith( genere));
        }
        return CopiesQuery;
    }
}
