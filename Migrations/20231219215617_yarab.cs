using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class yarab : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0a64d4a6-d169-4fc0-8d14-ce8a826d786d", "1", "Admin", "ADMIN" },
                    { "3b96100b-e029-4a5c-8d51-bf10e93cbee8", "2", "Customer", "CUSTOMER" },
                    { "760a1668-dd0b-4390-a7c5-9d3a40ca0bcd", "1", "Worker", "WORKER" }
                });

            migrationBuilder.InsertData(
                table: "OrderStatuses",
                columns: new[] { "OrderStatusID", "StatusName" },
                values: new object[] { "1", "Set" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0a64d4a6-d169-4fc0-8d14-ce8a826d786d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3b96100b-e029-4a5c-8d51-bf10e93cbee8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "760a1668-dd0b-4390-a7c5-9d3a40ca0bcd");

            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "OrderStatusID",
                keyValue: "1");
        }
    }
}
