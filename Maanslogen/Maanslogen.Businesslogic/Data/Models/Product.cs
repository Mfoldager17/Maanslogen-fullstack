using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Maanslogen.Businesslogic.Models;

public class Product
{
    public Product(string name, LiquidType liquidType, float alcoholPercentage, Guid manufacturerId,
        int priceInHundreds)
    {
        Name = name;
        LiquidType = liquidType;
        AlcoholPercentage = alcoholPercentage;
        ManufacturerId = manufacturerId;
        PriceInHundreds = priceInHundreds;
        Ratings = new List<Rating>();
    }

    [Key] public Guid Id { get; set; } = Guid.NewGuid();

    [Required] 
    public string Name { get; set; }

    public string? Description { get; set; }
    
    public LiquidType LiquidType { get; set; }

    public float AlcoholPercentage { get; set; }

    public List<Rating>? Ratings { get; set; }

    [ForeignKey("Manufacturer")] public Guid ManufacturerId { get; set; }
    public Manufacturer? Manufacturer { get; set; }

    public int PriceInHundreds { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}

public enum LiquidType
{
    Beer,
    Wine,
    Gin,
    Vodka,
    Whiskey,
    Rum,
    Tequila,
    Cognac,
    Liqueur,
    Other
}