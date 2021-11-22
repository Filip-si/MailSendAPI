using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityTypeConfiguration
{
  public class FileFootersEntityTypeConfiguration : IEntityTypeConfiguration<FileFooter>
  {
    public void Configure(EntityTypeBuilder<FileFooter> builder)
    {
      builder.HasKey(x => x.FileFooterId);
      builder.Property(x => x.FileFooterId);
    }
  }
}
