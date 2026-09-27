namespace Library.Application.Features.Reservations.Queries.GetReservations;

public sealed record GetReservationsQuery(
    int PageNumber=1,int PageSize=10,string? UserName=null,
    DateTime? FromReservationDate=null,DateTime? ToReservationDate=null,string? Title=null,
    string? Genere=null,string? SortColumn="Title",string?SortDirection="desc")
    :IRequest<Result<PaginatedList< ReservationDto>>>;
