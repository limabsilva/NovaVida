using System.ComponentModel.DataAnnotations;


namespace NovaVida.Models;

public class Products
{
    [Key]
    public int IdProduct { get; set; }
    public string? NameProduct { get; set; }
    public decimal PriceProduct { get; set; }
    public string? URLProduct { get; set; }
}