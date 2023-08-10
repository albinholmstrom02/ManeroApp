using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Orders_OrderEntityOrderId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_OrderEntityOrderId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "OrderEntityOrderId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "OrderDate",
                table: "Orders");

            migrationBuilder.CreateTable(
                name: "CartItemViewModel",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    OrderEntityOrderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItemViewModel", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_CartItemViewModel_Orders_OrderEntityOrderId",
                        column: x => x.OrderEntityOrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItemViewModel_OrderEntityOrderId",
                table: "CartItemViewModel",
                column: "OrderEntityOrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItemViewModel");

            migrationBuilder.AddColumn<int>(
                name: "OrderEntityOrderId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Products_OrderEntityOrderId",
                table: "Products",
                column: "OrderEntityOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Orders_OrderEntityOrderId",
                table: "Products",
                column: "OrderEntityOrderId",
                principalTable: "Orders",
                principalColumn: "OrderId");
        }
    }
}
