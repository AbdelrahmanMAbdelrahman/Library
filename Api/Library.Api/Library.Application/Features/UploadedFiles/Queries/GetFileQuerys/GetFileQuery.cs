namespace Library.Application.Features.UploadedFiles.Queries.GetFileQuerys;

public sealed record GetFileQuery(Guid Id):IRequest<Result<DownloadFileDto>>
{
}
