namespace Library.Application.Features.BorrowingRecords.Queries.GetBorrowingRecords;

public sealed class GetBorrowingRecordsValidator:AbstractValidator<GetBorrowingRecordsCommand>
{
    public GetBorrowingRecordsValidator()
    {
        RuleFor(b => b.PageNumber).NotEmpty().WithMessage("Must provide '{PropertyName}'");
        RuleFor(b => b.PageSize).NotEmpty().WithMessage("Must provide '{PropertyName}'");
        RuleFor(b => b.SortColumn).Must(sc => sc.Equals("BorrowingDate") || sc.Equals("DueDate") ||
        sc == null).WithMessage("only { 'BorrowingDate' , 'DueDate' , null } is allowed for '{PropertyName}'");

    }
}
