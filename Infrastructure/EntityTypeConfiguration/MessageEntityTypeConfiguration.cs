//using Domain.Entities;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using System;

//namespace Infrastructure.EntityTypeConfiguration
//{
//  public class MessageEntityTypeConfiguration : IEntityTypeConfiguration<Message>
//  {
//    public void Configure(EntityTypeBuilder<Message> builder)
//    {
//      builder.HasKey(x => x.MessageId);
//      builder.Property(x => x.MessageId)
//        .IsRequired();
//    }
//  }
//}
