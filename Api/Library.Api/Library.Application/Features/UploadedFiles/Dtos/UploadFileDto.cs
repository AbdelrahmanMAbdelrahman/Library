namespace Library.Application.Features.UploadedFiles.Dtos;

public sealed record UploadFileDto(string ContentType,string FileName,string StoredFileName,
    string Extension,long Length);
