using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityTypeConfiguration
{
  public class MessageEntityTypeConfiguration : IEntityTypeConfiguration<Message>
  {
    public void Configure(EntityTypeBuilder<Message> builder)
    {
      builder.HasKey(x => x.MessageId);
      builder.Property(x => x.MessageId)
        .IsRequired();

      builder.HasOne(x => x.MailMessageTemplate)
        .WithMany(x => x.Messages)
        .HasForeignKey(x => x.MessageId);

      builder.Property(x => x.From)
        .HasMaxLength(100);

      builder.Property(x => x.To)
        .HasMaxLength(100);
    }
  }
}
