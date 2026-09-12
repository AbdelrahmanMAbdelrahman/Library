
namespace Library.Application.Features.Books.Commands.DeleteBooks;

public sealed class DeleteBookHandler(ILogger<DeleteBookHandler>logger,IAppDbContext context) 
    : IRequestHandler<DeleteBookCommand, Result<Deleted>>
{
    public async Task<Result<Deleted>> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        Book? book = await context.Books.FindAsync(request.Id);
        if (book is null)
        {
            logger.LogError($"no book found for id = {request.Id}");
            return ApplicationErrors.BookNotFound(request.Id);
        }

         book.Copies.Select(c => c).ToList().Clear();
         context.Books.Remove(book);

        return Result.Deleted;

    }
}
