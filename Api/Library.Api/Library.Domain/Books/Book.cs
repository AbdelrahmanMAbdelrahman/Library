namespace Library.Domain.Books;
public sealed class Book:Audit
{
    public  string Title { get;private set; }
    public  string ISBN { get; private set; }
    public DateTimeOffset PublicationDate { get; private set; }
    public  string Genere { get; private set; }
    public string? AdditionalNotes { get; private set; }
    private List<Copy> _Copies = [];
    public IEnumerable<Copy> Copies => _Copies;
    protected Book(){}
    protected Book(
        Guid Id,string title,string isbn,DateTimeOffset publicationDate,string genere,
        string additionalNotes,List<Copy>? copies){
        this.Title = title;
        this.ISBN = isbn;
        this.PublicationDate = publicationDate;
        this.Genere = genere;
        this._Copies = copies;
    }
    public static Result<Book> Create(Guid Id, string title, string isbn, DateTimeOffset publicationDate, string genere,
        string additionalNotes, List<Copy>? copies)
    {
        if (Id == Guid.Empty) return BookErrors.NotValidId;
        if (string.IsNullOrWhiteSpace(title)) return BookErrors.EmptyTitle;
        if (string.IsNullOrWhiteSpace(isbn)) return BookErrors.EmptyISBN;
        if (publicationDate > DateTime.UtcNow) return BookErrors.InvalidPublicationDate;
        if(string.IsNullOrWhiteSpace(genere)) return BookErrors.EmptyGenere;
        return new Book(Id, title, isbn, publicationDate, genere, additionalNotes, copies ?? []);
    }
    public  Result<Updated> Update( string title, string isbn, DateTimeOffset publicationDate, string genere,
        string additionalNotes, List<Copy>? copies)
    {
        if (string.IsNullOrWhiteSpace(title)) return BookErrors.EmptyTitle;
        if (string.IsNullOrWhiteSpace(isbn)) return BookErrors.EmptyISBN;
        if (publicationDate > DateTime.UtcNow) return BookErrors.InvalidPublicationDate;
        if(string.IsNullOrWhiteSpace(genere)) return BookErrors.EmptyGenere;
        this.Title = title;
        this.ISBN = isbn;
        this.AdditionalNotes = additionalNotes;
        this.Genere = genere;
        this._Copies = copies ?? [];
        return Result.Updated;
    }

}
