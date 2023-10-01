using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models;

public class User
{
    public int Id { get; set; }

    [Required, EmailAddress]
    public string Email { get; set; }

    [Required]
    public string PasswordHash { get; set; } // Passwords should be hashed, never store plain text

    [Required, MaxLength(50)]
    public string FirstName { get; set; }

    [Required, MaxLength(50)]
    public string LastName { get; set; }

    [Required, MaxLength(100)]
    public string City { get; set; }

    public bool IsActive { get; set; } = true; // To check if a user is active

    public Cart ActiveCart { get; set; }  // One active cart per user
}