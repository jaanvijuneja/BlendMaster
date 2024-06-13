using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BlendMaster.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoryList",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryList", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "TableList",
                columns: table => new
                {
                    TableId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TableName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableList", x => x.TableId);
                });

            migrationBuilder.CreateTable(
                name: "WineList",
                columns: table => new
                {
                    WineId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WineName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false),
                    Introduction = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WineList", x => x.WineId);
                });

            migrationBuilder.CreateTable(
                name: "OrderList",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TableID = table.Column<int>(type: "int", nullable: false),
                    OrderMoney = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderList", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_OrderList_TableList_TableID",
                        column: x => x.TableID,
                        principalTable: "TableList",
                        principalColumn: "TableId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetailList",
                columns: table => new
                {
                    OrderDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    WineID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetailList", x => x.OrderDetailId);
                    table.ForeignKey(
                        name: "FK_OrderDetailList_OrderList_OrderID",
                        column: x => x.OrderID,
                        principalTable: "OrderList",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetailList_WineList_WineID",
                        column: x => x.WineID,
                        principalTable: "WineList",
                        principalColumn: "WineId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CategoryList",
                columns: new[] { "CategoryId", "CategoryName" },
                values: new object[,]
                {
                    { 1, "Red Wine" },
                    { 2, "White Wine" },
                    { 3, "Sparkling Wine" },
                    { 4, "Dessert Wine" },
                    { 5, "Rose Wine" },
                    { 6, "Fortified Wine" },
                    { 7, "Organic Wine" },
                    { 8, "Ice Wine" }
                });

            migrationBuilder.InsertData(
                table: "TableList",
                columns: new[] { "TableId", "TableName" },
                values: new object[,]
                {
                    { 1, "Table1" },
                    { 2, "Table2" },
                    { 3, "Table3" },
                    { 4, "Table4" },
                    { 5, "Table5" },
                    { 6, "Table6" },
                    { 7, "Table7" },
                    { 8, "Table8" }
                });

            migrationBuilder.InsertData(
                table: "WineList",
                columns: new[] { "WineId", "CategoryID", "ImageUrl", "Introduction", "Price", "WineName" },
                values: new object[,]
                {
                    { 1, 1, "/images/wine1.jpg", "A smooth and fruity red wine, perfect for any occasion.", 35.50m, "Merlot" },
                    { 2, 1, "/images/wine2.jpg", "A bold red wine with notes of blackcurrant and a hint of oak.", 50.00m, "Cabernet Sauvignon" },
                    { 3, 1, "/images/wine3.jpg", "A delicate red wine with notes of cherry and spice.", 45.00m, "Pinot Noir" },
                    { 4, 1, "/images/wine4.jpg", "A robust red wine with rich flavors of blackberry and pepper.", 40.00m, "Zinfandel" },
                    { 5, 1, "/images/wine5.jpg", "A spicy and bold red wine with dark fruit flavors.", 55.00m, "Syrah" },
                    { 6, 1, "/images/wine6.jpg", "A full-bodied red wine with flavors of plum and black cherry.", 38.00m, "Malbec" },
                    { 7, 1, "/images/wine7.jpg", "A classic Italian red wine with flavors of dark cherry and oregano.", 42.00m, "Sangiovese" },
                    { 8, 2, "/images/wine8.jpg", "A popular white wine known for its rich and creamy flavor.", 29.99m, "Chardonnay" },
                    { 9, 2, "/images/wine9.jpg", "A crisp and refreshing white wine with hints of citrus.", 25.00m, "Sauvignon Blanc" },
                    { 10, 2, "/images/wine10.jpg", "A sweet white wine with floral aromas and a crisp finish.", 22.50m, "Riesling" },
                    { 11, 2, "/images/wine11.jpg", "A light and zesty white wine with hints of green apple and pear.", 30.00m, "Pinot Grigio" },
                    { 12, 2, "/images/wine12.jpg", "A aromatic white wine with notes of lychee and rose petals.", 35.00m, "Gewürztraminer" },
                    { 13, 2, "/images/wine13.jpg", "A sweet and fruity white wine with flavors of peach and apricot.", 20.00m, "Moscato" },
                    { 14, 2, "/images/wine14.jpg", "A full-bodied white wine with floral and tropical fruit notes.", 40.00m, "Viognier" },
                    { 15, 3, "/images/wine15.jpg", "A classic sparkling wine with crisp acidity and fine bubbles.", 60.00m, "Champagne" },
                    { 16, 3, "/images/wine16.jpg", "A light and refreshing sparkling wine with flavors of green apple and melon.", 25.00m, "Prosecco" },
                    { 17, 3, "/images/wine17.jpg", "A Spanish sparkling wine with zesty citrus flavors and fine bubbles.", 28.00m, "Cava" },
                    { 18, 3, "/images/wine18.jpg", "A French sparkling wine with delicate bubbles and notes of white peach.", 35.00m, "Crémant" },
                    { 19, 3, "/images/wine19.jpg", "A sweet and fruity sparkling wine with flavors of honey and apricot.", 20.00m, "Asti Spumante" },
                    { 20, 3, "/images/wine20.jpg", "A sparkling wine with flavors of strawberry and raspberry.", 30.00m, "Sparkling Rosé" },
                    { 21, 3, "/images/wine21.jpg", "A very dry sparkling wine with high acidity and mineral notes.", 45.00m, "Brut Nature" },
                    { 22, 4, "/images/wine22.jpg", "A rich and sweet fortified wine with flavors of dried fruit and spice.", 50.00m, "Port" },
                    { 23, 4, "/images/wine23.jpg", "A fortified wine with nutty flavors and hints of dried fruit.", 30.00m, "Sherry" },
                    { 24, 4, "/images/wine24.jpg", "A fortified wine with caramel and nutty flavors.", 40.00m, "Madeira" },
                    { 25, 4, "/images/wine25.jpg", "A sweet wine with honey and apricot flavors.", 55.00m, "Sauternes" },
                    { 26, 4, "/images/wine26.jpg", "A Hungarian sweet wine with flavors of peach and honey.", 60.00m, "Tokaji" },
                    { 27, 4, "/images/wine27.jpg", "A sweet wine made from frozen grapes, with intense fruit flavors.", 65.00m, "Ice Wine" },
                    { 28, 4, "/images/wine28.jpg", "A sweet wine with floral and fruity flavors.", 35.00m, "Muscat" },
                    { 29, 5, "/images/wine29.jpg", "A light and refreshing rosé with flavors of strawberry and citrus.", 25.00m, "Provence Rosé" },
                    { 30, 5, "/images/wine30.jpg", "A sweet rosé with flavors of cherry and raspberry.", 20.00m, "White Zinfandel" },
                    { 31, 5, "/images/wine31.jpg", "A dry rosé with flavors of watermelon and strawberry.", 30.00m, "Grenache Rosé" },
                    { 32, 5, "/images/wine32.jpg", "A bold rosé with flavors of raspberry and spice.", 35.00m, "Syrah Rosé" },
                    { 33, 5, "/images/wine33.jpg", "A delicate rosé with flavors of cherry and rose petals.", 28.00m, "Pinot Noir Rosé" },
                    { 34, 5, "/images/wine34.jpg", "A Spanish rosé with flavors of strawberry and peach.", 32.00m, "Tempranillo Rosé" },
                    { 35, 5, "/images/wine35.jpg", "A rosé with flavors of red currant and green pepper.", 40.00m, "Cabernet Franc Rosé" },
                    { 36, 6, "/images/wine36.jpg", "A fortified wine with flavors of caramel and vanilla.", 25.00m, "Marsala" },
                    { 37, 6, "/images/wine37.jpg", "A fortified wine with botanical flavors.", 20.00m, "Vermouth" },
                    { 38, 6, "/images/wine38.jpg", "A sweet fortified wine with flavors of dried fruit and honey.", 35.00m, "Commandaria" },
                    { 39, 6, "/images/wine39.jpg", "A fortified wine with flavors of chocolate and coffee.", 30.00m, "Banyuls" },
                    { 40, 6, "/images/wine40.jpg", "A fortified wine with flavors of apricot and almond.", 40.00m, "Pineau des Charentes" },
                    { 41, 6, "/images/wine41.jpg", "A fortified wine with flavors of caramel and nuts.", 45.00m, "Madeira" },
                    { 42, 6, "/images/wine42.jpg", "A fortified wine with nutty flavors.", 30.00m, "Sherry" },
                    { 43, 7, "/images/wine43.jpg", "An organic red wine with flavors of black cherry and plum.", 40.00m, "Organic Merlot" },
                    { 44, 7, "/images/wine44.jpg", "An organic white wine with flavors of apple and pear.", 35.00m, "Organic Chardonnay" },
                    { 45, 7, "/images/wine45.jpg", "An organic red wine with flavors of blackcurrant and oak.", 50.00m, "Organic Cabernet Sauvignon" },
                    { 46, 7, "/images/wine46.jpg", "An organic red wine with flavors of cherry and spice.", 45.00m, "Organic Pinot Noir" },
                    { 47, 7, "/images/wine47.jpg", "An organic white wine with flavors of citrus and melon.", 30.00m, "Organic Sauvignon Blanc" },
                    { 48, 7, "/images/wine48.jpg", "An organic red wine with flavors of blackberry and pepper.", 38.00m, "Organic Zinfandel" },
                    { 49, 7, "/images/wine49.jpg", "An organic red wine with flavors of dark fruit and spice.", 55.00m, "Organic Syrah" },
                    { 50, 8, "/images/wine50.jpg", "A sweet ice wine with flavors of honey and apricot.", 60.00m, "Vidal Ice Wine" },
                    { 51, 8, "/images/wine51.jpg", "A sweet ice wine with flavors of peach and citrus.", 65.00m, "Riesling Ice Wine" },
                    { 52, 8, "/images/wine52.jpg", "A sweet ice wine with flavors of strawberry and rhubarb.", 70.00m, "Cabernet Franc Ice Wine" },
                    { 53, 8, "/images/wine53.jpg", "A sweet ice wine with flavors of lychee and rose petals.", 75.00m, "Gewürztraminer Ice Wine" },
                    { 54, 8, "/images/wine54.jpg", "A sweet ice wine with flavors of apple and pear.", 55.00m, "Chardonnay Ice Wine" },
                    { 55, 8, "/images/wine55.jpg", "A sweet ice wine with flavors of blackberry and plum.", 80.00m, "Merlot Ice Wine" },
                    { 56, 8, "/images/wine56.jpg", "A sweet ice wine with flavors of cherry and spice.", 85.00m, "Pinot Noir Ice Wine" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetailList_OrderID",
                table: "OrderDetailList",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetailList_WineID",
                table: "OrderDetailList",
                column: "WineID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderList_TableID",
                table: "OrderList",
                column: "TableID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryList");

            migrationBuilder.DropTable(
                name: "OrderDetailList");

            migrationBuilder.DropTable(
                name: "OrderList");

            migrationBuilder.DropTable(
                name: "WineList");

            migrationBuilder.DropTable(
                name: "TableList");
        }
    }
}
