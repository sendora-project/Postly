using Microsoft.EntityFrameworkCore;
using Sendora.Core.Misc;
using Sendora.Core.Models;

namespace Sendora.Core.Database;

public class MailsContext : DbContext
{
    private string DbPath { get; }
    
    public DbSet<MailEntity> Mails { get; init; }
    
    public MailsContext()
    {
        DbPath = Path.Join(StorageUtils.CacheFolder, "_mails.db");
        
        if (!File.Exists(DbPath))
            File.Create(DbPath).Dispose();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}