namespace Library.Application.Features.UploadedFiles.Dtos;

public sealed record DownloadFileDto(Stream FileStream,string ContentType,string StoredFileName);
