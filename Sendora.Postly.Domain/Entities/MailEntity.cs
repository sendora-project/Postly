using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MailKit;

namespace Sendora.Postly.Domain.Entities;

[Table("Mails")]
public class MailEntity
{
    [Key]
    [Column("id")]
    [StringLength(36)]
    public required string Id { get; init; }
    
    [Required]
    [Column("sender_id")]
    [StringLength(36)]
    public required string SenderId { get; init; }

    [Required]
    [Column("subject")]
    [StringLength(100)]
    public required string Subject { get; init; }
    
    [Required]
    [Column("delivered_at")]
    public required DateTime DeliveredAt { get; init; }

    [Required]
    [Column("flags")]
    public required MessageFlags Flags { get; init; }
    
    [Column("sender")] 
    public virtual UserEntity Sender { get; init; } = null!;
    
    [Column("recipients")] 
    public virtual required ICollection<RecipientEntity> Recipients { get; init; }
}