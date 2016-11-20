using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseProjectSculptureWorks.Data.Migrations
{
    public partial class ThirdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExcursionTypes",
                columns: table => new
                {
                    NameOfType = table.Column<string>(nullable: false),
                    Discount = table.Column<int>(nullable: false),
                    MaxNumberOfPeople = table.Column<int>(nullable: false),
                    MinNumberOfPeople = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExcursionTypes", x => x.NameOfType);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExcursionTypes");
        }
    }
}
