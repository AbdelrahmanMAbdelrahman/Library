using Library.Domain.UplodedFiles;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Infrastructure.Configuration;
public sealed class UploadedFileConfiguration : IEntityTypeConfiguration<UploadedFile>
{
    public void Configure(EntityTypeBuilder<UploadedFile> builder)
    {
        builder.HasKey(x => x.Id).IsClustered();
        builder.Property(f => f.ContentType).IsRequired();
        builder.Property(f => f.Extension).IsRequired();
        builder.Property(f => f.FileName).IsRequired();
        builder.Property(f => f.FileSize).IsRequired();
        builder.Property(f => f.StoredFileName).IsRequired();

    }
}
