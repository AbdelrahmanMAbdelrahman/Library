namespace Library.Domain.Fines;

public sealed class Fine:Audit
{
    public string AppUserId { get;private set; }
    public Guid BorrowingRecordId { get;private set; }
    public double NumberOfLateDays { get;private set; }
    public double FineAmount { get;private set; }
    public PaymentStatus PaymentStatus { get;private set; }
    
    public BorrowingRecord BorrowingRecord { get; set; }
    public AppUser AppUser { get; set; }
    public Fine(){}
    public Fine(Guid id,Guid borrowingRecordId,string userId,
        double numberOfLateDays,double fineAmount,PaymentStatus paymentStatus){
        this.Id = id;
        this.AppUserId = userId;
        this.NumberOfLateDays = numberOfLateDays;
        this.BorrowingRecordId= borrowingRecordId;
        this.FineAmount= fineAmount;
        this.PaymentStatus = paymentStatus;
    }

    public static Result<Fine> Create(Guid borrowingRecordId, string userId,
        double numberOfLateDays, double fineAmount, PaymentStatus paymentStatus)
    {
        if (borrowingRecordId == Guid.Empty) return FineErrors.InvalidBorrowingRecordId;
        if (string.IsNullOrEmpty(userId)) return FineErrors.InvalidUserId;
        if (numberOfLateDays < 1 || numberOfLateDays > 100) return FineErrors.InvalidNumberOfLateDays;
        if (fineAmount < 1) return FineErrors.InvalidFineAmount;
        if (!Enum.IsDefined(paymentStatus)) return FineErrors.InvalidPaymentStatus;

        return new Fine(Guid.NewGuid(),borrowingRecordId,userId,numberOfLateDays,fineAmount,paymentStatus);
    }
    public Result<Updated>UpdateFine(double numberOfLateDays, double fineAmount, PaymentStatus paymentStatus)
    {
        if (numberOfLateDays < 1 || numberOfLateDays > 100) return FineErrors.InvalidNumberOfLateDays;
        if (fineAmount < 1) return FineErrors.InvalidFineAmount;
        if (!Enum.IsDefined(paymentStatus)) return FineErrors.InvalidPaymentStatus;
        this.NumberOfLateDays= numberOfLateDays;
        this.FineAmount= fineAmount;
        this.PaymentStatus= paymentStatus;
        return Result.Updated;
    }

    

}
