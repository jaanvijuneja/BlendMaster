using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Entities
{
    public class TestDbContext : IdentityDbContext<IdentityUser>
    {
        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options) { }

        public DbSet<CustomerOrder> CustomerOrder { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Recipe> Recipe { get; set; }
        public DbSet<Table> Table { get; set; }
        public DbSet<Bill> Bill { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<CustomerOrder>()
                .HasMany(co => co.OrderDetails)
                .WithOne(od => od.CustomerOrder)
                .HasForeignKey(od => od.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            var classicCategoryId = Guid.NewGuid();
            var tropicalCategoryId = Guid.NewGuid();
            var modernCategoryId = Guid.NewGuid();

            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = classicCategoryId, CategoryName = "Classic" },
                new Category { CategoryId = tropicalCategoryId, CategoryName = "Tropical" },
                new Category { CategoryId = modernCategoryId, CategoryName = "Modern" }
            );

            modelBuilder.Entity<Table>().HasData(
                new Table { TableId = 1, TableName = "Table1" },
                new Table { TableId = 2, TableName = "Table2" },
                new Table { TableId = 3, TableName = "Table3" },
                new Table { TableId = 4, TableName = "Table4" },
                new Table { TableId = 5, TableName = "Table5" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "Old Fashioned",
                    Price = 12.99m,
                    ProductDescription = "A timeless cocktail featuring bourbon, sugar, and bitters.",
                    CategoryId = classicCategoryId
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "Martini",
                    Price = 11.99m,
                    ProductDescription = "A sophisticated cocktail made with gin and vermouth, garnished with an olive.",
                    CategoryId = classicCategoryId
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "Mai Tai",
                    Price = 10.99m,
                    ProductDescription = "A tropical mix of rum, lime juice, and orgeat syrup.",
                    CategoryId = tropicalCategoryId
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "Pina Colada",
                    Price = 9.99m,
                    ProductDescription = "A creamy blend of rum, pineapple juice, and coconut cream.",
                    CategoryId = tropicalCategoryId
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "Espresso Martini",
                    Price = 13.49m,
                    ProductDescription = "A sophisticated mix of vodka, coffee liqueur, and espresso.",
                    CategoryId = modernCategoryId
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "Cucumber Cooler",
                    Price = 10.49m,
                    ProductDescription = "A refreshing cocktail with gin, cucumber, lime, and mint.",
                    CategoryId = modernCategoryId
                }
            );
        }
    }
}
