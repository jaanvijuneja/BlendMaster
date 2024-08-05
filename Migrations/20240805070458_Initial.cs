using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApplication2.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Recipe",
                columns: table => new
                {
                    RecipeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Ingredients = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Instructions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipe", x => x.RecipeId);
                });

            migrationBuilder.CreateTable(
                name: "Table",
                columns: table => new
                {
                    TableId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TableName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Table", x => x.TableId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Product_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerOrder",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderStatus = table.Column<int>(type: "int", nullable: false),
                    TableId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerOrder", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_CustomerOrder_Table_TableId",
                        column: x => x.TableId,
                        principalTable: "Table",
                        principalColumn: "TableId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bill",
                columns: table => new
                {
                    BillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerOrderOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TableId = table.Column<int>(type: "int", nullable: false),
                    BillDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Tax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AmountPaid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentMethod = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bill", x => x.BillId);
                    table.ForeignKey(
                        name: "FK_Bill_CustomerOrder_CustomerOrderOrderId",
                        column: x => x.CustomerOrderOrderId,
                        principalTable: "CustomerOrder",
                        principalColumn: "OrderId");
                    table.ForeignKey(
                        name: "FK_Bill_Table_TableId",
                        column: x => x.TableId,
                        principalTable: "Table",
                        principalColumn: "TableId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetail",
                columns: table => new
                {
                    OrderDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetail", x => x.OrderDetailId);
                    table.ForeignKey(
                        name: "FK_OrderDetail_CustomerOrder_OrderId",
                        column: x => x.OrderId,
                        principalTable: "CustomerOrder",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetail_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "CategoryId", "CategoryName" },
                values: new object[,]
                {
                    { new Guid("48fe2be4-0d0f-44a7-a22d-6c5ecde7cde4"), "Classic" },
                    { new Guid("bdeb2a78-0127-48c9-b0ec-a69bd6b47d94"), "Modern" },
                    { new Guid("d7ee74db-e0c0-4d5a-b236-51e2ee551b20"), "Tropical" }
                });

            migrationBuilder.InsertData(
                table: "Table",
                columns: new[] { "TableId", "TableName" },
                values: new object[,]
                {
                    { 1, "Table1" },
                    { 2, "Table2" },
                    { 3, "Table3" },
                    { 4, "Table4" },
                    { 5, "Table5" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "Email", "Name", "Password" },
                values: new object[] { new Guid("b18c94b5-8caf-4b8a-a25d-148f6d425b13"), "email@example.com", "Bartender", "$2a$11$pT.kvgXwJ2aksguJ1lb4jOKuBQFbV9MdVp7tJ7OXgDUsteveblGPa" });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "CategoryId", "ImageUrl", "Price", "ProductDescription", "ProductName" },
                values: new object[,]
                {
                    { new Guid("242d227f-ee94-4d8a-9e0d-87ef7a65c40b"), new Guid("bdeb2a78-0127-48c9-b0ec-a69bd6b47d94"), null, 10.49m, "A refreshing cocktail with gin, cucumber, lime, and mint.", "Cucumber Cooler" },
                    { new Guid("593ab29d-3e7b-489c-b0ef-01be3d83fff2"), new Guid("d7ee74db-e0c0-4d5a-b236-51e2ee551b20"), null, 10.99m, "A tropical mix of rum, lime juice, and orgeat syrup.", "Mai Tai" },
                    { new Guid("6b9cb6c0-b4e6-4dcf-9c3b-f625567e74de"), new Guid("d7ee74db-e0c0-4d5a-b236-51e2ee551b20"), null, 9.99m, "A creamy blend of rum, pineapple juice, and coconut cream.", "Pina Colada" },
                    { new Guid("7231940c-d749-4ced-b914-2ef21cccad25"), new Guid("48fe2be4-0d0f-44a7-a22d-6c5ecde7cde4"), null, 12.99m, "A timeless cocktail featuring bourbon, sugar, and bitters.", "Old Fashioned" },
                    { new Guid("977736eb-aabd-471e-9b14-dc1d108083c4"), new Guid("bdeb2a78-0127-48c9-b0ec-a69bd6b47d94"), null, 13.49m, "A sophisticated mix of vodka, coffee liqueur, and espresso.", "Espresso Martini" },
                    { new Guid("f6788097-baf0-4b8d-adee-77b78b94e5e8"), new Guid("48fe2be4-0d0f-44a7-a22d-6c5ecde7cde4"), null, 11.99m, "A sophisticated cocktail made with gin and vermouth, garnished with an olive.", "Martini" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bill_CustomerOrderOrderId",
                table: "Bill",
                column: "CustomerOrderOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Bill_TableId",
                table: "Bill",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrder_TableId",
                table: "CustomerOrder",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_OrderId",
                table: "OrderDetail",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_ProductId",
                table: "OrderDetail",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategoryId",
                table: "Product",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bill");

            migrationBuilder.DropTable(
                name: "OrderDetail");

            migrationBuilder.DropTable(
                name: "Recipe");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "CustomerOrder");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Table");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
