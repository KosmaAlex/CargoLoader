using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CargoLoader.EntityFraemwork.Migrations
{
    public partial class add_thumbnail_to_models : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Containers",
                newName: "Thumbnail");

            migrationBuilder.AddColumn<byte[]>(
                name: "Thumbnail",
                table: "Products",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Thumbnail",
                table: "Cargo",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Thumbnail",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Thumbnail",
                table: "Cargo");

            migrationBuilder.RenameColumn(
                name: "Thumbnail",
                table: "Containers",
                newName: "Image");
        }
    }
}
