using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MenuVoting.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class seedRestaurants : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "Address", "Name" },
                values: new object[,]
                {
                    { new Guid("2c578a19-98dc-48c0-a460-3af245ce8d4e"), "Washinghton 7", "KFC" },
                    { new Guid("a08aff22-1f11-4913-8296-6d96ac706321"), "Sydney 10", "McDonalds" },
                    { new Guid("c5739bd7-0375-4b80-9bef-c76406d3fb39"), "Toronto 7", "KFC" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("2c578a19-98dc-48c0-a460-3af245ce8d4e"));

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("a08aff22-1f11-4913-8296-6d96ac706321"));

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("c5739bd7-0375-4b80-9bef-c76406d3fb39"));
        }
    }
}
