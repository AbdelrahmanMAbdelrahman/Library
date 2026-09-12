namespace Library.Application.Common.Interfaces;
public interface IFileStorage
{
    Task<Result<UploadFileDto>> UploadAsync(Stream file, string ContentType, string FileName, CancellationToken ct);
    Task<Result<DownloadFileDto>> DownloadAsync(Guid Id, CancellationToken ct);
}
