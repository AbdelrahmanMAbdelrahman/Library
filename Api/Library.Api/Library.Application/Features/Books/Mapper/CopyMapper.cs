

namespace Library.Application.Features.Books.Mapper;

public static class CopyMapper
{
    public static CopyDto ToDto(this Copy copy)
    {
        return new CopyDto(copy.Id,
            new BookDto( copy.BookId,copy.Book.Title,copy.Book.ISBN,copy.Book.Genere
            ,copy.Book.AdditionalNotes??"",copy.Book.PublicationDate,copy.Book.UploadedFileId),
            copy.Available);
    }
    public static IQueryable<CopyDto> ToDto(this IQueryable<Copy> copies)
    {
        return copies.Select(c=>new CopyDto(c.Id,c.Book.ToDto(),c.Available));

            //new CopyDto(copy.Id,
            //new BookDto( copy.BookId,copy.Book.Title,copy.Book.ISBN,copy.Book.Genere
            //,copy.Book.AdditionalNotes??"",copy.Book.PublicationDate,copy.Book.UploadedFileId),
            //copy.Available);
    }
}
