namespace Library.Application.Features.Fines.Queries.GetFineById;

public sealed class GetFineValidator:AbstractValidator<GetFineQuery>
{
    public GetFineValidator()
    {
        RuleFor(f=>f.Id).NotEmpty().WithMessage("Must provide '{PropertyName}'");
    }
}
