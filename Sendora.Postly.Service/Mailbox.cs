using System.Collections;
using System.Collections.Concurrent;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MimeKit;
using Sendora.Postly.Domain;
using Sendora.Postly.Domain.Args;

namespace Sendora.Service;

public interface IMailBox
{

}

public abstract class AbstractMailBox : IMailBox
{

}

public abstract class AbstractImapMailBox(IImapClient client, IMailFolder folder) : AbstractMailBox
{
    protected IImapClient Client { get; init; } = client;
    protected IMailFolder Folder { get; init; } = folder;
    
    public EventHandler<MessageLoadedArgs>? OnMessageLoaded;

    public async Task Load()
    {
        // Open inbox for readonly
        // TODO: Change
        await folder.OpenAsync(FolderAccess.ReadOnly);
        
        // Collection email ids from latest to oldest
        var uids = await Folder.SortAsync(SearchQuery.New, [OrderBy.Arrival]);
        
        // Collection email flags based on email ids
        var summaries = await Folder.FetchAsync(uids, MessageSummaryItems.Flags | MessageSummaryItems.UniqueId);
        
        foreach (var uid in uids)
        {
            var args = new MessageLoadedArgs
            {
                Folder = folder.FullName,
                Message = await Folder.GetMessageAsync(uid),
                Flags = summaries.Single(x => x.UniqueId == uid).Flags
            };
            
            OnMessageLoaded?.Invoke(this, args);
        }
    }
}

public class ImapMailBox(IImapClient client, IMailFolder folder) : AbstractImapMailBox(client, folder)
{

}

public static class MailboxFinder
{
    public static async Task<IList<ImapMailBox>> Resolve(IImapClient client)
    {
        var mailBoxes = new ConcurrentDictionary<string, ImapMailBox>();
        
        var tasks = client.PersonalNamespaces.Select(async folderNamespace =>
        {
            var folders = await client.GetFoldersAsync(folderNamespace);

            if (folders is not { Count: > 0 })
                return;
        
            foreach (var folder in folders)
                mailBoxes[folder.FullName] = new ImapMailBox(client, folder);
            
        }).ToList();
        
        await Task.WhenAll(tasks);
        return mailBoxes.Values.ToList();
    }
}