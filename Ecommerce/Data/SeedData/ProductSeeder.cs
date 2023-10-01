using Ecommerce.Models;
using System.Linq;

namespace Ecommerce.Data.SeedData
{
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
                var electronicsCategory = _context.Categories.FirstOrDefault(c => c.Name == "Electronics");
                var laptopsCategory = _context.Categories.FirstOrDefault(c => c.Name == "Laptops");
                var smartphonesCategory = _context.Categories.FirstOrDefault(c => c.Name == "Smartphones");
                var mensClothingCategory = _context.Categories.FirstOrDefault(c => c.Name == "Men's Clothing");
                var womensClothingCategory = _context.Categories.FirstOrDefault(c => c.Name == "Women's Clothing");
                var clothingCategory = _context.Categories.FirstOrDefault(c => c.Name == "Clothing");

                var laptopProduct = new Product
                {
                    Name = "Top-end Gaming Laptop",
                    Description = "High performance gaming laptop with RTX graphics",
                    Price = 1299.99M,
                    Stock = 15,
                    CategoryId = laptopsCategory.Id
                };

                var smartphoneProduct = new Product
                {
                    Name = "Latest 5G Smartphone",
                    Description = "Latest model with high-resolution camera",
                    Price = 499.99M,
                    Stock = 10,
                    CategoryId = smartphonesCategory.Id
                };

                var tShirtProduct = new Product
                {
                    Name = "Designer T-shirt",
                    Description = "Soft cotton t-shirt with unique design",
                    Price = 29.99M,
                    Stock = 40,
                    CategoryId = mensClothingCategory.Id
                };

                var dressProduct = new Product
                {
                    Name = "Summer Dress",
                    Description = "Light summer dress with floral patterns",
                    Price = 59.99M,
                    Stock = 25,
                    CategoryId = womensClothingCategory.Id
                };

                var jeansProduct = new Product
                {
                    Name = "Blue Denim Jeans",
                    Description = "Comfort fit blue jeans",
                    Price = 59.99M,
                    Stock = 25,
                    CategoryId = mensClothingCategory.Id
                };

                var smartwatchProduct = new Product
                {
                    Name = "Smartwatch with Heart Rate Monitor",
                    Description = "Smartwatch with various fitness tracking features",
                    Price = 199.99M,
                    Stock = 20,
                    CategoryId = electronicsCategory.Id
                };

                _context.Products.AddRange(laptopProduct, smartphoneProduct, tShirtProduct, dressProduct, jeansProduct, smartwatchProduct);

                _context.SaveChanges(); // Save changes for products
            }
        }
    }
}
