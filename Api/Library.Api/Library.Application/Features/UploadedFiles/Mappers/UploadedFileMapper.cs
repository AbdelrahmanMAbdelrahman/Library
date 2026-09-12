namespace Library.Application.Features.UploadedFiles.Mappers;

public static class UploadedFileMapper
{
    public static UploadFileDto ToDto(this UploadedFile uploadedFile)
    {
        Stream content = null;
        return new UploadFileDto(uploadedFile.ContentType,uploadedFile.FileName,uploadedFile.StoredFileName,
            uploadedFile.Extension,uploadedFile.FileSize); //----------------------------------  problem
    }
}
