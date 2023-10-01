using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    // Inject necessary services, e.g. ICategoryService

    [HttpGet]
    public IActionResult GetCategories()
    {
        // Implement fetching logic here

        return Ok(new { categories = "List of categories" }); // Sample return
    }
}