namespace Library.Application.Features.Fines.Commands.CreateFines;

public sealed class CreateFinesValidator:AbstractValidator<CreateFinesCommand>
{
    public CreateFinesValidator()
    {
        RuleFor(f=>f.FineAmount)
            .NotEmpty().WithMessage("Must provide a valid value for '{PropertyName}'").Must(f=>f>0&&f<1000)
            .WithMessage("'{PropertyName}' length must be at least '{MinLength}' chars & at most '{MaxLength}' chars");

        RuleFor(f => f.NumberOfLateDays).NotEmpty().WithMessage("Must provide a valid value for '{PropertyName}'")
            .Must(f => f > 0 && f < 100)
            .WithMessage("'{PropertyName}' length must be at least '{MinLength}' chars & at most '{MaxLength}' chars");

        RuleFor(f => f.PaymentStatus).NotEmpty().WithMessage("Must provide a valid value for '{PropertyName}'")
            .Must(f => Enum.IsDefined(f)).WithMessage("Must provide a valid value for '{PropertyName}'");

        RuleFor(f => f.BorrowingRecordId).NotEmpty().WithMessage("Must provide a valid value for '{PropertyName}'");
    }
}
