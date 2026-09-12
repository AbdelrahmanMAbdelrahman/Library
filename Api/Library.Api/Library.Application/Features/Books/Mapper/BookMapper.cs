namespace Library.Application.Features.Books.Mapper;

public static class BookMapper
{
    public static BookDto ToDto(this Book book)
    {
        return new BookDto(book.Id,book.Title,book.ISBN,book.Genere,
            book.AdditionalNotes,book.PublicationDate,book.UploadedFileId);
    }
    public static IQueryable<BookDto> ToDto(this IQueryable<Book> books)
    {
        return books.Select(b=>b.ToDto());
    }
}
