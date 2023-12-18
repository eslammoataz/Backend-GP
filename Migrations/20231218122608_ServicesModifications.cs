using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class ServicesModifications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_Criteria_CriteriaID",
                table: "Services");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Criteria",
                table: "Criteria");

            migrationBuilder.RenameTable(
                name: "Criteria",
                newName: "Criterias");

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "AvailabilityStatus",
                keyValue: null,
                column: "AvailabilityStatus",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "AvailabilityStatus",
                table: "Services",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Criterias",
                table: "Criterias",
                column: "CriteriaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Criterias_CriteriaID",
                table: "Services",
                column: "CriteriaID",
                principalTable: "Criterias",
                principalColumn: "CriteriaID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_Criterias_CriteriaID",
                table: "Services");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Criterias",
                table: "Criterias");

            migrationBuilder.RenameTable(
                name: "Criterias",
                newName: "Criteria");

            migrationBuilder.AlterColumn<string>(
                name: "AvailabilityStatus",
                table: "Services",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Criteria",
                table: "Criteria",
                column: "CriteriaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Criteria_CriteriaID",
                table: "Services",
                column: "CriteriaID",
                principalTable: "Criteria",
                principalColumn: "CriteriaID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
