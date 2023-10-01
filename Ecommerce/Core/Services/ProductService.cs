using System.Collections.Generic;
using System.Linq;
using Ecommerce.Core.Interfaces;
using Ecommerce.Models;

public class ProductService : IProductService
{
    private readonly EcommerceDbContext _context;

    public ProductService(EcommerceDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Product> GetProductsByCategory(int categoryId)
    {
        return _context.Products.Where(p => p.CategoryId == categoryId).ToList();
    }

    public Product GetProductDetails(int id)
    {
        return _context.Products.FirstOrDefault(p => p.Id == id);
    }
}