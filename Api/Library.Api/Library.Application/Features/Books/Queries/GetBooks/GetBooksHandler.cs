
namespace Library.Application.Features.Books.Queries.GetBooks;

public sealed class GetBooksHandler(ILogger<GetBooksHandler>logger,IAppDbContext context) : IRequestHandler<GetBooksQuery, Result<PaginatedList<BookDto>>>
{
    public async Task<Result<PaginatedList<BookDto>>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Book> BooksQuery = context.Books;
        BooksQuery = ApplyFilter(
            BooksQuery,request.Title,request.ISBN,request.PublicationDateFrom,request.PublicationDateTo,request.Genere);
        BooksQuery = ApplySort(BooksQuery,request.SortColumn,request.SortDirection);
        Result<PaginatedList<BookDto>> Books =await PaginatedList<BookDto>
            .Create(BooksQuery.ToDto(),request.PageNumber,request.PageSize);
        if (Books.IsError)
        {
            logger.LogError(string.Join(" - ",Books.Errors));
            return Books.Errors;
        }
        return Books.Value;
    }

    private IQueryable<Book> ApplySort(IQueryable<Book> booksQuery, string sortColumn, string sortDirection)
    {
        bool desc = sortDirection.Equals("Desc",StringComparison.CurrentCultureIgnoreCase);
        return sortDirection switch
        {
            "Title" => desc ? booksQuery.OrderByDescending(b => b.Title) : booksQuery.OrderBy(b => b.Title),
            "ISBN" => desc ? booksQuery.OrderByDescending(b => b.ISBN) : booksQuery.OrderBy(b => b.ISBN),
            "Genere" => desc ? booksQuery.OrderByDescending(b => b.Genere) : booksQuery.OrderBy(b => b.Genere),
            "PublicationDate" => desc ? booksQuery.OrderByDescending(b => b.PublicationDate) : booksQuery.OrderBy(b => b.PublicationDate),
            _ => desc ? booksQuery.OrderByDescending(b => b.Title) : booksQuery.OrderBy(b => b.Title)
        };
    }

    private IQueryable<Book> ApplyFilter(IQueryable<Book> booksQuery, string? title, string? iSBN,
        DateTime? publicationDateFrom, DateTime? publicationDateTo, string? genere)
    {
        if (!string.IsNullOrEmpty(title))
        {
            booksQuery = booksQuery.Where(b => b.Title == title);
        }
        if (!string.IsNullOrEmpty(iSBN))
        {
            booksQuery = booksQuery.Where(b => b.ISBN == iSBN);
        }
        if (publicationDateFrom.HasValue)
        {
            booksQuery = booksQuery.Where(b => b.PublicationDate > publicationDateFrom);
        }
        if (publicationDateTo.HasValue)
        {
            booksQuery = booksQuery.Where(b => b.PublicationDate > publicationDateTo);
        }
        if (!string.IsNullOrEmpty(genere))
        {
            booksQuery = booksQuery.Where(b => b.Genere == genere);
        }
        return booksQuery;
    }
}
