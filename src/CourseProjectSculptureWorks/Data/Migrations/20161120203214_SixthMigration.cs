using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CourseProjectSculptureWorks.Data.Migrations
{
    public partial class SixthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Excursion",
                columns: table => new
                {
                    ExcursionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateOfExcursion = table.Column<DateTime>(nullable: false),
                    ExcursionTypeId = table.Column<int>(nullable: true),
                    NumberOfPeople = table.Column<int>(nullable: false),
                    PriceOfExcursion = table.Column<decimal>(nullable: false),
                    Subjects = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Excursion", x => x.ExcursionId);
                    table.ForeignKey(
                        name: "FK_Excursion_ExcursionTypes_ExcursionTypeId",
                        column: x => x.ExcursionTypeId,
                        principalTable: "ExcursionTypes",
                        principalColumn: "ExcursionTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Excursion_ExcursionTypeId",
                table: "Excursion",
                column: "ExcursionTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Excursion");
        }
    }
}
