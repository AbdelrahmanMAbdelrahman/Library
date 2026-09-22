
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
            //BorrowingRecord? borrowingRecord =await context.BorrowingRecords.AsNoTracking()
            //    .FirstOrDefaultAsync(br=>br.CopyId==request.Id&&br.AppUserId==user.Id,cancellationToken);
            //if(borrowingRecord is null)
            //{
            //    logger.LogError($"invalid copy with id = {request.Id} for uesr with id = ${user.Id}");
            //    return ApplicationErrors.CopyNotFound(request.Id);
            //}
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
