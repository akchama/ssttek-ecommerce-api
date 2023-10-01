using Ecommerce.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("{categoryId}")]
    public IActionResult GetProductsByCategory(int categoryId)
    {
        var products = _productService.GetProductsByCategory(categoryId);
        if (products == null || !products.Any())
            return NotFound("No products found for the given category.");

        return Ok(new { products });
    }

    [HttpGet("details/{id}")]
    public IActionResult GetProductDetails(int id)
    {
        var product = _productService.GetProductDetails(id);
        if (product == null)
            return NotFound("Product not found.");

        return Ok(new { product });
    }
}