using Library.Domain.UplodedFiles;

namespace Library.Domain.Books;
public sealed class Book:Audit
{
    public  string Title { get;private set; }
    public  string ISBN { get; private set; }
    public DateTime PublicationDate { get; private set; }
    public  string Genere { get; private set; }
    public string? AdditionalNotes { get; private set; }
    public Guid? UploadedFileId { get; private set; }
    public UploadedFile uploadedFile { get; private set; }
    private List<Copy> _Copies = [];
    public IEnumerable<Copy> Copies => _Copies;
    protected Book(){}
    protected Book(
        Guid Id,string title,string isbn,DateTime publicationDate,string genere,
        string additionalNotes,Guid fileId,List<Copy>? copies){
        this.Title = title;
        this.ISBN = isbn;
        this.PublicationDate = publicationDate;
        this.Genere = genere;
        this.AdditionalNotes = additionalNotes;
        this.UploadedFileId= fileId;
        this._Copies = copies;
    }
    public static Result<Book> Create(string title, string isbn, DateTime publicationDate, string genere,
        string additionalNotes, Guid fileId, int NumberOfCopies)
    {
        Guid BookId = Guid.NewGuid();

        if (fileId == Guid.Empty) return BookErrors.NotValidFileId;
        if (string.IsNullOrWhiteSpace(title)) return BookErrors.EmptyTitle;
        if (string.IsNullOrWhiteSpace(isbn)) return BookErrors.EmptyISBN;
        if (publicationDate > DateTime.UtcNow) return BookErrors.InvalidPublicationDate;
        if(string.IsNullOrWhiteSpace(genere)) return BookErrors.EmptyGenere;
        List<Copy> copies = new List<Copy>();
        for(int i = 0; i < NumberOfCopies; i++)
        {
            copies.Add(new Copy(Guid.NewGuid(),BookId,true));
        }
        return new Book(BookId,title, isbn, publicationDate, genere, additionalNotes,fileId, copies);
    }
    public  Result<Updated> Update( string title, string isbn, DateTime publicationDate, string genere,
        string additionalNotes)
    {
        if (string.IsNullOrWhiteSpace(title)) return BookErrors.EmptyTitle;
        if (string.IsNullOrWhiteSpace(isbn)) return BookErrors.EmptyISBN;
        if (publicationDate > DateTime.UtcNow) return BookErrors.InvalidPublicationDate;
        if(string.IsNullOrWhiteSpace(genere)) return BookErrors.EmptyGenere;
        this.Title = title;
        this.ISBN = isbn;
        this.AdditionalNotes = additionalNotes;
        this.Genere = genere;
        this.PublicationDate=publicationDate;
        return Result.Updated;
    }

}
