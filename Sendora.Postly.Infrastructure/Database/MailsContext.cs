using Microsoft.EntityFrameworkCore;
using Sendora.Core.Misc;
using Sendora.Postly.Domain.Entities;

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
        // A user has one address and address has one user
        modelBuilder.Entity<UserEntity>()
            .HasOne(e => e.Address);
        
        // An email has one sender but one sender has many emails
        modelBuilder.Entity<MailEntity>()
            .HasOne(e => e.Sender)
            .WithMany()
            .HasForeignKey(e => e.SenderId)
            .HasPrincipalKey(e => e.Id);
        
        // An email has many recipients but a recipient has one email
        modelBuilder.Entity<MailEntity>()
            .HasMany(e => e.Recipients)
            .WithOne(e => e.Mail)
            .HasForeignKey(e => e.MailId)
            .HasPrincipalKey(e => e.Id);
        
        // A recipient has one mail but mails have many recipients
        modelBuilder.Entity<RecipientEntity>()
            .HasOne(e => e.Mail)
            .WithMany()
            .HasForeignKey(e => e.MailId)
            .HasPrincipalKey(e => e.Id);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}