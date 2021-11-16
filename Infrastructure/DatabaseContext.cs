using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
  public class DatabaseContext : DbContext
  {
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    public DbSet<MailMessageTemplate> MailMessageTemplates { get; set; }

    public DbSet<File> Files { get; set; }

    public DbSet<Message> Messages { get; set; }

    public DbSet<OutboxMessage> OutboxMessages { get; set; }
  }
}
