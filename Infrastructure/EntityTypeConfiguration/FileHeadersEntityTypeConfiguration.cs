using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityTypeConfiguration
{
  public class FileHeaderEntityTypeConfiguration : IEntityTypeConfiguration<FileHeader>
  {
    public void Configure(EntityTypeBuilder<FileHeader> builder)
    {
      builder.HasKey(x => x.FileHeaderId);
      builder.Property(x => x.FileHeaderId);
    }
  }
}
