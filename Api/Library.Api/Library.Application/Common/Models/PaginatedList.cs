using System.Threading.Tasks;

namespace Library.Application.Common.Models;

public sealed class PaginatedList<T>
{
    public List<T> Items { get;private set; }
    public bool HasNextPage { get; private set; }
    public bool HasPreviousPage { get; private set; }
    public int PageNumber { get; private set; }
    public int TotalPages { get; private set; }

    public PaginatedList(List<T> items,int pageNumber,int pageSize,int totalCount)
    {
        Items = items;
        PageNumber = pageNumber;
        TotalPages =(int) Math.Ceiling((double) totalCount / pageSize);
        HasNextPage = PageNumber < TotalPages;
        HasPreviousPage = pageNumber > 1;
    }

    public static async Task<Result<PaginatedList<T>>>Create(IQueryable<T> items,int PageNumber=1,int PageSize = 10)
    {
        List<T> ItemsList =await items.Skip((PageNumber-1) * PageSize)
            .Take(PageSize).ToListAsync();

        return new PaginatedList<T>(ItemsList,PageNumber,PageSize,items.Count());
    }
}
