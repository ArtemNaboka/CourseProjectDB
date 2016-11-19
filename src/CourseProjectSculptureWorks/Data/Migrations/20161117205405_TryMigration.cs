using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CourseProjectSculptureWorks.Data.Migrations
{
    public partial class TryMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    LocationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(maxLength: 50, nullable: false),
                    City = table.Column<string>(maxLength: 30, nullable: false),
                    Country = table.Column<string>(maxLength: 30, nullable: false),
                    DurationOfExcursion = table.Column<int>(nullable: false),
                    LocationName = table.Column<string>(maxLength: 50, nullable: false),
                    LocationType = table.Column<string>(nullable: false),
                    PriceForPerson = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.LocationId);
                });

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Sculptures",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sculptures_LocationId",
                table: "Sculptures",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sculptures_Locations_LocationId",
                table: "Sculptures",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "LocationId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sculptures_Locations_LocationId",
                table: "Sculptures");

            migrationBuilder.DropIndex(
                name: "IX_Sculptures_LocationId",
                table: "Sculptures");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Sculptures");

            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}
