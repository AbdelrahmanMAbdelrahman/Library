using Library.Application.Features.Reservations.Queries.GetReservations;
namespace Library.Application.Features.Reservations.Queries.GetReservation;

public class GetReservationValidator:AbstractValidator<GetReservationQuery>
{
    public GetReservationValidator()
    {
        RuleFor(r => r.Id).Must(id => id != Guid.Empty).WithMessage("Must provide a valid '{PropertyName}'") ;
    }
}
