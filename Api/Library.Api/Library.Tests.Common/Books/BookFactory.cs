using Library.Domain.Books;
using Library.Domain.Common.Results;

namespace Library.Tests.Common.Books;

public static class BookFactory
{
    public static Result<Book> CreateBook(
        string? title, DateTime? publicationDate, string? genere,
        string? isbn, Guid? fileId, string? additionalNotes,
        int? numberOfCopies)
    {
        FakeTimeProvider timeProvider = new FakeTimeProvider();
        return Book.Create(title??string.Empty,isbn??string.Empty,
            publicationDate??timeProvider.GetUtcNow().LocalDateTime,genere??string.Empty,
            additionalNotes??string.Empty,fileId??Guid.Empty,
            numberOfCopies??1);
    }
}
