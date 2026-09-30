namespace Library.Domain.Reservations;

public sealed class Reservation:Audit
{
    public Guid CopyId {  get;private set; }
    public string UserId { get; private set; } = default!;
    public DateTime ReservationDate { get; private set; }
    public Copy Copy { get; set; } 
    public AppUser User { get;  set; } = default!;
    public Reservation(){}
    public Reservation(Guid id,Guid copyId,string userId,DateTime reservationDate ){
    this.CopyId = copyId;
        this.UserId = userId;
        this.ReservationDate = reservationDate;
        this.Id = id;
    }

    public static Result<Reservation> Create(Guid copyId, string userId, DateTime reservationDate)
    {
        if (copyId == Guid.Empty) return ReservationErrors.InvalidCopyId;
        if (string.IsNullOrEmpty(userId)) return ReservationErrors.InvalidUserId;
        if (reservationDate < DateTime.UtcNow.Subtract(TimeSpan.FromDays(1))) return ReservationErrors.InvalidReservationDate;

        return new Reservation(Guid.NewGuid(),copyId,userId,reservationDate);
    }
}
