using System.ComponentModel.DataAnnotations;

namespace NovaVida.Models;

public class ProductReviews
{
    [Key]
    public int IDProductReviews { get; set; }
    public int IDProduct { get; set; }
    public string? AuthorName { get; set; }
    public string? Title { get; set; }
    public DateTime? register { get; set; }
    public string? review { get; set; }
}