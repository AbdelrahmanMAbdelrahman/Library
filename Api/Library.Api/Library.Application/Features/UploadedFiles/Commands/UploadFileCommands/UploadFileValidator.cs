namespace Library.Application.Features.UploadedFiles.Commands.UploadFileCommands;

public sealed class UploadFileValidator:AbstractValidator<UploadFileCommand>
{
    public UploadFileValidator()
    {
        RuleFor(f=>f.ContentType);
    }
}
