using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ChocolateManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class firstmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChocolateFactories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChocolateFactories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Wholesalers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wholesalers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WholesalersChocolateBarsStocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChocolateBarId = table.Column<int>(type: "int", nullable: false),
                    WholesalerId = table.Column<int>(type: "int", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WholesalersChocolateBarsStocks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChocolateBars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cacao = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FactoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChocolateBars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChocolateBars_ChocolateFactories_FactoryId",
                        column: x => x.FactoryId,
                        principalTable: "ChocolateFactories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChocolateBarWholesaler",
                columns: table => new
                {
                    ChocolateBarsId = table.Column<int>(type: "int", nullable: false),
                    WholesalersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChocolateBarWholesaler", x => new { x.ChocolateBarsId, x.WholesalersId });
                    table.ForeignKey(
                        name: "FK_ChocolateBarWholesaler_ChocolateBars_ChocolateBarsId",
                        column: x => x.ChocolateBarsId,
                        principalTable: "ChocolateBars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChocolateBarWholesaler_Wholesalers_WholesalersId",
                        column: x => x.WholesalersId,
                        principalTable: "Wholesalers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ChocolateFactories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Neuhaus" },
                    { 2, "ChocoPlus" }
                });

            migrationBuilder.InsertData(
                table: "Wholesalers",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Mamadou" },
                    { 2, "Keita" },
                    { 3, "Heung" },
                    { 4, "Pedro" }
                });

            migrationBuilder.InsertData(
                table: "ChocolateBars",
                columns: new[] { "Id", "Cacao", "FactoryId", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 12.5m, 1, "White Chocolate", 9.5m },
                    { 2, 20m, 1, "Beast Chocolate", 22.99m },
                    { 3, 5m, 1, "Mix Chocolate", 5.5m },
                    { 4, 10m, 2, "Strawberry Chocolate", 12m },
                    { 5, 11m, 2, "Dark Chocolate", 9m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChocolateBars_FactoryId",
                table: "ChocolateBars",
                column: "FactoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ChocolateBarWholesaler_WholesalersId",
                table: "ChocolateBarWholesaler",
                column: "WholesalersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChocolateBarWholesaler");

            migrationBuilder.DropTable(
                name: "WholesalersChocolateBarsStocks");

            migrationBuilder.DropTable(
                name: "ChocolateBars");

            migrationBuilder.DropTable(
                name: "Wholesalers");

            migrationBuilder.DropTable(
                name: "ChocolateFactories");
        }
    }
}
