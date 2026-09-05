
namespace Library.Domain.Copies
{
    public sealed class Copy:Audit
    {
        public Guid BookId { get; set; }
        public Book Book { get; set; } = default!;
        public bool Available { get; set; }
        public Copy() { }
        public Copy(Guid bookId,bool Available) {
        this.BookId = bookId;   
        this.Available=Available;
        }

    }
}
