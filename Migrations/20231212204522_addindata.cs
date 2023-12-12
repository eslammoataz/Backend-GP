using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class addindata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: "4391ce53-c0e2-4c48-9768-636850006502");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "61868c39-5105-4626-9627-adea75f304ce");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a734193a-30aa-4e77-9076-bc5df5801efe");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "4391ce53-c0e2-4c48-9768-636850006502");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "49fc6b32-a3ef-47d9-b45a-5cd5b03bf3f7", "1", "Admin", "ADMIN" },
                    { "54ceb25a-aa61-4e8d-9796-3a1df5c67c0d", "2", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1", 0, "8ae46922-08c0-43b4-ad5f-768dd9577c20", "admin@example.com", true, null, null, false, null, "ADMIN@EXAMPLE.COM", "ADMIN@EXAMPLE.COM", "YourHashedPassword", null, false, "", false, "admin@example.com" });

            migrationBuilder.InsertData(
                table: "Admins",
                column: "Id",
                value: "1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "49fc6b32-a3ef-47d9-b45a-5cd5b03bf3f7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "54ceb25a-aa61-4e8d-9796-3a1df5c67c0d");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "61868c39-5105-4626-9627-adea75f304ce", "2", "User", "USER" },
                    { "a734193a-30aa-4e77-9076-bc5df5801efe", "1", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "4391ce53-c0e2-4c48-9768-636850006502", 0, "08b7fa5f-e2f5-451b-b18c-cf2c5d69bcc3", "admin@admin.com", false, null, null, false, null, null, null, "admin", null, false, "47983c83-8f16-4f94-b874-1f41d73c007c", false, "admin" });

            migrationBuilder.InsertData(
                table: "Admins",
                column: "Id",
                value: "4391ce53-c0e2-4c48-9768-636850006502");
        }
    }
}
