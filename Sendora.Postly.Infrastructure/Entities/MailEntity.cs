using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MailKit;

namespace Sendora.Core.Models;

[Table("Mails")]
public class MailEntity
{
    [Key]
    [Column("id")]
    public required string Id { get; init; }

    [Required]
    [Column("folder")] 
    public required string Folder { get; init; } = null!;
    
    [Required]
    [Column("delivered_at")]
    public required DateTime DeliveredAt { get; init; }
    
    [Required]
    [Column("flags")]
    public required MessageFlags? Flags { get; init; }
}