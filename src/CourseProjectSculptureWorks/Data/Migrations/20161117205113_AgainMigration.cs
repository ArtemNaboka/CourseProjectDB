using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseProjectSculptureWorks.Data.Migrations
{
    public partial class AgainMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "Sculptors",
                maxLength: 25,
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "Sculptors",
                maxLength: 15,
                nullable: false);
        }
    }
}
