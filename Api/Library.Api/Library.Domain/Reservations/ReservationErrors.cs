namespace Library.Domain.Reservations;

internal static class ReservationErrors
{
    internal static Error InvalidReservationDate => 
        Error.Validation("ReservationErrors.InvalidReservationDate", "Provide a valid value for Reservation Date");
    internal static Error InvalidUserId => 
        Error.Validation("ReservationErrors.InvalidUserId", "Provide a valid value for User Id");
    internal static Error InvalidCopyId =>
        Error.Validation("ReservationErrors.InvalidCopyId", "Provide a valid value for Copy Id");
}
