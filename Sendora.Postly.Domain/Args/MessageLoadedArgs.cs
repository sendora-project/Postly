using MailKit;
using MimeKit;

namespace Sendora.Postly.Domain.Args;

public class MessageLoadedArgs : EventArgs
{
    public required string Folder { get; init; }
    public required IMimeMessage Message { get; init; }
    public required MessageFlags? Flags{ get; init; }
}