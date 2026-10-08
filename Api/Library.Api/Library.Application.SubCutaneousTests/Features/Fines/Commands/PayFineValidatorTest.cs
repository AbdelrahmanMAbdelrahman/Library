using Library.Application.Features.Fines.Commands.PayFines;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Library.Application.SubCutaneousTests.Features.Fines.Commands;

public class PayFineValidatorTest
{
    private readonly PayFineValidator  _validator=new();
    [Fact]
    public void Validate_ShouldPass_ForValidData()
    {
        var command = new PayFineCommand(Guid.Parse("6ac73dd6-739c-83e8-b06e-d37f9e8da782"));
        var result=_validator.Validate(command);
        Assert.True(result.IsValid);
    }
    [Fact]
    public void Validate_ShouldFail_ForInValidId()
    {
        var command = new PayFineCommand(Guid.TryParse("6ac73dd6-",out Guid id)?id:Guid.Empty);
        var result=_validator.Validate(command);
        Assert.False(result.IsValid);
    }
}
