
namespace Library.Domain.UplodedFiles;

internal sealed class UploadedFileErrors
{
    internal static Error InvalidId =>Error.Validation("UploadedFileErrors.InvalidId", "Provide a valid Id");
    internal static Error InvalidFileName => Error.Validation("UploadedFileErrors.InvalidFileName", "Provide a valid File Name");
    internal static Error InvalidStoredFileName => Error.Validation("UploadedFileErrors.InvalidStoredFileName", "Provide a valid Stored File Name");
    internal static Error InvalidExtension => Error.Validation("UploadedFileErrors.InvalidExtension", "Provide a valid Extension");
    internal static Error InvalidContentType => Error.Validation("UploadedFileErrors.InvalidContentType", "Provide a valid Content Type");
    internal static Error InvalidFileSize => Error.Validation("UploadedFileErrors.InvalidFileSize", "Provide a valid File Size");
}
