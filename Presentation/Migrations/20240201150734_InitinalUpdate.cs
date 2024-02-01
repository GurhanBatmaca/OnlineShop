using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Presentation.Migrations
{
    /// <inheritdoc />
    public partial class InitinalUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderState",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "OrderStateId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "OrderStates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStates", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "OrderStates",
                columns: new[] { "Id", "Name", "Url" },
                values: new object[,]
                {
                    { 1, "Beklemede", "beklemede" },
                    { 2, "Hazırlanıyor", "hazirlaniyor" },
                    { 3, "Kargoda", "kargoda" },
                    { 4, "Teslim Edildi", "teslim-edildi" },
                    { 5, "İptal Edildi", "iptal-edildi" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderStateId",
                table: "Orders",
                column: "OrderStateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderStates_OrderStateId",
                table: "Orders",
                column: "OrderStateId",
                principalTable: "OrderStates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderStates_OrderStateId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "OrderStates");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderStateId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderStateId",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "OrderState",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
