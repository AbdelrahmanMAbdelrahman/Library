using Library.Domain.Common.Results;
using Library.Domain.Fines;
using Library.Domain.Fines.Enums;
using Library.Infrastructure.Migrations;
using Library.Tests.Common.Fines;
namespace Library.Domain.Tests.Fines;

public class FineTests
{
    [Fact]
    public void CreateFine_ShouldPass_ForValidData()
    {
        PaymentStatus paymentStatus = PaymentStatus.UnPaid;
        double fineAmount = 10;
        Guid borrowingRecordId = Guid.NewGuid();
        int numberOfLateDays = 2;
        string userId = Guid.NewGuid().ToString();
        Result<Fine> fineRes = FineFactory.CreateFine(
            paymentStatus, fineAmount, borrowingRecordId, numberOfLateDays, userId
            );
        Assert.True(fineRes.IsSuccess);
        Fine fine = fineRes.Value;
        Assert.Equal(paymentStatus, fine.PaymentStatus);
        Assert.Equal(borrowingRecordId, fine.BorrowingRecordId);
        Assert.Equal(numberOfLateDays, fine.NumberOfLateDays);
        Assert.Equal(userId, fine.AppUserId);

    }
    [Fact]
    public void CreateFine_ShouldFail_ForInValidPaymentStatus()
    {
        PaymentStatus paymentStatus = PaymentStatus.Paid;
        double fineAmount = 5;
        Guid borrowingRecordId = Guid.NewGuid();
        int numberOfLateDays = 2;
        string userId = Guid.NewGuid().ToString();
        Result<Fine> fineRes = FineFactory.CreateFine(
            paymentStatus, fineAmount, borrowingRecordId, numberOfLateDays, userId
            );
        Assert.False(fineRes.IsSuccess);


    }
    [Fact]
    public void CreateFine_ShouldFail_ForInValidUserId()
    {
        PaymentStatus paymentStatus = PaymentStatus.UnPaid;
        double fineAmount = 5;
        Guid borrowingRecordId = Guid.NewGuid();
        int numberOfLateDays = 2;

        Result<Fine> fineRes = FineFactory.CreateFine(
            paymentStatus, fineAmount, borrowingRecordId, numberOfLateDays, ""
            );
        Assert.False(fineRes.IsSuccess);


    }
    [Fact]
    public void CreateFine_ShouldFail_ForInValidBorrowingId()
    {
        PaymentStatus paymentStatus = PaymentStatus.UnPaid;
        double fineAmount = 5;
        int numberOfLateDays = 2;
        string userId = Guid.NewGuid().ToString();
        Result<Fine> fineRes = FineFactory.CreateFine(
            paymentStatus, fineAmount, Guid.Empty, numberOfLateDays, userId
            );
        Assert.False(fineRes.IsSuccess);
    }
    [Theory]
    [InlineData(1, -1)]
    [InlineData(-1, 1)]
    [InlineData(101, 1000)]
    public void CreateFine_ShouldFail_ForInValidData(double fineAmount, int numberOfLateDays)
    {
        PaymentStatus paymentStatus = PaymentStatus.UnPaid;
        Guid borrowingRecordId = Guid.NewGuid();
        string userId = Guid.NewGuid().ToString();
        Result<Fine> fineRes = FineFactory.CreateFine(
            paymentStatus, fineAmount, borrowingRecordId, numberOfLateDays, userId
            );
        Assert.False(fineRes.IsSuccess);
    }

    [Fact]
    public void UpdateFine_ShouldPass_ForValidData()
    {
        Result<Fine> fineRes = FineFactory.CreateFine(
            paymentStatus:PaymentStatus.UnPaid, fineAmount:15, borrowingRecordId:Guid.NewGuid(),
            numberOfLateDays:3, userId:Guid.NewGuid().ToString()
            );
        Fine fine = fineRes.Value;
        Result<Updated> updateFineRes = fine.UpdateFine(4,20,PaymentStatus.UnPaid);
        Assert.True(updateFineRes.IsSuccess);
        
    }
    [Theory]
    [InlineData(10,1)]
    [InlineData(10,0)]
    [InlineData(0,1)]
    [InlineData(-1,1)]
    [InlineData(10,-1)]
    [InlineData(-1,-1)]
    public void UpdateFine_ShouldFail_ForInvalidData(double fineAmount,int numberOfLateDays)
    {
        Result<Fine> fineRes = FineFactory.CreateFine(
            paymentStatus: PaymentStatus.UnPaid, fineAmount: 15, borrowingRecordId: Guid.NewGuid(),
            numberOfLateDays: 3, userId: Guid.NewGuid().ToString()
            );
        Fine fine = fineRes.Value;
        Result<Updated> updateFineRes = fine.UpdateFine(numberOfLateDays, fineAmount, PaymentStatus.UnPaid);
        Assert.False(updateFineRes.IsSuccess);
    }
}
