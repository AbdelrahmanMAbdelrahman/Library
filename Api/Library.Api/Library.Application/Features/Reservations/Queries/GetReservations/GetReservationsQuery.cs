namespace Library.Application.Features.Reservations.Queries.GetReservations;

public sealed record GetReservationsQuery(
    int PageNumber = 1, int PageSize = 10, string? UserName = null,
    DateTime? FromReservationDate = null, DateTime? ToReservationDate = null, string? Title = null,
    string? Genere = null, string? SortColumn = "Title", string? SortDirection = "desc")
    : ICachedQuery<Result<PaginatedList<ReservationDto>>>
{
    public string Key => 
        $"p:{PageNumber}-" +
        $"ps:{PageSize}-" +
        $"u:{UserName??""}-" +
        $"sc:{SortColumn??""}-" +
        $"sd:{SortDirection??""}-" +
        $"frd:{FromReservationDate?.ToString("yyyyMMdd")??""}-" +
        $"trd:{ToReservationDate?.ToString("yyyyMMdd") ?? ""}-" +
        $"t:{Title??""}-" +
        $"g:{Genere??""}-";

    public string[] Tags => ["Reservation"];

    public TimeSpan Expiration => TimeSpan.FromMinutes(10);
}
