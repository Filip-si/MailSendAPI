using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityTypeConfiguration
{
  public class FileEntityTypeConfiguration : IEntityTypeConfiguration<File>
  {
    public void Configure(EntityTypeBuilder<File> builder)
    {
      builder.HasKey(x => x.FilesId);
      builder.Property(x => x.FilesId);

      builder.HasOne(x => x.FileHeader)
        .WithOne(y => y.Files)
        .HasForeignKey<File>(y => y.FileHeaderId);

      builder.HasOne(x => x.FileBody)
        .WithOne(y => y.Files)
        .HasForeignKey<File>(y => y.FileBodyId);

      builder.HasOne(x => x.FileFooter)
        .WithOne(y => y.Files)
        .HasForeignKey<File>(y => y.FileFooterId);
    }
  }
}
