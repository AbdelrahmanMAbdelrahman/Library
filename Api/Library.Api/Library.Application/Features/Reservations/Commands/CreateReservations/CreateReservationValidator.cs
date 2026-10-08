namespace Library.Application.Features.Reservations.Commands.CreateReservations
{
    public class CreateReservationValidator:AbstractValidator<CreateReservationCommand>
    {
        public CreateReservationValidator()
        {
            RuleFor(r=>r.CopyId).Must(id=>id!=Guid.Empty).WithMessage("Must provide '{Propertyname}'");
        }
    }
}
