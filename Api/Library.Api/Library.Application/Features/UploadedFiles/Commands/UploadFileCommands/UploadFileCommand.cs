namespace Library.Application.Features.UploadedFiles.Commands.UploadFileCommands;

public sealed record UploadFileCommand(Stream Content, string ContentType, string FileName) 
    :IRequest<Result<UploadFileDto>>
{
}
