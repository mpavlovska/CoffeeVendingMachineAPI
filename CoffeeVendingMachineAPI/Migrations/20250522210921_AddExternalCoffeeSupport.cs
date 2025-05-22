using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeVendingMachineAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddExternalCoffeeSupport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoffeeOrders_CoffeeTypes_CoffeeTypeId",
                table: "CoffeeOrders");

            migrationBuilder.AlterColumn<int>(
                name: "CoffeeTypeId",
                table: "CoffeeOrders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "ExternalCoffeeName",
                table: "CoffeeOrders",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_CoffeeOrders_CoffeeTypes_CoffeeTypeId",
                table: "CoffeeOrders",
                column: "CoffeeTypeId",
                principalTable: "CoffeeTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoffeeOrders_CoffeeTypes_CoffeeTypeId",
                table: "CoffeeOrders");

            migrationBuilder.DropColumn(
                name: "ExternalCoffeeName",
                table: "CoffeeOrders");

            migrationBuilder.AlterColumn<int>(
                name: "CoffeeTypeId",
                table: "CoffeeOrders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CoffeeOrders_CoffeeTypes_CoffeeTypeId",
                table: "CoffeeOrders",
                column: "CoffeeTypeId",
                principalTable: "CoffeeTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
