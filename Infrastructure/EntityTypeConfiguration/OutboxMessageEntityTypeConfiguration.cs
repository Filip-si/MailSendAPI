using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.EntityTypeConfiguration
{
  public class OutboxMessageEntityTypeConfiguration : IEntityTypeConfiguration<OutboxMessage>
  {
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
      builder.HasKey(x => x.OutboxMessageId);
      builder.Property(x => x.OutboxMessageId)
        .IsRequired();

      builder.HasOne(x => x.Message)
        .WithOne(y => y.OutboxMessage)
        .HasForeignKey<OutboxMessage>(y => y.MessageId);
    }
  }
}
