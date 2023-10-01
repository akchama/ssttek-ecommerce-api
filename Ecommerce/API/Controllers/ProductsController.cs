using Ecommerce.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    // List Products by Category
    [HttpGet("api/categories/{categoryId}/products")]
    public IActionResult GetProductsByCategory(int categoryId)
    {
        var products = _productService.GetProductsByCategory(categoryId);
        if (products == null || !products.Any())
            return NotFound("No products found for the given category.");

        return Ok(new { products });
    }

    // Get Product Details
    [HttpGet("api/products/{id}")]
    public IActionResult GetProductDetails(int id)
    {
        var product = _productService.GetProductDetails(id);
        if (product == null)
            return NotFound("Product not found.");

        return Ok(new { product });
    }
}