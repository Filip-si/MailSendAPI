using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityTypeConfiguration
{
  public class FileBodiesEntityTypeConfiguration : IEntityTypeConfiguration<FileBody>
  {
    public void Configure(EntityTypeBuilder<FileBody> builder)
    {
      builder.HasKey(x => x.FileBodyId);
      builder.Property(x => x.FileBodyId);
    }
  }
}
