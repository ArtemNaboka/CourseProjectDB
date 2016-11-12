using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CourseProjectSculptureWorks.Data.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Styles",
                columns: table => new
                {
                    StyleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Country = table.Column<string>(maxLength: 20, nullable: false),
                    Era = table.Column<string>(nullable: false),
                    StyleName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Styles", x => x.StyleId);
                });

            migrationBuilder.CreateTable(
                name: "Sculptures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Height = table.Column<double>(nullable: false),
                    Material = table.Column<string>(maxLength: 20, nullable: false),
                    Name = table.Column<string>(maxLength: 25, nullable: false),
                    Square = table.Column<double>(nullable: false),
                    StyleId = table.Column<int>(nullable: true),
                    Type = table.Column<string>(nullable: false),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sculptures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sculptures_Styles_StyleId",
                        column: x => x.StyleId,
                        principalTable: "Styles",
                        principalColumn: "StyleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sculptures_StyleId",
                table: "Sculptures",
                column: "StyleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sculptures");

            migrationBuilder.DropTable(
                name: "Styles");
        }
    }
}
