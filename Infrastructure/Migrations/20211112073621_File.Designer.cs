﻿// <auto-generated />
using System;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20211112073621_File")]
    partial class File
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.Entities.File", b =>
                {
                    b.Property<Guid>("FileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ContentType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("MailMessageTemplateId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("FileId");

                    b.HasIndex("MailMessageTemplateId");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("Domain.Entities.MailMessageTemplate", b =>
                {
                    b.Property<Guid>("MailMessageTemplateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Body")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Subject")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MailMessageTemplateId");

                    b.ToTable("MailMessageTemplates");
                });

            modelBuilder.Entity("Domain.Entities.File", b =>
                {
                    b.HasOne("Domain.Entities.MailMessageTemplate", "MailMessageTemplate")
                        .WithMany("Files")
                        .HasForeignKey("MailMessageTemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MailMessageTemplate");
                });

            modelBuilder.Entity("Domain.Entities.MailMessageTemplate", b =>
                {
                    b.Navigation("Files");
                });
#pragma warning restore 612, 618
        }
    }
}
