using Ecommerce.API.DTOs;
using Ecommerce.Core.Common;
using Ecommerce.Core.Interfaces;
using Ecommerce.Models;
using BC = BCrypt.Net.BCrypt;

namespace Ecommerce.Core.Services;

public class UserService : IUserService
{
    private readonly EcommerceDbContext _dbContext;
    
    public UserService(EcommerceDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public User? GetUserByEmail(string email)
    {
        return _dbContext.Users.FirstOrDefault(u => u.Email == email);
    }

    public bool VerifyPassword(string inputPassword, string storedHash)
    {
        return BC.Verify(inputPassword, storedHash);
    }

    public bool IsEmailRegistered(string email)
    {
        return _dbContext.Users.Any(u => u.Email == email);
    }

    public string HashPassword(string password)
    {
        return BC.HashPassword(password);
    }

    public void AddUser(User user)
    {
        _dbContext.Users.Add(user);  // Add the user to the DbContext's Users DbSet
        _dbContext.SaveChanges();   // Commit changes to the database
    }
    
    public RegistrationResult RegisterUser(RegisterDto dto)
    {
        if (IsEmailRegistered(dto.Email))
            return RegistrationResult.Failure("Email is already registered");

        var hashedPassword = HashPassword(dto.Password);
        var user = new User
        {
            Email = dto.Email,
            PasswordHash = hashedPassword,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            City = dto.City
        };

        AddUser(user);
        return RegistrationResult.Success();
    }
}