using Microsoft.EntityFrameworkCore;

namespace Sendora.Core.Misc;

public static class DatabaseUtils
{
    public static async Task CreateAndMigrate(DbContext db)
    {
        await db.Database.EnsureCreatedAsync();
        await db.Database.MigrateAsync();
    }
}