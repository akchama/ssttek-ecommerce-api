using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ecommerce.Core.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    // Inject the ICategoryService via the constructor
    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories()
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