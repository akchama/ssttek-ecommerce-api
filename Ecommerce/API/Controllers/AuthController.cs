using Ecommerce.API.DTOs;
using Ecommerce.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IJwtService _jwtService;

    public AuthController(IUserService userService, IJwtService jwtService)
    {
        _userService = userService;
        _jwtService = jwtService;
    }

    [HttpPost("login")]
    public IActionResult Login(LoginDto dto)
    {
        var user = _userService.GetUserByEmail(dto.Email);

        if (user == null)
            return BadRequest(new { message = "Email or password is incorrect" });

        var isPasswordValid = _userService.VerifyPassword(dto.Password, user.PasswordHash);

        if (!isPasswordValid)
            return BadRequest(new { message = "Email or password is incorrect" });

        var token = _jwtService.GenerateToken(user);

        return Ok(new { token });
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterDto dto)
    {
        // Ensure no fields are left empty
        if (string.IsNullOrWhiteSpace(dto.Email) || 
            string.IsNullOrWhiteSpace(dto.Password) || 
            string.IsNullOrWhiteSpace(dto.PasswordConfirm) || 
            string.IsNullOrWhiteSpace(dto.FirstName) || 
            string.IsNullOrWhiteSpace(dto.LastName) || 
            string.IsNullOrWhiteSpace(dto.City))
        {
            return BadRequest(new { message = "All fields are required." });
        }

        // Email format validation (basic check)
        if (!dto.Email.Contains("@"))
        {
            return BadRequest(new { message = "Invalid email format." });
        }

        // Check if email is already registered
        var existingUser = _userService.GetUserByEmail(dto.Email);
        if (existingUser != null)
        {
            return BadRequest(new { message = "Email is already registered." });
        }

        // Password requirements check
        if (dto.Password.Length < 10 || 
            !dto.Password.Any(char.IsUpper) || 
            !dto.Password.Any(char.IsDigit))
        {
            return BadRequest(new { message = "Password must be at least 10 characters long, contain at least one uppercase letter, and one digit." });
        }

        // Password confirmation match
        if (dto.Password != dto.PasswordConfirm)
        {
            return BadRequest(new { message = "Password and confirmation do not match." });
        }

        var result = _userService.RegisterUser(dto);

        if (!result.IsSuccess)
        {
            return BadRequest(new { message = result.ErrorMessage });
        }

        return Created("", new { message = "User registered successfully" });
    }
}