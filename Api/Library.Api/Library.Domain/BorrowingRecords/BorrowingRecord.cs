namespace Library.Domain.BorrowingRecords;

public sealed class BorrowingRecord : Audit
{
    public Guid CopyId { get; private set; }
    public string AppUserId { get; private set; } = default!;
    public DateTime BorrowingDate { get; private set; }
    public DateTime DueDate { get; private set; }
    public DateTime? ActualReturnDate { get; private set; }
    public Copy Copy { get;  set; } = default!;
    public AppUser AppUser { get;  set; }=default!;
    public Fine Fine { get; set; }
    public BorrowingRecord(){}
    public BorrowingRecord(Guid id,Guid copyId,string appUserId,DateTime DueDate,
         DateTime borrowingDate,DateTime? ActualReturnDate){
        this.CopyId = copyId;
        this.AppUserId = appUserId;
        this.DueDate = DueDate;
        this.BorrowingDate = borrowingDate;
        this.ActualReturnDate = ActualReturnDate;
        this.Id = id;
    }

    public static Result<BorrowingRecord> Create(Guid copyId, string appUserId, DateTime DueDate,
         DateTime borrowingDate, DateTime? ActualReturnDate)
    {
        if (copyId == Guid.Empty) return borrowingRecordErrors.InvalidCopyId;
        if(string.IsNullOrEmpty(appUserId))return borrowingRecordErrors.InvalidUserId;
        if (DueDate <= DateTime.UtcNow) return borrowingRecordErrors.InvalidDueDate;
        if (borrowingDate < DateTime.UtcNow.Subtract(TimeSpan.FromHours(6))) return borrowingRecordErrors.InalidBorrowingDate;

        return new BorrowingRecord(Guid.NewGuid(),copyId,appUserId,DueDate,
            borrowingDate,ActualReturnDate);
    }

}
