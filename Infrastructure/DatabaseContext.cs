using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
  public class DatabaseContext : DbContext
  {
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    public DbSet<FileAttachment> FileAttachments { get; set; }

    public DbSet<FileHeader> FileHeaders { get; set; }

    public DbSet<FileBody> FileBodies { get; set; }

    public DbSet<FileFooter> FileFooters { get; set; }

    public DbSet<File> Files { get; set; }

    public DbSet<Template> Templates { get; set; }
  }
}
