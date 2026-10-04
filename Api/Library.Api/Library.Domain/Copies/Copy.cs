
using Library.Domain.BorrowingRecords;
using Library.Domain.Copies.Enum;

namespace Library.Domain.Copies
{
    public sealed class Copy:Audit
    {
        public Guid BookId { get;private set; }
        public Book Book { get;private set; } = default!;
        public CopyStatus Status { get;private set; }
        public byte[] RowVersion { get;private set; } = [];
        public ICollection< BorrowingRecord> BorrowingRecords { get;private set; }=default!;
        public ICollection< Reservation> Reservation { get; set; }
        public Copy() { }
        public Copy(Guid id,Guid bookId, CopyStatus status) {
        this.BookId = bookId;
            this.Id = id;
        this.Status=status;
        }

        public static Result<Copy> Create(Guid id, Guid bookId, CopyStatus status)
        {
            if (id == Guid.Empty) return CopyErrors.InvalidCopyId;
            if (bookId == Guid.Empty) return CopyErrors.InvalidBookId;
            
            return new Copy(id, bookId,status);
        }

        public Result<Updated> SetBorrowed()
        {
            Status= CopyStatus.Borrowed;
            return Result.Updated;
        }

        public Result<Updated> SetAvailable()
        {
            Status = CopyStatus.Available;
            return Result.Updated;
        }
        public Result<Updated> SetReserved()
        {
            Status = CopyStatus.Reserved;
            return Result.Updated;
        }
    }
}
