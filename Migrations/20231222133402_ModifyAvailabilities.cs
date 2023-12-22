using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class ModifyAvailabilities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkerAvailabilities");

            migrationBuilder.CreateTable(
                name: "ProviderAvailabilities",
                columns: table => new
                {
                    ProviderAvailabilityID = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ServiceProviderID = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AvailabilityDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProviderAvailabilities", x => x.ProviderAvailabilityID);
                    table.ForeignKey(
                        name: "FK_ProviderAvailabilities_ServiceProviders_ServiceProviderID",
                        column: x => x.ServiceProviderID,
                        principalTable: "ServiceProviders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Slots",
                columns: table => new
                {
                    TimeSlotID = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StartTime = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    enable = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    ProviderAvailabilityID = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slots", x => x.TimeSlotID);
                    table.ForeignKey(
                        name: "FK_Slots_ProviderAvailabilities_ProviderAvailabilityID",
                        column: x => x.ProviderAvailabilityID,
                        principalTable: "ProviderAvailabilities",
                        principalColumn: "ProviderAvailabilityID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ProviderAvailabilities_ServiceProviderID",
                table: "ProviderAvailabilities",
                column: "ServiceProviderID");

            migrationBuilder.CreateIndex(
                name: "IX_Slots_ProviderAvailabilityID",
                table: "Slots",
                column: "ProviderAvailabilityID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Slots");

            migrationBuilder.DropTable(
                name: "ProviderAvailabilities");

            migrationBuilder.CreateTable(
                name: "WorkerAvailabilities",
                columns: table => new
                {
                    WorkerAvailabilityID = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ServiceID = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    WorkerID = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DayOfWeek = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EndTime = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkerAvailabilities", x => x.WorkerAvailabilityID);
                    table.ForeignKey(
                        name: "FK_WorkerAvailabilities_Services_ServiceID",
                        column: x => x.ServiceID,
                        principalTable: "Services",
                        principalColumn: "ServiceID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkerAvailabilities_Workers_WorkerID",
                        column: x => x.WorkerID,
                        principalTable: "Workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_WorkerAvailabilities_ServiceID",
                table: "WorkerAvailabilities",
                column: "ServiceID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkerAvailabilities_WorkerID",
                table: "WorkerAvailabilities",
                column: "WorkerID");
        }
    }
}
