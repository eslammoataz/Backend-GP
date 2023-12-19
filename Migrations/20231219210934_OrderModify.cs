using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class OrderModify : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Services_ServiceID",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Orders_OrderID",
                table: "Services");

            migrationBuilder.AlterColumn<string>(
                name: "CriteriaID",
                table: "Services",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "ServiceID",
                table: "Schedules",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "Orders",
                type: "decimal(65,30)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Services_ServiceID",
                table: "Schedules",
                column: "ServiceID",
                principalTable: "Services",
                principalColumn: "ServiceID");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Orders_OrderID",
                table: "Services",
                column: "OrderID",
                principalTable: "Orders",
                principalColumn: "OrderID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Services_ServiceID",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Orders_OrderID",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "CriteriaID",
                keyValue: null,
                column: "CriteriaID",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "CriteriaID",
                table: "Services",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ServiceID",
                keyValue: null,
                column: "ServiceID",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "ServiceID",
                table: "Schedules",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Services_ServiceID",
                table: "Schedules",
                column: "ServiceID",
                principalTable: "Services",
                principalColumn: "ServiceID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Orders_OrderID",
                table: "Services",
                column: "OrderID",
                principalTable: "Orders",
                principalColumn: "OrderID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
