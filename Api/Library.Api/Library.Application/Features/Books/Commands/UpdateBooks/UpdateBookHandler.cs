
namespace Library.Application.Features.Books.Commands.UpdateBooks
{
    public sealed class UpdateBookHandler(IAppDbContext context,ILogger<UpdateBookHandler>logger) 
        : IRequestHandler<UpdateBookCommand, Result<Updated>>
    {
        public async Task<Result<Updated>> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            Copy? copy = await context.Copies
                .Include(c=>c.Book)
                .FirstOrDefaultAsync(c=>c.Id== request.CopyId);
            if(copy is null)
            {
                logger.LogError($"no book found for id = {request.CopyId}");
                return ApplicationErrors.BookNotFound(request.CopyId);
            }

            Result<Updated> UpdateBookResult = copy.Book.Update(request.Title,request.ISBN,
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
