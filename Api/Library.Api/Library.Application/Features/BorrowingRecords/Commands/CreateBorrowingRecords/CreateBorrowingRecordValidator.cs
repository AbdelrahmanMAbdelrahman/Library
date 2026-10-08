namespace Library.Application.Features.BorrowingRecords.Commands.CreateBorrowingRecords;

public sealed class CreateBorrowingRecordValidator:AbstractValidator<CreateBorrowingRecordCommand>
{
    public CreateBorrowingRecordValidator()
    {
        RuleFor(b => b.CopyId).Must(i=>i!=Guid.Empty).WithMessage("Must Provide valid'{PropertyName}'");
        RuleFor(b => b.BorrowingDate).NotEmpty()
            .Must(d=>d<=DateTime.UtcNow)
            .WithMessage("Must Provide '{PropertyName}'");
       
            
     
    }
}
