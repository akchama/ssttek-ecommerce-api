using Ecommerce.Core.Interfaces;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Ecommerce.Core.Services;

public class CartService : ICartService
{
    private readonly EcommerceDbContext _context;

    public CartService(EcommerceDbContext context)
    {
        _context = context;
    }

    public async Task<Cart> GetActiveCartAsync(int? userId = null)
    {
        // If userId is provided, get user cart. Otherwise, get guest cart.
        return await _context.Carts
            .Include(c => c.CartItems)
            .ThenInclude(ci => ci.Product)
            .FirstOrDefaultAsync(c => (!userId.HasValue && c.IsGuest) || 
                                      (userId.HasValue && c.UserId == userId.Value && c.IsActive));
    }

    public async Task AddToCartAsync(int? userId, int productId, int quantity)
    {
        var cart = await GetActiveCartAsync(userId);

        if (cart == null)
        {
            cart = new Cart 
            { 
                UserId = userId, 
                IsGuest = !userId.HasValue 
            };
        
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync(); // Save immediately to generate ID
        }

        var cartItem = cart.CartItems?.FirstOrDefault(ci => ci.ProductId == productId);

        if (cartItem == null)
        {
            cartItem = new CartItem { ProductId = productId, Quantity = quantity };
            if (cart.CartItems == null)
            {
                cart.CartItems = new List<CartItem>();
            }
            cart.CartItems.Add(cartItem);
        }
        else
        {
            cartItem.Quantity = Math.Min(cartItem.Product.Stock, cartItem.Quantity + quantity);
        }

        await _context.SaveChangesAsync();
    }


    public async Task UpdateCartAsync(int? userId, int productId, int quantity)
    {
        var cart = await GetActiveCartAsync(userId);
        var cartItem = cart?.CartItems.FirstOrDefault(ci => ci.ProductId == productId);

        if (cartItem != null)
        {
            cartItem.Quantity = Math.Clamp(quantity, 1, cartItem.Product.Stock);
            await _context.SaveChangesAsync();
        }
    }

    public async Task RemoveFromCartAsync(int? userId, int productId)
    {
        var cart = await GetActiveCartAsync(userId);
        var cartItem = cart?.CartItems.FirstOrDefault(ci => ci.ProductId == productId);

        if (cartItem != null)
        {
            cart.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
        }
    }

    public async Task MergeGuestCartToUserCart(int userId)
    {
        var userCart = await GetActiveCartAsync(userId);
        var guestCart = await GetActiveCartAsync();

        if (userCart != null && guestCart != null)
        {
            foreach (var guestItem in guestCart.CartItems)
            {
                var existingItem = userCart.CartItems.FirstOrDefault(ci => ci.ProductId == guestItem.ProductId);

                if (existingItem != null)
                {
                    existingItem.Quantity += guestItem.Quantity;
                }
                else
                {
                    userCart.CartItems.Add(guestItem);
                }
            }

            _context.Carts.Remove(guestCart);
            await _context.SaveChangesAsync();
        }
    }
}
