
namespace Library.Application.Features.Books.Queries.GetBook;

public sealed class GetBookHandler (IAppDbContext context,ILogger<GetBookHandler> logger): IRequestHandler<GetBookQuery, Result<BookDto>>
{
    public async Task<Result<BookDto>> Handle(GetBookQuery request, CancellationToken cancellationToken)
    {
        Book? book = await context.Books.FindAsync(request.Id);
        if(book is null)
        {
            logger.LogError($"no book found with id = {request.Id}");
            return ApplicationErrors.BookNotFound(request.Id);
        }

        return new BookDto(book.Id,book.Title,book.ISBN,book.Genere,book.AdditionalNotes,book.PublicationDate,
            book.UploadedFileId);
    }
}
