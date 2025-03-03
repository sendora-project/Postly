using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sendora.Postly.Domain.Entities;

[Table("Addresses")]
public class AddressEntity
{
    [Key]
    [Required]
    [Column("id")]
    [StringLength(36)]
    public required string Id { get; init; }
    
    [Required]
    [Column("address")]
    [StringLength(50)]
    public required string Address { get; init; }
    
    [Column("name")]
    [StringLength(50)]
    public string Name { get; init; }
    
    [Column("left")]
    [StringLength(50)]
    public string Left { get; init; }
    
    [Column("right")]
    [StringLength(50)]
    public string Right { get; init; }
}