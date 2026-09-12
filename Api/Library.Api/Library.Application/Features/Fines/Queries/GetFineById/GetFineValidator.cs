namespace Library.Application.Features.Fines.Queries.GetFineById;

public sealed class GetFineValidator:AbstractValidator<GetFineCommand>
{
    public GetFineValidator()
    {
        RuleFor(f=>f.Id).NotEmpty().WithMessage("Must provide '{PropertyName}'");
    }
}
