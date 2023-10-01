using Ecommerce.API.DTOs;
using Ecommerce.Core.Interfaces;
using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;

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
        var result = _userService.RegisterUser(dto);

        if (!result.IsSuccess)
            return BadRequest(new { message = result.ErrorMessage });

        return Created("", new { message = "User registered successfully" });
    }

}