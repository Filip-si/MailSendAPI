using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityTypeConfiguration
{
  public class FilesEntityTypeConfiguration : IEntityTypeConfiguration<Files>
  {
    public void Configure(EntityTypeBuilder<Files> builder)
    {
      builder.HasKey(x => x.FilesId);
      builder.Property(x => x.FilesId);

      builder.HasOne(x => x.FileHeader)
        .WithOne(y => y.Files)
        .HasForeignKey<Files>(y => y.FileHeaderId);

      builder.HasOne(x => x.FileBody)
        .WithOne(y => y.Files)
        .HasForeignKey<Files>(y => y.FileBodyId);

      builder.HasOne(x => x.FileFooter)
        .WithOne(y => y.Files)
        .HasForeignKey<Files>(y => y.FileFooterId);
    }
  }
}
