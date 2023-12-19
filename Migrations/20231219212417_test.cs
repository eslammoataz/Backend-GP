using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_Orders_OrderID",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_OrderID",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "OrderID",
                table: "Services");

            migrationBuilder.AddColumn<string>(
                name: "OrderID",
                table: "WorkerServices",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_WorkerServices_OrderID",
                table: "WorkerServices",
                column: "OrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkerServices_Orders_OrderID",
                table: "WorkerServices",
                column: "OrderID",
                principalTable: "Orders",
                principalColumn: "OrderID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkerServices_Orders_OrderID",
                table: "WorkerServices");

            migrationBuilder.DropIndex(
                name: "IX_WorkerServices_OrderID",
                table: "WorkerServices");

            migrationBuilder.DropColumn(
                name: "OrderID",
                table: "WorkerServices");

            migrationBuilder.AddColumn<string>(
                name: "OrderID",
                table: "Services",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Services_OrderID",
                table: "Services",
                column: "OrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Orders_OrderID",
                table: "Services",
                column: "OrderID",
                principalTable: "Orders",
                principalColumn: "OrderID");
        }
    }
}
