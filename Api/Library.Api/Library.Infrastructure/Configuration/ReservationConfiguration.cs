namespace Library.Infrastructure.Configuration;

public sealed class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.HasKey(r => r.Id).IsClustered();
        builder.Property(r => r.ReservationDate).IsRequired();
        builder.HasOne(r => r.User).WithMany(u=>u.Reservations);
        builder.HasOne(r => r.Copy).WithMany(c => c.Reservation);
        builder.Property(r => r.RowVersion).IsRowVersion();
    }
}
