using Ecommerce.Core.Interfaces;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Core.Services;

public class CartService : ICartService
{
    private readonly EcommerceDbContext _context;

    public CartService(EcommerceDbContext context)
    {
        _context = context;
    }

    public async Task<Cart> GetActiveCartAsync(int userId)
    {
        return await _context.Carts
            .Include(c => c.CartItems)
            .ThenInclude(ci => ci.Product)
            .FirstOrDefaultAsync(c => c.UserId == userId && c.IsActive);
    }

    public async Task AddToCartAsync(int userId, int productId, int quantity)
    {
        var cart = await GetActiveCartAsync(userId) ?? new Cart { UserId = userId };
        var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);

        if (cartItem == null)
        {
            cartItem = new CartItem { ProductId = productId, Quantity = quantity };
            cart.CartItems.Add(cartItem);
        }
        else
        {
            cartItem.Quantity += quantity; 
        }

        if (cart.Id == 0)
            _context.Carts.Add(cart);

        await _context.SaveChangesAsync();
    }

    public async Task UpdateCartAsync(int userId, int productId, int quantity)
    {
        var cart = await GetActiveCartAsync(userId);
        var cartItem = cart?.CartItems.FirstOrDefault(ci => ci.ProductId == productId);

        if (cartItem != null)
        {
            cartItem.Quantity = quantity;
            await _context.SaveChangesAsync();
        }
    }

    public async Task RemoveFromCartAsync(int userId, int productId)
    {
        var cart = await GetActiveCartAsync(userId);
        var cartItem = cart?.CartItems.FirstOrDefault(ci => ci.ProductId == productId);

        if (cartItem != null)
        {
            cart.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
        }
    }
}