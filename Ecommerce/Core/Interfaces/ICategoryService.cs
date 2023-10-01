namespace Ecommerce.Core.Interfaces;

public interface ICategoryService
{
    Task<List<CategoryDto>> GetHierarchicalCategoriesAsync();
}