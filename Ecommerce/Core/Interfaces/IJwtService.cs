using Ecommerce.Models;

namespace Ecommerce.Core.Interfaces;

public interface IJwtService
{
    string GenerateToken(User user);
}