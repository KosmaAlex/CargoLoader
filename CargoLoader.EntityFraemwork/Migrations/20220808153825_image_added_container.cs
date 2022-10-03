using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CargoLoader.EntityFraemwork.Migrations
{
    public partial class image_added_container : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Containers",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Containers");
        }
    }
}
