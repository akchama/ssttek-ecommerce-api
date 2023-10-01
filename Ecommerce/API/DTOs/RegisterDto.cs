namespace Ecommerce.API.DTOs;

public class RegisterDto : LoginDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string City { get; set; }
    public string? PasswordConfirm { get; set; }
}