namespace Library.Domain.UplodedFiles;

public sealed class UploadedFile:Audit
{
    public  string FileName { get;private set; }
    public  string StoredFileName { get;private set; }
    public  string ContentType { get;private set; }
    public  string Extension { get;private set; }
    public long FileSize { get;private set; }
    public Book book { get;private set; }
    public UploadedFile() { }
    public UploadedFile(Guid id, string fileName, string storedFileName, string contentType,
        string extension, long fileSize)
    {
        this.Id = id;
        this.FileName = fileName;
        this.StoredFileName = storedFileName;
        this.ContentType = contentType;
        this.Extension = extension;
        this.FileSize = fileSize;
    }

    public static Result<UploadedFile> Create( string fileName, string storedFileName, string contentType,
        string extension, long fileSize)
    {
        
        if (string.IsNullOrWhiteSpace(fileName)) return UploadedFileErrors.InvalidFileName;
        if (string.IsNullOrWhiteSpace(storedFileName)) return UploadedFileErrors.InvalidStoredFileName;
        if (string.IsNullOrWhiteSpace(contentType)) return UploadedFileErrors.InvalidContentType;
        if (string.IsNullOrWhiteSpace(extension)) return UploadedFileErrors.InvalidExtension;
        if (fileSize<0) return UploadedFileErrors.InvalidFileSize;

        return new UploadedFile(Guid.NewGuid(),fileName,storedFileName,contentType,extension,fileSize);
    }

    public Result<Updated> Update(string fileName, string storedFileName, string contentType,
        string extension, long fileSize)
    {
        this.FileName= fileName;
        this.StoredFileName= storedFileName;
        this.ContentType= contentType;
        this.Extension = extension;
        this.FileSize = fileSize;

        if (string.IsNullOrWhiteSpace(fileName)) return UploadedFileErrors.InvalidFileName;
        if (string.IsNullOrWhiteSpace(storedFileName)) return UploadedFileErrors.InvalidStoredFileName;
        if (string.IsNullOrWhiteSpace(contentType)) return UploadedFileErrors.InvalidContentType;
        if (string.IsNullOrWhiteSpace(extension)) return UploadedFileErrors.InvalidExtension;
        if (fileSize < 0) return UploadedFileErrors.InvalidFileSize;

        return Result.Updated;
    }
}
