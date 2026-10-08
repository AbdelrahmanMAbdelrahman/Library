namespace Library.Application.Features.Reservations.Queries.GetReservations;

public sealed class GetReservationsValidator:AbstractValidator<GetReservationsQuery>
{
    public GetReservationsValidator()
    {
        RuleFor(c => c.PageNumber).Must(p => p > 0 && p < 1000)// small library
 .WithMessage("'{PropertyName}' must be in range '{MinValue}' : '{MaxValue}'");
        RuleFor(c => c.PageSize).Must(p => p > 0 && p < 20)
            .WithMessage("'{PropertyName}' must be in range '{MinValue}' : '{MaxValue}'");
    }
}
