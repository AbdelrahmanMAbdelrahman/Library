namespace Library.Infrastructure.Configuration;
public sealed class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b=>b.Title).IsRequired().HasMaxLength(255);
        builder.Property(b=>b.ISBN).IsRequired().HasMaxLength(255);
        builder.Property(b=>b.Genere).IsRequired().HasMaxLength(255);
        builder.Property(b=>b.PublicationDate).IsRequired();
        builder.HasMany(b => b.Copies).WithOne(c=>c.Book);
        builder.HasOne(b => b.uploadedFile).WithOne(f=>f.book);
    }
}
