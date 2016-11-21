using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseProjectSculptureWorks.Data.Migrations
{
    public partial class SeventhMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Compositions",
                columns: table => new
                {
                    LocationId = table.Column<int>(nullable: false),
                    ExcursionId = table.Column<int>(nullable: false),
                    SerialNumber = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compositions", x => new { x.LocationId, x.ExcursionId });
                    table.ForeignKey(
                        name: "FK_Compositions_Excursion_ExcursionId",
                        column: x => x.ExcursionId,
                        principalTable: "Excursion",
                        principalColumn: "ExcursionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Compositions_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Compositions_ExcursionId",
                table: "Compositions",
                column: "ExcursionId");

            migrationBuilder.CreateIndex(
                name: "IX_Compositions_LocationId",
                table: "Compositions",
                column: "LocationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Compositions");
        }
    }
}
