using Ecommerce.API.DTOs;
using Ecommerce.Core.Common;
using Ecommerce.Models;

namespace Ecommerce.Core.Interfaces;

public interface IUserService
{
    User? GetUserByEmail(string email);
    bool VerifyPassword(string inputPassword, string storedHash);
    bool IsEmailRegistered(string email);
    string HashPassword(string password);
    void AddUser(User user);
    RegistrationResult RegisterUser(RegisterDto dto);
}