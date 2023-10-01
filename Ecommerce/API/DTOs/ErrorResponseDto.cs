namespace Ecommerce.API.DTOs;

public class ErrorResponseDto
{
    public string Message { get; set; }
    public IEnumerable<string> Details { get; set; }
}