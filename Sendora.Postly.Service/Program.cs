// See https://aka.ms/new-console-template for more information

using Sendora.Domain.Objects;
using MailKit.Net.Imap;
using MimeKit;
using Sendora.Core.Database;
using Sendora.Core.Misc;
using Sendora.Core.Models;
using Sendora.Service;

var db = new MailsContext();
await DatabaseUtils.CreateAndMigrate(db);

var cfg = new MailHostConfig("mail.noideaindustry.com", 993);

var client = new ImapClient();
client.Connect(cfg.Host, cfg.Port, cfg.UseSsl);
client.Authenticate("contact@noideaindustry.com", "qeqHav-9jarwe-nevqyg");

var mailBoxes = await MailboxFinder.Resolve(client);
foreach (var mailbox in mailBoxes)
{
    mailbox.OnMessageLoaded += (_, args) =>
    {
        var message = args.Message;
        var hash = HashUtils.HashString($"{message.Subject}|{message.Date}");
        if (db.Mails.SingleOrDefault(x => x.Id == hash) != null) return;
        
        db.Mails.Add(new MailEntity {
            Id = hash,
            Folder = args.Folder,
            DeliveredAt = message.Date.UtcDateTime,
            Flags = args.Flags
        });
        db.SaveChanges();

        using var stream = File.Create(Path.Join(StorageUtils.MailsFolder, hash));
        message.WriteToAsync(FormatOptions.Default, stream);
    };
    
    await mailbox.Load();
}
Console.WriteLine();


