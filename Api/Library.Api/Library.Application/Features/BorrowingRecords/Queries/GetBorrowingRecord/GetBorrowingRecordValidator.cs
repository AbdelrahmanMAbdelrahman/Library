namespace Library.Application.Features.BorrowingRecords.Queries.GetBorrowingRecord;

public sealed class GetBorrowingRecordValidator:AbstractValidator<GetBorrowingRecordQuery>
{
    public GetBorrowingRecordValidator()
    {
        RuleFor(b => b.Id).Must(id=>id!=Guid.Empty).WithMessage("must provide valid '{PropertyName}'");
    }
}
