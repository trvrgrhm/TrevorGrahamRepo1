using Microsoft.EntityFrameworkCore.Migrations;

namespace StoreApplication.Migrations
{
    public partial class migration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Inventories_InventoryId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_InventoryId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "InventoryId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Orders");

            migrationBuilder.CreateTable(
                name: "OrderLine",
                columns: table => new
                {
                    OrderLineId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: true),
                    InventoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderLine", x => x.OrderLineId);
                    table.ForeignKey(
                        name: "FK_OrderLine_Inventories_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventories",
                        principalColumn: "InventoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderLine_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderLine_InventoryId",
                table: "OrderLine",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLine_OrderId",
                table: "OrderLine",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderLine");

            migrationBuilder.AddColumn<int>(
                name: "InventoryId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_InventoryId",
                table: "Orders",
                column: "InventoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Inventories_InventoryId",
                table: "Orders",
                column: "InventoryId",
                principalTable: "Inventories",
                principalColumn: "InventoryId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
