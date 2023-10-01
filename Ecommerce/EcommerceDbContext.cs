using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

public class EcommerceDbContext : DbContext
{
    public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }

    public void SeedData()
    {
        // User seeding
        if (!Users.Any())
        {
            var mockPassword = "a72b84zx";
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(mockPassword);

            Users.Add(new User
            {
                Email = "akcamdev@gmail.com",
                PasswordHash = hashedPassword,
                FirstName = "Abdullah",
                LastName = "Akcam",
                City = "Istanbul"
            });

            SaveChanges(); // Save the user data
        }

        // Categories seeding
        if (!Categories.Any())
        {
            var electronics = new Category
            {
                Name = "Electronics"
            };
            Categories.Add(electronics);

            var clothing = new Category
            {
                Name = "Clothing"
            };
            Categories.Add(clothing);

            SaveChanges(); // Persist the root categories to get their generated Ids
        }

        if (!Categories.Any(c => c.Name == "Smartphones"))
        {
            var smartphones = new Category
            {
                Name = "Smartphones",
                ParentCategoryId = Categories.FirstOrDefault(c => c.Name == "Electronics").Id
            };
            Categories.Add(smartphones);
        }

        if (!Categories.Any(c => c.Name == "Laptops"))
        {
            var laptops = new Category
            {
                Name = "Laptops",
                ParentCategoryId = Categories.FirstOrDefault(c => c.Name == "Electronics").Id
            };
            Categories.Add(laptops);
        }

        if (!Categories.Any(c => c.Name == "Men's Clothing"))
        {
            var mensClothing = new Category
            {
                Name = "Men's Clothing",
                ParentCategoryId = Categories.FirstOrDefault(c => c.Name == "Clothing").Id
            };
            Categories.Add(mensClothing);
        }

        if (!Categories.Any(c => c.Name == "Women's Clothing"))
        {
            var womensClothing = new Category
            {
                Name = "Women's Clothing",
                ParentCategoryId = Categories.FirstOrDefault(c => c.Name == "Clothing").Id
            };
            Categories.Add(womensClothing);
        }

        SaveChanges(); // Save changes for categories
        
        if (!Products.Any())
        {
            var smartphonesCategory = Categories.FirstOrDefault(c => c.Name == "Smartphones");
            var mensClothingCategory = Categories.FirstOrDefault(c => c.Name == "Men's Clothing");

            var smartphoneProduct = new Product
            {
                Name = "Latest 5G Smartphone",
                Description = "Latest model with high-resolution camera",
                Price = 499.99M,
                Stock = 10,
                CategoryId = smartphonesCategory.Id
            };

            var jeansProduct = new Product
            {
                Name = "Blue Denim Jeans",
                Description = "Comfort fit blue jeans",
                Price = 59.99M,
                Stock = 25,
                CategoryId = mensClothingCategory.Id
            };

            Products.AddRange(smartphoneProduct, jeansProduct);
        }

        SaveChanges(); // Save changes for products
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>()
            .HasMany(c => c.SubCategories)
            .WithOne(c => c.ParentCategory)
            .HasForeignKey(c => c.ParentCategoryId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false); // Set explicitly

        // Ensure Email is unique
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        // Setup User and Cart one-to-one relationship
        modelBuilder.Entity<User>()
            .HasOne(u => u.ActiveCart)
            .WithOne(c => c.User)
            .HasForeignKey<Cart>(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Setup Cart and CartItem one-to-many relationship
        modelBuilder.Entity<Cart>()
            .HasMany(c => c.CartItems)
            .WithOne(ci => ci.Cart)
            .HasForeignKey(ci => ci.CartId);

        // Setup Product and CartItem one-to-many relationship
        modelBuilder.Entity<Product>()
            .HasMany(p => p.CartItems)
            .WithOne(ci => ci.Product)
            .HasForeignKey(ci => ci.ProductId);
    }
}