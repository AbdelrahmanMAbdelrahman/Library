namespace Library.Application.Features.BorrowingRecords.Queries.GetBorrowingRecord;

public sealed class GetBorrowingRecordValidator:AbstractValidator<GetBorrowingRecordCommand>
{
    public GetBorrowingRecordValidator()
    {
        RuleFor(b => b.Id).NotEmpty().WithMessage("must provide '{PropertyName}'");
    }
}
