namespace Ecommerce.Models;

public class Cart
{
    public int Id { get; set; }

    public int? UserId { get; set; }  // Make this nullable
    public User User { get; set; }  // Reference to the user it belongs to

    public bool IsGuest { get; set; } = false; // New field to indicate if it's a guest cart

    public ICollection<CartItem> CartItems { get; set; }  // Items in the cart
    public bool IsActive { get; set; } = true;
}
