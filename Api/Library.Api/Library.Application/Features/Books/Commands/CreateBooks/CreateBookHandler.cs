
namespace Library.Application.Features.Books.Commands.CreateBooks;

public sealed class CreateBookHandler
    (ILogger<CreateBookHandler>logger,IAppDbContext context,IFileStorage fileStorage)
    : IRequestHandler<CreateBookCommand, Result<BookDto>>
{
    public async Task<Result<BookDto>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        Result<UploadFileDto> uploadFileResult =await fileStorage
            .UploadAsync(request.Image,request.ContentType,request.FileName,cancellationToken);
        if (uploadFileResult.IsError)
        {
            logger.LogError(string.Join(" - ",uploadFileResult.Errors));
            return uploadFileResult.Errors;
        }
        UploadFileDto dto = uploadFileResult.Value;
        Result<UploadedFile> CreateFileResult =UploadedFile.Create(dto.FileName,dto.StoredFileName,dto.ContentType,
            dto.Extension,dto.Length) ;
        if (CreateFileResult.IsError)
        {
            logger.LogError(string.Join(" - ", CreateFileResult.Errors));
            return CreateFileResult.Errors;
        }

        Result<Book> CreateBookResult = Book.Create(request.Title,request.ISBN,request.PublicationDate,
            request.Genere,request.AdditionalDetails,CreateFileResult.Value.Id,request.NumberOfCopies);
        if (CreateBookResult.IsError) {
            logger.LogError(string.Join(" - ", CreateBookResult.Errors));
            return CreateBookResult.Errors;
        }

        await context.Books.AddAsync(CreateBookResult.Value,cancellationToken);
        await context.UploadedFiles.AddAsync(CreateFileResult.Value,cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        
        return CreateBookResult.Value.ToDto();
    }
}
