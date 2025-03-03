using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MimeKit;
using Sendora.Postly.Domain.Enums;

namespace Sendora.Postly.Domain.Entities;

[Table("Recipients")]
public class RecipientEntity
{
    [Key]
    [Column("id")]
    [StringLength(50)]
    public required string Id { get; init; }
    
    [Column("mail_id")]
    [StringLength(36)]
    public required string MailId { get; init; }
    
    [Required]
    [Column("recipient_category")]
    public required RecipientCategory Category { get; init; }

    [Column("mail")]
    public virtual MailEntity Mail { get; init; } = null!;
}