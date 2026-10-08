

namespace Library.Application.Features.Fines.Commands.PayFines
{
    public sealed class PayFineValidator:AbstractValidator<PayFineCommand>
    {
        public PayFineValidator() {
            RuleFor(f => f.Id).Must(i => i != Guid.Empty).WithMessage("Provide a valid '{PropertyName}'");
                }
    }
}
