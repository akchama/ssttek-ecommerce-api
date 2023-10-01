using Ecommerce.Models;

namespace Ecommerce.Core.Interfaces;

public interface ICartService
{
    Task<Cart> GetActiveCartAsync(int? userId);
    Task AddToCartAsync(int? userId, int productId, int quantity);
    Task UpdateCartAsync(int userId, int productId, int quantity);
    Task RemoveFromCartAsync(int userId, int productId);
}