using Library.Application.Features.Books.Commands.CreateBooks;
namespace Library.Application.SubCutaneousTests.Features.Books.Commands.CreateBooks;

public class CreateBook_validatorTests()
{
   private readonly CreateBookValidator _validator = new CreateBookValidator();
    [Fact]
    public void Validate_ShouldPass_ForValidData()
    {
        CreateBookCommand command = new CreateBookCommand("C++", "isbn", "Prog", "no det", DateTime.UtcNow
            , 2, null, "", "");
        var result = _validator.Validate(command);
        Assert.True(result.IsValid);

    }
    [Theory]
    [InlineData("", "isbn", "Prog", "no det",2)]
    [InlineData("C++", "", "Prog", "no det",2)]
    [InlineData("C++", "isbn", "", "no det",2)]
    [InlineData("C++", "isbn", "Prog", "",2)]
    [InlineData("C++", "isbn", "Prog", "no det",0)]
    public void Validate_ShouldFail_ForInValidData(string title,string isbn,string genre,string notes,int numberOfCopies)
    {
        CreateBookCommand command = new CreateBookCommand(title, isbn, genre, notes, DateTime.UtcNow
           , numberOfCopies, null, "", "");
        var result = _validator.Validate(command);
        Assert.False(result.IsValid);
    }
    [Fact]
    public void Validate_ShouldFail_ForInValidDate()
    {
        CreateBookCommand command = new CreateBookCommand("C++", "isbn", "Prog", "no det",
            DateTime.UtcNow.AddDays(1)
            , 2, null, "", "");
        var result = _validator.Validate(command);
        Assert.False(result.IsValid);
    }
}
