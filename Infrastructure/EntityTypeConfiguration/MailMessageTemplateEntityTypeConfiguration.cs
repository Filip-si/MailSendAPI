using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityTypeConfiguration
{
  public class MailMessageTemplateEntityTypeConfiguration : IEntityTypeConfiguration<MailMessageTemplate>
  {
    public void Configure(EntityTypeBuilder<MailMessageTemplate> builder)
    {
      builder.HasKey(x => x.MailMessageTemplateId);
      builder.Property(x => x.MailMessageTemplateId)
        .IsRequired();

      builder.Property(x => x.Subject)
        .HasMaxLength(100);

      builder.Property(x => x.Body)
        .HasMaxLength(500);
    }
  }
}
