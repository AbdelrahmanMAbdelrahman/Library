using Library.Application.Features.Books.Commands.CreateBooks;
using Library.Application.Features.Books.Commands.UpdateBooks;

namespace Library.Application.SubCutaneousTests.Features.Books.Commands.UpdateBooks;

public class UpdateBookValidatorTests
{
    private readonly UpdateBookValidator _validator = new();
    [Fact]
    public void Validate_ShouldPass_ForValidData()
    {
        UpdateBookCommand command = new UpdateBookCommand(Guid.Parse("6ac73dd6-739c-83e8-b06e-d37f9e8da782"),
            "C++", "isbn", "Prog", "no det", DateTime.UtcNow
            ,  null, "", "");
        var result = _validator.Validate(command);
        Assert.True(result.IsValid);

    }
    [Theory]
    [InlineData("", "isbn", "Prog", "no det")]
    [InlineData("C++", "", "Prog", "no det")]
    [InlineData("C++", "isbn", "", "no det")]
    [InlineData("C++", "isbn", "Prog", "")]
    [InlineData("C++", "isbn", "Prog", "no det")]
    public void Validate_ShouldFail_ForInValidData(string title, string isbn, string genre, string notes)
    {
        UpdateBookCommand command = new UpdateBookCommand(
            Guid.TryParse("6ac73dd6-739c-",out Guid id)?id:Guid.Empty
            , title, isbn, genre, notes, DateTime.UtcNow
           , null, "", "");
        var result = _validator.Validate(command);
        Assert.False(result.IsValid);
    }
    [Fact]
    public void Validate_ShouldFail_ForInValidDate()
    {
        UpdateBookCommand command = new UpdateBookCommand(
            Guid.TryParse("6ac73dd6-739c-83e8-b06e-d37f9e8da782", out Guid id) ? id : Guid.Empty
            , "C++", "isbn", "Prog", "no det",
            DateTime.UtcNow.AddDays(1)
            , null, "", "");
        var result = _validator.Validate(command);
        Assert.False(result.IsValid);
    }
}
