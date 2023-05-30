using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShop.Migrations
{
    /// <inheritdoc />
    public partial class fixedDeleteMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_Products_ProductId",
                table: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Products_Name",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Item_ProductId",
                table: "Item");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Item",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$mi8LQj0FkmaMQxguXEGUwOalMo1/FwNLVwnjN5ohu6BKk4C8pMOXC");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Item");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$6T6tuav.XNl4WFBaOSC8Ceg7b1YgofB3Fez56IOAmkeb2r5w6/l2S");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Name",
                table: "Products",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Item_ProductId",
                table: "Item",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Products_ProductId",
                table: "Item",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
