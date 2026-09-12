
namespace Library.Application.Features.UploadedFiles.Commands.UploadFileCommands;

internal class UploadFileHandler : IRequestHandler<UploadFileCommand, Result<UploadFileDto>>
{
    public Task<Result<UploadFileDto>> Handle(UploadFileCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
