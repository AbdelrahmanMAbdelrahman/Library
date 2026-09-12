namespace Library.Infrastructure.Configuration;

public sealed class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.HasKey(r => r.Id).IsClustered();
        builder.Property(r => r.ReservationDate).IsRequired();
        builder.HasOne(r => r.User).WithMany(u=>u.Reservations);
        builder.HasOne(r => r.Copy).WithOne(c => c.Reservation);
    }
}
