using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseProjectSculptureWorks.Data.Migrations
{
    public partial class RenameMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compositions_Excursion_ExcursionId",
                table: "Compositions");

            migrationBuilder.DropForeignKey(
                name: "FK_Excursion_ExcursionTypes_ExcursionTypeId",
                table: "Excursion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Excursion",
                table: "Excursion");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Excursions",
                table: "Excursion",
                column: "ExcursionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Compositions_Excursions_ExcursionId",
                table: "Compositions",
                column: "ExcursionId",
                principalTable: "Excursion",
                principalColumn: "ExcursionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Excursions_ExcursionTypes_ExcursionTypeId",
                table: "Excursion",
                column: "ExcursionTypeId",
                principalTable: "ExcursionTypes",
                principalColumn: "ExcursionTypeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.RenameIndex(
                name: "IX_Excursion_ExcursionTypeId",
                table: "Excursion",
                newName: "IX_Excursions_ExcursionTypeId");

            migrationBuilder.RenameTable(
                name: "Excursion",
                newName: "Excursions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compositions_Excursions_ExcursionId",
                table: "Compositions");

            migrationBuilder.DropForeignKey(
                name: "FK_Excursions_ExcursionTypes_ExcursionTypeId",
                table: "Excursions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Excursions",
                table: "Excursions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Excursion",
                table: "Excursions",
                column: "ExcursionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Compositions_Excursion_ExcursionId",
                table: "Compositions",
                column: "ExcursionId",
                principalTable: "Excursions",
                principalColumn: "ExcursionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Excursion_ExcursionTypes_ExcursionTypeId",
                table: "Excursions",
                column: "ExcursionTypeId",
                principalTable: "ExcursionTypes",
                principalColumn: "ExcursionTypeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.RenameIndex(
                name: "IX_Excursions_ExcursionTypeId",
                table: "Excursions",
                newName: "IX_Excursion_ExcursionTypeId");

            migrationBuilder.RenameTable(
                name: "Excursions",
                newName: "Excursion");
        }
    }
}
