
using Library.Domain.Fines.Enums;

namespace Library.Infrastructure.Configuration;

public sealed class FineConfiguration : IEntityTypeConfiguration<Fine>
{
    public void Configure(EntityTypeBuilder<Fine> builder)
    {
        builder.HasIndex(f=>f.Id);
        //builder.Property(f=>f.PaymentStatus).IsRequired().HasDefaultValue(PaymentStatus.UnPaid);
        builder.Property(f => f.FineAmount).IsRequired();
        builder.Property(f=>f.NumberOfLateDays).IsRequired();
        builder.Property(f=>f.BorrowingRecordId).IsRequired();
        builder.HasOne(f => f.BorrowingRecord).WithOne(br=>br.Fine);
        builder.HasOne(f => f.AppUser).WithMany(u => u.Fines).HasForeignKey(f=>f.AppUserId);
    }
}
