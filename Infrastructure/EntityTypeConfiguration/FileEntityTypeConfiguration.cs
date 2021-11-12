using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityTypeConfiguration
{
  public class FileEntityTypeConfiguration : IEntityTypeConfiguration<File>
  {
    public void Configure(EntityTypeBuilder<File> builder)
    {
      builder.HasKey(x => x.FileId);
      builder.Property(x => x.FileId)
        .IsRequired();

      builder.HasOne(x => x.MailMessageTemplate)
        .WithMany(x => x.Files)
        .HasForeignKey(x => x.FileId);

      builder.Property(x => x.FileName)
        .HasMaxLength(50);

      builder.Property(x => x.ContentType)
        .HasMaxLength(250);
    }
  }
}
