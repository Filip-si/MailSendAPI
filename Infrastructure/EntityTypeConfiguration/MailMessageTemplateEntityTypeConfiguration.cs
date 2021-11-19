//using Domain.Entities;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace Infrastructure.EntityTypeConfiguration
//{
//  public class MailMessageTemplateEntityTypeConfiguration : IEntityTypeConfiguration<Template>
//  {
//    public void Configure(EntityTypeBuilder<Template> builder)
//    {
//      builder.HasKey(x => x.TemplateId);
//      builder.Property(x => x.TemplateId)
//        .IsRequired();

//      builder.Property(x => x.Subject)
//        .HasMaxLength(100);

//      builder.Property(x => x.Body)
//        .HasMaxLength(500);
//    }
//  }
//}
