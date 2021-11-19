using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityTypeConfiguration
{
  public class FileAttachmentEntityTypeConfiguration : IEntityTypeConfiguration<FileAttachment>
  {
    public void Configure(EntityTypeBuilder<FileAttachment> builder)
    {
      builder.HasKey(x => x.FileAttachmentId);
      builder.Property(x => x.FileAttachmentId);

      builder.HasOne(x => x.Files)
        .WithMany(x => x.FilesAttachments)
        .HasForeignKey(x => x.FilesId);

      builder.Property(x => x.FileName)
        .HasMaxLength(50);

      builder.Property(x => x.ContentType)
        .HasMaxLength(250);
    }
  }
}
