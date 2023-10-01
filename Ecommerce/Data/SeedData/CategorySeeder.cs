using Ecommerce.Models;

namespace Ecommerce.Data.SeedData;

public class CategorySeeder
{
    private readonly EcommerceDbContext _context;

    public CategorySeeder(EcommerceDbContext context)
    {
        _context = context;
    }

    public void Seed()
    {
        if (!_context.Categories.Any())
        {
            var electronics = new Category
            {
                Name = "Electronics"
            };
            _context.Categories.Add(electronics);

            var clothing = new Category
            {
                Name = "Clothing"
            };
            _context.Categories.Add(clothing);

            _context.SaveChanges(); // Persist the root categories to get their generated Ids
        }

        if (!_context.Categories.Any(c => c.Name == "Smartphones"))
        {
            var smartphones = new Category
            {
                Name = "Smartphones",
                ParentCategoryId = _context.Categories.FirstOrDefault(c => c.Name == "Electronics").Id
            };
            _context.Categories.Add(smartphones);
        }

        if (!_context.Categories.Any(c => c.Name == "Laptops"))
        {
            var laptops = new Category
            {
                Name = "Laptops",
                ParentCategoryId = _context.Categories.FirstOrDefault(c => c.Name == "Electronics").Id
            };
            _context.Categories.Add(laptops);
        }

        if (!_context.Categories.Any(c => c.Name == "Men's Clothing"))
        {
            var mensClothing = new Category
            {
                Name = "Men's Clothing",
                ParentCategoryId = _context.Categories.FirstOrDefault(c => c.Name == "Clothing").Id
            };
            _context.Categories.Add(mensClothing);
        }

        if (!_context.Categories.Any(c => c.Name == "Women's Clothing"))
        {
            var womensClothing = new Category
            {
                Name = "Women's Clothing",
                ParentCategoryId = _context.Categories.FirstOrDefault(c => c.Name == "Clothing").Id
            };
            _context.Categories.Add(womensClothing);
        }

        _context.SaveChanges(); // Save changes for categories
    }
}