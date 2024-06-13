using Microsoft.EntityFrameworkCore;

namespace BlendMaster.Entities
{
    public class EnDbContext : DbContext
    {
        public EnDbContext(DbContextOptions<EnDbContext> options) : base(options)
        {
        }

        public DbSet<OrderDetail> OrderDetailList { get; set; }
        public DbSet<Order> OrderList { get; set; }
        public DbSet<Category> CategoryList { get; set; }
        public DbSet<Wine> WineList { get; set; }
        public DbSet<Table> TableList { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 主键
            modelBuilder.Entity<Wine>().HasKey(t => t.WineId);
            modelBuilder.Entity<Category>().HasKey(t => t.CategoryId);
            modelBuilder.Entity<Table>().HasKey(t => t.TableId);
            modelBuilder.Entity<OrderDetail>().HasKey(od => od.OrderDetailId);
            modelBuilder.Entity<Order>().HasKey(o => o.OrderId);

            // 外键
            modelBuilder.Entity<Order>()
                .HasOne<Table>()
                .WithMany()
                .HasForeignKey(o => o.TableID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderDetail>()
                .HasOne<Order>()
                .WithMany()
                .HasForeignKey(od => od.OrderID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderDetail>()
                .HasOne<Wine>()
                .WithMany()
                .HasForeignKey(od => od.WineID)
                .OnDelete(DeleteBehavior.Cascade);

            // 种子数据
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // 添加酒类数据
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Red Wine" },
                new Category { CategoryId = 2, CategoryName = "White Wine" },
                new Category { CategoryId = 3, CategoryName = "Sparkling Wine" },
                new Category { CategoryId = 4, CategoryName = "Dessert Wine" },
                new Category { CategoryId = 5, CategoryName = "Rose Wine" },
                new Category { CategoryId = 6, CategoryName = "Fortified Wine" },
                new Category { CategoryId = 7, CategoryName = "Organic Wine" },
                new Category { CategoryId = 8, CategoryName = "Ice Wine" }
            );

            // 添加酒品数据
            modelBuilder.Entity<Wine>().HasData(
                // Red Wine
                new Wine { WineId = 1, WineName = "Merlot", Price = 35.50m, CategoryID = 1, Introduction = "A smooth and fruity red wine, perfect for any occasion.", ImageUrl = "/images/wine1.jpg" },
                new Wine { WineId = 2, WineName = "Cabernet Sauvignon", Price = 50.00m, CategoryID = 1, Introduction = "A bold red wine with notes of blackcurrant and a hint of oak.", ImageUrl = "/images/wine2.jpg" },
                new Wine { WineId = 3, WineName = "Pinot Noir", Price = 45.00m, CategoryID = 1, Introduction = "A delicate red wine with notes of cherry and spice.", ImageUrl = "/images/wine3.jpg" },
                new Wine { WineId = 4, WineName = "Zinfandel", Price = 40.00m, CategoryID = 1, Introduction = "A robust red wine with rich flavors of blackberry and pepper.", ImageUrl = "/images/wine4.jpg" },
                new Wine { WineId = 5, WineName = "Syrah", Price = 55.00m, CategoryID = 1, Introduction = "A spicy and bold red wine with dark fruit flavors.", ImageUrl = "/images/wine5.jpg" },
                new Wine { WineId = 6, WineName = "Malbec", Price = 38.00m, CategoryID = 1, Introduction = "A full-bodied red wine with flavors of plum and black cherry.", ImageUrl = "/images/wine6.jpg" },
                new Wine { WineId = 7, WineName = "Sangiovese", Price = 42.00m, CategoryID = 1, Introduction = "A classic Italian red wine with flavors of dark cherry and oregano.", ImageUrl = "/images/wine7.jpg" },

                // White Wine
                new Wine { WineId = 8, WineName = "Chardonnay", Price = 29.99m, CategoryID = 2, Introduction = "A popular white wine known for its rich and creamy flavor.", ImageUrl = "/images/wine8.jpg" },
                new Wine { WineId = 9, WineName = "Sauvignon Blanc", Price = 25.00m, CategoryID = 2, Introduction = "A crisp and refreshing white wine with hints of citrus.", ImageUrl = "/images/wine9.jpg" },
                new Wine { WineId = 10, WineName = "Riesling", Price = 22.50m, CategoryID = 2, Introduction = "A sweet white wine with floral aromas and a crisp finish.", ImageUrl = "/images/wine10.jpg" },
                new Wine { WineId = 11, WineName = "Pinot Grigio", Price = 30.00m, CategoryID = 2, Introduction = "A light and zesty white wine with hints of green apple and pear.", ImageUrl = "/images/wine11.jpg" },
                new Wine { WineId = 12, WineName = "Gewürztraminer", Price = 35.00m, CategoryID = 2, Introduction = "A aromatic white wine with notes of lychee and rose petals.", ImageUrl = "/images/wine12.jpg" },
                new Wine { WineId = 13, WineName = "Moscato", Price = 20.00m, CategoryID = 2, Introduction = "A sweet and fruity white wine with flavors of peach and apricot.", ImageUrl = "/images/wine13.jpg" },
                new Wine { WineId = 14, WineName = "Viognier", Price = 40.00m, CategoryID = 2, Introduction = "A full-bodied white wine with floral and tropical fruit notes.", ImageUrl = "/images/wine14.jpg" },

                // Sparkling Wine
                new Wine { WineId = 15, WineName = "Champagne", Price = 60.00m, CategoryID = 3, Introduction = "A classic sparkling wine with crisp acidity and fine bubbles.", ImageUrl = "/images/wine15.jpg" },
                new Wine { WineId = 16, WineName = "Prosecco", Price = 25.00m, CategoryID = 3, Introduction = "A light and refreshing sparkling wine with flavors of green apple and melon.", ImageUrl = "/images/wine16.jpg" },
                new Wine { WineId = 17, WineName = "Cava", Price = 28.00m, CategoryID = 3, Introduction = "A Spanish sparkling wine with zesty citrus flavors and fine bubbles.", ImageUrl = "/images/wine17.jpg" },
                new Wine { WineId = 18, WineName = "Crémant", Price = 35.00m, CategoryID = 3, Introduction = "A French sparkling wine with delicate bubbles and notes of white peach.", ImageUrl = "/images/wine18.jpg" },
                new Wine { WineId = 19, WineName = "Asti Spumante", Price = 20.00m, CategoryID = 3, Introduction = "A sweet and fruity sparkling wine with flavors of honey and apricot.", ImageUrl = "/images/wine19.jpg" },
                new Wine { WineId = 20, WineName = "Sparkling Rosé", Price = 30.00m, CategoryID = 3, Introduction = "A sparkling wine with flavors of strawberry and raspberry.", ImageUrl = "/images/wine20.jpg" },
                new Wine { WineId = 21, WineName = "Brut Nature", Price = 45.00m, CategoryID = 3, Introduction = "A very dry sparkling wine with high acidity and mineral notes.", ImageUrl = "/images/wine21.jpg" },

                // Dessert Wine
                new Wine { WineId = 22, WineName = "Port", Price = 50.00m, CategoryID = 4, Introduction = "A rich and sweet fortified wine with flavors of dried fruit and spice.", ImageUrl = "/images/wine22.jpg" },
                new Wine { WineId = 23, WineName = "Sherry", Price = 30.00m, CategoryID = 4, Introduction = "A fortified wine with nutty flavors and hints of dried fruit.", ImageUrl = "/images/wine23.jpg" },
                new Wine { WineId = 24, WineName = "Madeira", Price = 40.00m, CategoryID = 4, Introduction = "A fortified wine with caramel and nutty flavors.", ImageUrl = "/images/wine24.jpg" },
                new Wine { WineId = 25, WineName = "Sauternes", Price = 55.00m, CategoryID = 4, Introduction = "A sweet wine with honey and apricot flavors.", ImageUrl = "/images/wine25.jpg" },
                new Wine { WineId = 26, WineName = "Tokaji", Price = 60.00m, CategoryID = 4, Introduction = "A Hungarian sweet wine with flavors of peach and honey.", ImageUrl = "/images/wine26.jpg" },
                new Wine { WineId = 27, WineName = "Ice Wine", Price = 65.00m, CategoryID = 4, Introduction = "A sweet wine made from frozen grapes, with intense fruit flavors.", ImageUrl = "/images/wine27.jpg" },
                new Wine { WineId = 28, WineName = "Muscat", Price = 35.00m, CategoryID = 4, Introduction = "A sweet wine with floral and fruity flavors.", ImageUrl = "/images/wine28.jpg" },

                // Rose Wine
                new Wine { WineId = 29, WineName = "Provence Rosé", Price = 25.00m, CategoryID = 5, Introduction = "A light and refreshing rosé with flavors of strawberry and citrus.", ImageUrl = "/images/wine29.jpg" },
                new Wine { WineId = 30, WineName = "White Zinfandel", Price = 20.00m, CategoryID = 5, Introduction = "A sweet rosé with flavors of cherry and raspberry.", ImageUrl = "/images/wine30.jpg" },
                new Wine { WineId = 31, WineName = "Grenache Rosé", Price = 30.00m, CategoryID = 5, Introduction = "A dry rosé with flavors of watermelon and strawberry.", ImageUrl = "/images/wine31.jpg" },
                new Wine { WineId = 32, WineName = "Syrah Rosé", Price = 35.00m, CategoryID = 5, Introduction = "A bold rosé with flavors of raspberry and spice.", ImageUrl = "/images/wine32.jpg" },
                new Wine { WineId = 33, WineName = "Pinot Noir Rosé", Price = 28.00m, CategoryID = 5, Introduction = "A delicate rosé with flavors of cherry and rose petals.", ImageUrl = "/images/wine33.jpg" },
                new Wine { WineId = 34, WineName = "Tempranillo Rosé", Price = 32.00m, CategoryID = 5, Introduction = "A Spanish rosé with flavors of strawberry and peach.", ImageUrl = "/images/wine34.jpg" },
                new Wine { WineId = 35, WineName = "Cabernet Franc Rosé", Price = 40.00m, CategoryID = 5, Introduction = "A rosé with flavors of red currant and green pepper.", ImageUrl = "/images/wine35.jpg" },

                // Fortified Wine
                new Wine { WineId = 36, WineName = "Marsala", Price = 25.00m, CategoryID = 6, Introduction = "A fortified wine with flavors of caramel and vanilla.", ImageUrl = "/images/wine36.jpg" },
                new Wine { WineId = 37, WineName = "Vermouth", Price = 20.00m, CategoryID = 6, Introduction = "A fortified wine with botanical flavors.", ImageUrl = "/images/wine37.jpg" },
                new Wine { WineId = 38, WineName = "Commandaria", Price = 35.00m, CategoryID = 6, Introduction = "A sweet fortified wine with flavors of dried fruit and honey.", ImageUrl = "/images/wine38.jpg" },
                new Wine { WineId = 39, WineName = "Banyuls", Price = 30.00m, CategoryID = 6, Introduction = "A fortified wine with flavors of chocolate and coffee.", ImageUrl = "/images/wine39.jpg" },
                new Wine { WineId = 40, WineName = "Pineau des Charentes", Price = 40.00m, CategoryID = 6, Introduction = "A fortified wine with flavors of apricot and almond.", ImageUrl = "/images/wine40.jpg" },
                new Wine { WineId = 41, WineName = "Madeira", Price = 45.00m, CategoryID = 6, Introduction = "A fortified wine with flavors of caramel and nuts.", ImageUrl = "/images/wine41.jpg" },
                new Wine { WineId = 42, WineName = "Sherry", Price = 30.00m, CategoryID = 6, Introduction = "A fortified wine with nutty flavors.", ImageUrl = "/images/wine42.jpg" },

                // Organic Wine
                new Wine { WineId = 43, WineName = "Organic Merlot", Price = 40.00m, CategoryID = 7, Introduction = "An organic red wine with flavors of black cherry and plum.", ImageUrl = "/images/wine43.jpg" },
                new Wine { WineId = 44, WineName = "Organic Chardonnay", Price = 35.00m, CategoryID = 7, Introduction = "An organic white wine with flavors of apple and pear.", ImageUrl = "/images/wine44.jpg" },
                new Wine { WineId = 45, WineName = "Organic Cabernet Sauvignon", Price = 50.00m, CategoryID = 7, Introduction = "An organic red wine with flavors of blackcurrant and oak.", ImageUrl = "/images/wine45.jpg" },
                new Wine { WineId = 46, WineName = "Organic Pinot Noir", Price = 45.00m, CategoryID = 7, Introduction = "An organic red wine with flavors of cherry and spice.", ImageUrl = "/images/wine46.jpg" },
                new Wine { WineId = 47, WineName = "Organic Sauvignon Blanc", Price = 30.00m, CategoryID = 7, Introduction = "An organic white wine with flavors of citrus and melon.", ImageUrl = "/images/wine47.jpg" },
                new Wine { WineId = 48, WineName = "Organic Zinfandel", Price = 38.00m, CategoryID = 7, Introduction = "An organic red wine with flavors of blackberry and pepper.", ImageUrl = "/images/wine48.jpg" },
                new Wine { WineId = 49, WineName = "Organic Syrah", Price = 55.00m, CategoryID = 7, Introduction = "An organic red wine with flavors of dark fruit and spice.", ImageUrl = "/images/wine49.jpg" },

                // Ice Wine
                new Wine { WineId = 50, WineName = "Vidal Ice Wine", Price = 60.00m, CategoryID = 8, Introduction = "A sweet ice wine with flavors of honey and apricot.", ImageUrl = "/images/wine50.jpg" },
                new Wine { WineId = 51, WineName = "Riesling Ice Wine", Price = 65.00m, CategoryID = 8, Introduction = "A sweet ice wine with flavors of peach and citrus.", ImageUrl = "/images/wine51.jpg" },
                new Wine { WineId = 52, WineName = "Cabernet Franc Ice Wine", Price = 70.00m, CategoryID = 8, Introduction = "A sweet ice wine with flavors of strawberry and rhubarb.", ImageUrl = "/images/wine52.jpg" },
                new Wine { WineId = 53, WineName = "Gewürztraminer Ice Wine", Price = 75.00m, CategoryID = 8, Introduction = "A sweet ice wine with flavors of lychee and rose petals.", ImageUrl = "/images/wine53.jpg" },
                new Wine { WineId = 54, WineName = "Chardonnay Ice Wine", Price = 55.00m, CategoryID = 8, Introduction = "A sweet ice wine with flavors of apple and pear.", ImageUrl = "/images/wine54.jpg" },
                new Wine { WineId = 55, WineName = "Merlot Ice Wine", Price = 80.00m, CategoryID = 8, Introduction = "A sweet ice wine with flavors of blackberry and plum.", ImageUrl = "/images/wine55.jpg" },
                new Wine { WineId = 56, WineName = "Pinot Noir Ice Wine", Price = 85.00m, CategoryID = 8, Introduction = "A sweet ice wine with flavors of cherry and spice.", ImageUrl = "/images/wine56.jpg" }
            );

            // 添加桌子数据
            modelBuilder.Entity<Table>().HasData(
                new Table { TableId = 1, TableName = "Table1" },
                new Table { TableId = 2, TableName = "Table2" },
                new Table { TableId = 3, TableName = "Table3" },
                new Table { TableId = 4, TableName = "Table4" },
                new Table { TableId = 5, TableName = "Table5" },
                new Table { TableId = 6, TableName = "Table6" },
                new Table { TableId = 7, TableName = "Table7" },
                new Table { TableId = 8, TableName = "Table8" }
            );
        }
    }
}
