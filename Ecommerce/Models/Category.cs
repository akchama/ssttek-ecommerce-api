using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int? ParentCategoryId { get; set; } // Nullable
    public Category ParentCategory { get; set; }
    public ICollection<Category> SubCategories { get; set; } // List of sub-categories
}