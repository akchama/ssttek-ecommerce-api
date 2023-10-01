using Ecommerce.Models;

namespace Ecommerce.Core.Interfaces;

public interface IProductService
{
    IEnumerable<Product> GetProductsByCategory(int categoryId);
    Product GetProductDetails(int id);
}