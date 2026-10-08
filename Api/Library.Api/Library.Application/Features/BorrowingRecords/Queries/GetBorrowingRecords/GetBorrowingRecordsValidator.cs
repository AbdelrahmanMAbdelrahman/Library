namespace Library.Application.Features.BorrowingRecords.Queries.GetBorrowingRecords;

public class GetBorrowingRecordsValidator:AbstractValidator<GetBorrowingRecordsQuery>
{
    public GetBorrowingRecordsValidator()
    {
        RuleFor(c => c.PageNumber).Must(p => p > 0 && p < 1000)// small library
        .WithMessage("'{PropertyName}' must be in range '{MinValue}' : '{MaxValue}'");
        RuleFor(c => c.PageSize).Must(p => p > 0 && p < 20)
            .WithMessage("'{PropertyName}' must be in range '{MinValue}' : '{MaxValue}'");
    }
}
