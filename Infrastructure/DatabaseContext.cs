using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
  public class DatabaseContext : DbContext
  {
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    //public DbSet<Template> Templates { get; set; }

    public DbSet<FileAttachment> FileAttachments { get; set; }
    public DbSet<FileHeader> FileHeaders { get; set; }
    public DbSet<Files> Files { get; set; }

    public DbSet<Template> Templates { get; set; }

    //public DbSet<OutboxMessage> OutboxMessages { get; set; }
  }
}
