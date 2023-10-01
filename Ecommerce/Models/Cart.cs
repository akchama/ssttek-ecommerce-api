namespace Ecommerce.Models;

public class Cart
{
    public int Id { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }  // Reference to the user it belongs to

    public ICollection<CartItem> CartItems { get; set; }  // Items in the cart
}
