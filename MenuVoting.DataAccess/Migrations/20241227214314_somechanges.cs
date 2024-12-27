using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MenuVoting.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class somechanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Components",
                table: "Menus",
                newName: "Dishes");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Date",
                table: "MenuPools",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Dishes",
                table: "Menus",
                newName: "Components");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "MenuPools",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");
        }
    }
}
