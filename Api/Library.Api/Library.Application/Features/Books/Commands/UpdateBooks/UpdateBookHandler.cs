
namespace Library.Application.Features.Books.Commands.UpdateBooks
{
    public sealed class UpdateBookHandler(IAppDbContext context,ILogger<UpdateBookHandler>logger) 
        : IRequestHandler<UpdateBookCommand, Result<Updated>>
    {
        public async Task<Result<Updated>> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            Book? book = await context.Books.FindAsync(request.Id);
            if(book is null)
            {
                logger.LogError($"no book found for id = {request.Id}");
                return ApplicationErrors.BookNotFound(request.Id);
            }

            Result<Updated> UpdateBookResult = book.Update(request.Title,request.ISBN,
                request.PublicationDate,request.Genere,request.AdditionalDetails);
            if (UpdateBookResult.IsError)
            {
                logger.LogError(string.Join(" - ", UpdateBookResult.Errors));
                return UpdateBookResult.Errors;
            }

            await context.SaveChangesAsync(cancellationToken);

            return Result.Updated;
        }
    }
}
