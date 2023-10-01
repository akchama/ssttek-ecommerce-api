using Ecommerce.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    // Inject the ICategoryService via the constructor
    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
        // Fetch the hierarchical list of categories from the service
        var categories = await _categoryService.GetHierarchicalCategoriesAsync();

        // Check if there are any categories
        if (categories == null || categories.Count == 0)
        {
            return NotFound("No categories found.");
        }

        return Ok(categories);
    }
}

// The CategoryDto class will represent the data transfer object for categories
// It's useful when you don't want to send the entire database entity to the client
public class CategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<CategoryDto> SubCategories { get; set; }
}