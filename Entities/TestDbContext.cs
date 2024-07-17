using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Entities
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options) { }

        public DbSet<CustomerOrder> CustomerOrder { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Recipe> Recipe { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Table> Table { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerOrder>()
                .HasMany(co => co.OrderDetails)
                .WithOne(od => od.CustomerOrder)
                .HasForeignKey(od => od.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Classic" },
                new Category { CategoryId = 2, CategoryName = "Tropical" },
                new Category { CategoryId = 3, CategoryName = "Modern" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, ProductName = "Mojito", Price = 12m, CategoryId = 1, ProductDescription = "A smooth and fruity red Product, perfect for any occasion.", ImageUrl = "#" },
                new Product { ProductId = 2, ProductName = "Old Fashioned", Price = 15m, CategoryId = 1, ProductDescription = "A bold red Product with notes of blackcurrant and a hint of oak.", ImageUrl = "#" },
                new Product { ProductId = 3, ProductName = "Margarita", Price = 14m, CategoryId = 1, ProductDescription = "A delicate red Product with notes of cherry and spice.", ImageUrl = "#" },

                new Product { ProductId = 8, ProductName = "Piña Colada", Price = 13m, CategoryId = 2, ProductDescription = "A popular white Product known for its rich and creamy flavor.", ImageUrl = "#" },
                new Product { ProductId = 9, ProductName = "Mai Tai", Price = 16m, CategoryId = 2, ProductDescription = "A crisp and refreshing white Product with hints of citrus.", ImageUrl = "#" },
                new Product { ProductId = 10, ProductName = "Blue Lagoon", Price = 12m, CategoryId = 2, ProductDescription = "A sweet white Product with floral aromas and a crisp finish.", ImageUrl = "#" },

                new Product { ProductId = 15, ProductName = "Espresso Martini", Price = 18m, CategoryId = 3, ProductDescription = "A classic sparkling Product with crisp acidity and fine bubbles.", ImageUrl = "#" },
                new Product { ProductId = 16, ProductName = "Aperol Spritz", Price = 12m, CategoryId = 3, ProductDescription = "A light and refreshing sparkling Product with flavors of green apple and melon.", ImageUrl = "#" },
                new Product { ProductId = 17, ProductName = "Gin Basil Smash", Price = 14m, CategoryId = 3, ProductDescription = "A Spanish sparkling Product with zesty citrus flavors and fine bubbles.", ImageUrl = "#" }
            );

            modelBuilder.Entity<Table>().HasData(
                new Table { TableId = 1, TableName = "Table1" },
                new Table { TableId = 2, TableName = "Table2" },
                new Table { TableId = 3, TableName = "Table3" },
                new Table { TableId = 4, TableName = "Table4" },
                new Table { TableId = 5, TableName = "Table5" }
            );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = Guid.NewGuid(),
                    Name = "Bartender",
                    Email = "email@example.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("123")
                }
            );
        }
    }
}
