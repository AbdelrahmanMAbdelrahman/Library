namespace Library.Infrastructure.Services;

public sealed class FileStorageService(ILogger<FileStorageService>logger,IAppDbContext context) :
    IFileStorage
{
    private readonly string DirectoryPath = @"C:\LibraryBooksImages\";

    public  async Task<Result<DownloadFileDto>> DownloadAsync(Guid Id, CancellationToken ct)
    {
        UploadedFile? uploadedFile = await context.UploadedFiles.FindAsync(Id);
        if(uploadedFile is null)
        {
            logger.LogError($"no file exist with id = {Id}");
            return ApplicationErrors.FileNotFound(Id);
        }
        string FilePath=Path.Combine(DirectoryPath, uploadedFile.StoredFileName);
        if (!File.Exists(FilePath)) {
            logger.LogError($"no file exist with id = {Id}");
            return ApplicationErrors.FileNotFound(Id);
        }
         using Stream stream = new FileStream(FilePath,FileMode.Open,FileAccess.Read);
        string contentType = "image/jpeg";
        return new DownloadFileDto(stream,contentType,uploadedFile.StoredFileName);
    }

    public async Task<Result<UploadFileDto>> UploadAsync(Stream file, string ContentType, string FileName, CancellationToken ct)
    {
        if(!Directory.Exists(DirectoryPath))
            Directory.CreateDirectory(DirectoryPath);

        string Extn=Path.GetExtension(FileName);
        string StoredFileName = $"{Guid.NewGuid()}{Extn}";
        string FilePath=Path.Combine(DirectoryPath, StoredFileName);
        await using Stream stream = new FileStream(FilePath,FileMode.Create);
        await file.CopyToAsync(stream,ct);
        return new UploadFileDto(ContentType,FileName,StoredFileName,Extn,file.Length);
    }


}
