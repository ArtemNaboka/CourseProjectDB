using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CourseProjectSculptureWorks.Data.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sculptures_Styles_StyleId",
                table: "Sculptures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Styles",
                table: "Styles");

            migrationBuilder.DropColumn(
                name: "StyleId",
                table: "Styles");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Styles",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Styles",
                table: "Styles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sculptures_Styles_StyleId",
                table: "Sculptures",
                column: "StyleId",
                principalTable: "Styles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sculptures_Styles_StyleId",
                table: "Sculptures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Styles",
                table: "Styles");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Styles");

            migrationBuilder.AddColumn<int>(
                name: "StyleId",
                table: "Styles",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Styles",
                table: "Styles",
                column: "StyleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sculptures_Styles_StyleId",
                table: "Sculptures",
                column: "StyleId",
                principalTable: "Styles",
                principalColumn: "StyleId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
