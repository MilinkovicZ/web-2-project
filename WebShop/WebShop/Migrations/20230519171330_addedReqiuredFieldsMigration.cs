using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShop.Migrations
{
    /// <inheritdoc />
    public partial class addedReqiuredFieldsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$va6X7Sph1CMI5xbC63jLh.eFrqaVKnQnK/DO8O.V5hC0EYe3CBIt.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$tUBZMkh2KjDKdtPGNg1O8ebLNlPAR5N5v3i2z1MJ/ngcFQpBPeIb.");
        }
    }
}
