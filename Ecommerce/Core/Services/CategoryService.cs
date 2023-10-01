using Ecommerce.API.Controllers;
using Ecommerce.Core.Interfaces;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Core.Services;

public class CategoryService : ICategoryService
{
    private readonly EcommerceDbContext _context;

    public CategoryService(EcommerceDbContext context)
    {
        _context = context;
    }

    public async Task<List<CategoryDto>> GetHierarchicalCategoriesAsync()
    {
        // Fetch all categories from the database
        var categories = await _context.Categories.ToListAsync();

        // Filter out the top-level categories (those without a parent)
        var topLevelCategories = categories.Where(c => !c.ParentCategoryId.HasValue).ToList();

        // Convert to DTOs
        var dtoList = topLevelCategories.Select(c => ToDto(c, categories)).ToList();

        return dtoList;
    }

    private CategoryDto ToDto(Category category, List<Category> allCategories)
    {
        var dto = new CategoryDto
        {
            Id = category.Id,
            Name = category.Name
        };

        // Find sub-categories
        var subCategories = allCategories.Where(c => c.ParentCategoryId == category.Id).ToList();

        if (subCategories.Count > 0)
        {
            dto.SubCategories = new List<CategoryDto>();

            foreach (var sub in subCategories)
            {
                dto.SubCategories.Add(ToDto(sub, allCategories));
            }
        }

        return dto;
    }
}