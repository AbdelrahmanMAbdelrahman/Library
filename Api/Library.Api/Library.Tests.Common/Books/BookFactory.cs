using Library.Domain.Books;
using Library.Domain.Common.Results;

namespace Library.Tests.Common.Books;

public static class BookFactory
{
    public static Result<Book> CreateBook(
        string? title=null, DateTime? publicationDate=null, string? genere= null,
        string? isbn = null, Guid? fileId = null, string? additionalNotes = null,
        int? numberOfCopies = null)
    {
        FakeTimeProvider timeProvider = new FakeTimeProvider();
        return Book.Create(title??"C",isbn??"123",
            publicationDate??timeProvider.GetUtcNow().LocalDateTime,genere??"porg",
            additionalNotes??string.Empty,fileId??Guid.Empty,
            numberOfCopies??1);
    }
}
