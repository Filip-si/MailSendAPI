using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityTypeConfiguration
{
  public class TemplateEntityTypeConfiguration : IEntityTypeConfiguration<Template>
  {
    public void Configure(EntityTypeBuilder<Template> builder)
    {
      builder.HasKey(x => x.TemplateId);
      builder.Property(x => x.TemplateId)
        .IsRequired();

      builder.Property(x => x.DataTemplate)
        .HasMaxLength(1000);

      builder.Property(x => x.TextTemplate)
        .HasMaxLength(1000);

      builder.HasOne(x => x.Files)
        .WithOne(y => y.Template)
        .HasForeignKey<Template>(y => y.FilesId);
    }
  }
}
