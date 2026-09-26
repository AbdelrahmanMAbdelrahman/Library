namespace Library.Application.Features.Fines.Mappers;

public static class FineMappers
{
    public static FineDto ToDto(this Fine fine)
    {
        return new FineDto(fine.Id,fine.BorrowingRecord.ToDto(),fine.BorrowingRecordId,fine.NumberOfLateDays,
            fine.FineAmount,fine.PaymentStatus);
    }
    public static IQueryable<FineDto> ToDto(this IQueryable<Fine> fines)
    {
        return fines.Select(f=>f.ToDto());
    }
}
