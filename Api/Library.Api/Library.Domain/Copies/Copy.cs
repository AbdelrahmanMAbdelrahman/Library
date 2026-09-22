
using Library.Domain.BorrowingRecords;

namespace Library.Domain.Copies
{
    public sealed class Copy:Audit
    {
        public Guid BookId { get;private set; }
        public Book Book { get;private set; } = default!;
        public bool Available { get;private set; }
        public BorrowingRecord BorrowingRecord { get;private set; }=default!;
        public Reservation Reservation { get; set; }
        public Copy() { }
        public Copy(Guid id,Guid bookId,bool Available) {
        this.BookId = bookId;
            this.Id = id;
        this.Available=Available;
        }

        public static Result<Copy> Create(Guid id, Guid bookId, bool Available)
        {
            if (id == Guid.Empty) return CopyErrors.InvalidCopyId;
            if (bookId == Guid.Empty) return CopyErrors.InvalidBookId;
            return new Copy(id, bookId,Available);
        }

        public Result<Updated> SetUnAvailable()
        {
            Available= false;
            return Result.Updated;
        }

        public Result<Updated> SetAvailable()
        {
            Available = true;
            return Result.Updated;
        }
    }
}
