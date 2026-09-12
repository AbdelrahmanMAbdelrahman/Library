namespace Library.Application.Features.UploadedFiles.Queries.GetFileQuerys;

public sealed class GetFileValidator:AbstractValidator<GetFileQuery>
{
    public GetFileValidator()
    {
        RuleFor(f=>f.Id).NotEmpty().WithMessage("Must provide '{PropertyName}'");
    }
}
