using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sendora.Postly.Domain.Entities;

[Table("Users")]
public sealed class UserEntity
{
    [Key]
    [Required]
    [Column("id")]
    [StringLength(36)]
    public required string Id { get; init; }
    
    [Required]
    [Column("username")]
    [StringLength(50)]
    public required string Username { get; init; }
    
    [Required]
    [Column("password")]
    public required string Password { get; init; }

    [Column("address")] 
    public AddressEntity Address { get; init; } = null!;
}