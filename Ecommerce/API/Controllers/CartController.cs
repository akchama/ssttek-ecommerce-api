using System.Security.Claims;
using Ecommerce.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    private int UserId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)); 

    [HttpPost("add")]
    public async Task<IActionResult> AddToCart(AddToCartDto dto)
    {
        await _cartService.AddToCartAsync(UserId, dto.ProductId, dto.Quantity);
        return Ok(new { message = "Product added to cart" });
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateCart(UpdateCartDto dto)
    {
        await _cartService.UpdateCartAsync(UserId, dto.ProductId, dto.Quantity);
        return Ok(new { message = "Cart updated" });
    }

    [HttpDelete("remove/{productId}")]
    public async Task<IActionResult> RemoveFromCart(int productId)
    {
        await _cartService.RemoveFromCartAsync(UserId, productId);
        return Ok(new { message = "Product removed from cart" });
    }

    [HttpGet]
    public async Task<IActionResult> GetActiveCart()
    {
        var cart = await _cartService.GetActiveCartAsync(UserId);
        return Ok(new { cart });
    }
}

public class AddToCartDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}

public class UpdateCartDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}