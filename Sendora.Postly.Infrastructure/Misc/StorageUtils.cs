namespace Sendora.Core.Misc;

public static class StorageUtils
{
    public static string AppFolder { get; }
    
    public static string CacheFolder { get; }
    public static string MailsFolder { get; }

    static StorageUtils()
    {
        const Environment.SpecialFolder folder = Environment.SpecialFolder.LocalApplicationData;
        AppFolder = Path.Join(Environment.GetFolderPath(folder), "Sendora");
        CacheFolder = Path.Join(AppFolder, "Cache");
        MailsFolder = Path.Join(AppFolder, "Storage");

        if (!Directory.Exists(AppFolder))
            Directory.CreateDirectory(AppFolder);
        
        if (!Directory.Exists(CacheFolder))
            Directory.CreateDirectory(CacheFolder);
        
        if (!Directory.Exists(MailsFolder))
            Directory.CreateDirectory(MailsFolder);
    }
}