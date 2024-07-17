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
                new Category { CategoryId = Guid.NewGuid(), CategoryName = "Classic" },
                new Category { CategoryId = Guid.NewGuid(), CategoryName = "Tropical" },
                new Category { CategoryId = Guid.NewGuid(), CategoryName = "Modern" }
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
