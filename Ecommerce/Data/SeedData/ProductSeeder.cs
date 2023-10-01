using Ecommerce.Models;

namespace Ecommerce.Data.SeedData;

public class ProductSeeder
{
    private readonly EcommerceDbContext _context;

    public ProductSeeder(EcommerceDbContext context)
    {
        _context = context;
    }

    public void Seed()
    {
        if (!_context.Products.Any())
        {
            var smartphonesCategory = _context.Categories.FirstOrDefault(c => c.Name == "Smartphones");
            var mensClothingCategory = _context.Categories.FirstOrDefault(c => c.Name == "Men's Clothing");

            var smartphoneProduct = new Product
            {
                Name = "Latest 5G Smartphone",
                Description = "Latest model with high-resolution camera",
                Price = 499.99M,
                Stock = 10,
                CategoryId = smartphonesCategory.Id
            };

            var jeansProduct = new Product
            {
                Name = "Blue Denim Jeans",
                Description = "Comfort fit blue jeans",
                Price = 59.99M,
                Stock = 25,
                CategoryId = mensClothingCategory.Id
            };

            _context.Products.AddRange(smartphoneProduct, jeansProduct);

            _context.SaveChanges(); // Save changes for products
        }
    }
}