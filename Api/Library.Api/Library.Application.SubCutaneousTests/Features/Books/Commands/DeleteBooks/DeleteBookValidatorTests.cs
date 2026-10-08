using Library.Application.Features.Books.Commands.DeleteBooks;

namespace Library.Application.SubCutaneousTests.Features.Books.Commands.DeleteBooks;

public class DeleteBookValidatorTests
{
    private readonly DeleteCopyValidator _validtor = new();
    [Fact]
    public void Validate_ShouldPass_ForValidData()
    {
        DeleteCopyCommand command = new DeleteCopyCommand(Guid.NewGuid());
        var result = _validtor.Validate(command);
        Assert.True(result.IsValid);
    }
    [Fact]
    public void Validate_ShouldFail_FoInValidCopyId()
    {
        DeleteCopyCommand command = new DeleteCopyCommand(Guid.Empty);
        var result = _validtor.Validate(command);
        Assert.False(result.IsValid);
    }
}
