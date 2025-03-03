// See https://aka.ms/new-console-template for more information

using MailKit;
using MailKit.Net.Imap;
using MimeKit;
using Sendora.Core.Database;
using Sendora.Core.Misc;
using Sendora.Postly.Domain.Entities;
using Sendora.Postly.Domain.Enums;
using Sendora.Postly.Domain.Models;
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
    mailbox.OnMessageLoaded += (_, args) => {
        var message = args.Message;
        
        var subject = message.Subject;
        var date = message.Date.UtcDateTime;
        
        var mailId = HashUtils.HashMail(subject, date);
        if (db.Mails.SingleOrDefault(x => x.Id == mailId) != null) 
            return;
        
        var sender = MailUtils.ExtractAddress(message.From[0].ToString())!;
        var recipients = new List<RecipientEntity> {
            new()
            {
                Id = sender,
                MailId = mailId,
                Category = RecipientCategory.From
            }
        };

        foreach (var address in message.Cc)
        {
            recipients.Add(new RecipientEntity
            {
                Id = address.ToString(),
                MailId = mailId,
                Category = RecipientCategory.Cc
            });
        }

        foreach (var address in message.Bcc)
        {
            recipients.Add(new RecipientEntity
            {
                Id = address.ToString(),
                MailId = mailId,
                Category = RecipientCategory.Bcc
            });
        }
        
        var mail = new MailEntity
        {
            Id = mailId,
            SenderId = sender,
            Subject = subject,
            DeliveredAt = date,
            Flags = args.Flags ?? MessageFlags.None,
            Recipients = recipients
        };
        
        db.Mails.Add(mail);
        db.SaveChanges();

        using var stream = File.Create(Path.Join(StorageUtils.MailsFolder, mailId));
        message.WriteToAsync(FormatOptions.Default, stream);
    };

    await mailbox.Load();
}
Console.WriteLine();


