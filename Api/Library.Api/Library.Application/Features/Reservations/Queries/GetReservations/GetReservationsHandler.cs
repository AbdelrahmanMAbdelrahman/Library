

using Library.Application.Features.Reservations.Mappers;

namespace Library.Application.Features.Reservations.Queries.GetReservations;

public sealed class GetReservationsHandler(
    IAppDbContext context,ILogger<GetReservationsHandler>logger
    )
    : IRequestHandler<GetReservationsQuery, Result<PaginatedList<ReservationDto>>>//cs0535
{
    public async Task<Result<PaginatedList<ReservationDto>>> Handle(GetReservationsQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Reservation> reservations = context.Reservations.AsNoTracking()
            .Include(r=>r.Copy)
              .ThenInclude(r=>r.Book)
            .Include(r=>r.User);
        reservations = ApplySort(reservations, request.SortColumn, request.SortDirection);
        reservations = ApplyFilter(reservations,request.Genere,request.Title,request.UserName,
            request.FromReservationDate,request.ToReservationDate);
        Result<PaginatedList<ReservationDto>> paginatedReservations =await PaginatedList<ReservationDto>.
            Create(reservations.ToDto(),request.PageNumber,request.PageSize);
        if (paginatedReservations.IsSuccess)
        {
            logger.LogError(string.Join(" - ", paginatedReservations.Value));
        }
        return paginatedReservations.Value;
    }

  
    
    private IQueryable<Reservation> ApplyFilter(
        IQueryable<Reservation> reservations, string? genere, 
        string? title, string? userName, DateTime? fromReservationDate,DateTime? toReservationDate)
    {
        if(!string.IsNullOrEmpty(genere)) reservations =reservations.Where(r=>r.Copy.Book.Genere == genere);
        if(!string.IsNullOrEmpty(title)) reservations =reservations.Where(r=>r.Copy.Book.Title == title);
        if(!string.IsNullOrEmpty(userName)) reservations =reservations.Where(r=>r.User.Name == userName);
        if(fromReservationDate.HasValue) reservations =reservations.Where(r=>r.ReservationDate >= fromReservationDate);
        if(toReservationDate.HasValue) reservations =reservations.Where(r=>r.ReservationDate <= toReservationDate);
        return reservations;
    }

    private IQueryable<Reservation> ApplySort(IQueryable<Reservation> reservations,
        string? sortColumn, string? sortDirection)
    {
        bool desc = sortDirection.Equals("Desc", StringComparison.CurrentCultureIgnoreCase);
        return sortColumn switch {
            "Title" =>desc?reservations.OrderByDescending(r=>r.Copy.Book.Title):
            reservations.OrderBy(r=>r.Copy.Book.Title),
            "Genere" =>desc?reservations.OrderByDescending(r=>r.Copy.Book.Genere):
            reservations.OrderBy(r=>r.Copy.Book.Genere),
            "ReservationDate" =>desc?reservations.OrderByDescending(r=>r.ReservationDate):
            reservations.OrderBy(r=>r.ReservationDate),
            "UserName" =>desc?reservations.OrderByDescending(r=>r.User.Name):
            reservations.OrderBy(r=>r.User.Name),
            };
            
    }
}
