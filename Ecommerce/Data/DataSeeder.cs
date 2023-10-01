using Ecommerce.Data.SeedData;

namespace Ecommerce.Data;

public class DataSeeder
{
    private readonly UserSeeder _userSeeder;
    private readonly CategorySeeder _categorySeeder;
    private readonly ProductSeeder _productSeeder;

    public DataSeeder(UserSeeder userSeeder, CategorySeeder categorySeeder, ProductSeeder productSeeder)
    {
        _userSeeder = userSeeder;
        _categorySeeder = categorySeeder;
        _productSeeder = productSeeder;
    }

    public void SeedAll()
    {
        _userSeeder.Seed();
        _categorySeeder.Seed();
        _productSeeder.Seed();
    }
}