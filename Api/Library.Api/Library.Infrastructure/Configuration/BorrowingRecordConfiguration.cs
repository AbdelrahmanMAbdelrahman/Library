namespace Library.Infrastructure.Configuration;

public sealed class BorrowingRecordConfiguration : IEntityTypeConfiguration<BorrowingRecord>
{
    public void Configure(EntityTypeBuilder<BorrowingRecord> builder)
    {
       builder.HasKey(x => x.Id);
        builder.Property(b => b.BorrowingDate).IsRequired();
        builder.Property(b=>b.DueDate).IsRequired();
        builder.HasOne(b => b.Copy).WithOne(c=>c.BorrowingRecord);
        builder.HasOne(b => b.AppUser).WithMany(u => u.BorrowingRecords);
    }
}
