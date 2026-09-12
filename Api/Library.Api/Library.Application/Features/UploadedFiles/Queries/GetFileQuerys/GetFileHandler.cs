
namespace Library.Application.Features.UploadedFiles.Queries.GetFileQuerys;

public sealed class GetFileHandler(ILogger<GetFileHandler>logger,IAppDbContext context,IFileStorage storage) 
    : IRequestHandler<GetFileQuery, Result<DownloadFileDto>>
{
    string Directory = @"C:\LibraryBooksImages\";
    public async Task<Result<DownloadFileDto>> Handle(GetFileQuery request, CancellationToken cancellationToken)
    {
        Result<DownloadFileDto> result =await storage.DownloadAsync(request.Id,cancellationToken);
        //UploadedFile? uploadedFile = await context.UploadedFiles.FindAsync(request.Id);
        //if(uploadedFile is null)
        //{
        //    logger.LogError($"NO file found for id = {request.Id}");
        //    return ApplicationErrors.FileNotFound(request.Id);
        //}
        string FilePath = Path.Combine(Directory,result.Value.StoredFileName);
        return new DownloadFileDto(File.OpenRead(FilePath), result.Value.ContentType, result.Value.StoredFileName);
    }
}
