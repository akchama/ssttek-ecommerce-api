using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models;

public class Product
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; }

    [MaxLength(500)]
    public string Description { get; set; }

    [Required]
    public decimal Price { get; set; }

    public int Stock { get; set; }

    public bool IsAvailable { get; set; } = true;

    public int CategoryId { get; set; }
    public Category Category { get; set; }  // Reference to the category it belongs to

    public ICollection<CartItem> CartItems { get; set; } // Collection of cart items related to this product
}

