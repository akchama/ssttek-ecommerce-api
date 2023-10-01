using Ecommerce.Models;

namespace Ecommerce.Data.SeedData;

public class UserSeeder
{
    private readonly EcommerceDbContext _context;

    public UserSeeder(EcommerceDbContext context)
    {
        _context = context;
    }

    public void Seed()
    {
        if (!_context.Users.Any())
        {
            var mockPassword = "Abc123456789!";
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(mockPassword);

            _context.Users.Add(new User
            {
                Email = "akcamdev@gmail.com",
                PasswordHash = hashedPassword,
                FirstName = "Abdullah",
                LastName = "Akcam",
                City = "Istanbul"
            });

            _context.SaveChanges(); // Save the user data
        }
    }
}