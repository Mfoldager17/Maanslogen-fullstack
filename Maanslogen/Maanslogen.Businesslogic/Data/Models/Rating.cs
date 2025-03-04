using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Maanslogen.Businesslogic.Models;

public class Rating
{
    [Key] public Guid Id { get; set; } = Guid.NewGuid();

    [Required] public int RatingValue { get; set; }

    [ForeignKey("Product")] public Guid ProductId { get; set; }

    public Product Product { get; set; } = null!;

    public Guid UserId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}