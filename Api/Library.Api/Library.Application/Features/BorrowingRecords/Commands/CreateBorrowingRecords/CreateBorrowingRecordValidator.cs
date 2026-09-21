namespace Library.Application.Features.BorrowingRecords.Commands.CreateBorrowingRecords;

public sealed class CreateBorrowingRecordValidator:AbstractValidator<CreateBorrowingRecordCommand>
{
    public CreateBorrowingRecordValidator()
    {
        RuleFor(b => b.CopyId).NotEmpty().WithMessage("Must Provide '{PropertyName}'");
        RuleFor(b => b.BorrowingDate).NotEmpty().WithMessage("Must Provide '{PropertyName}'");
       
            
     
    }
}
