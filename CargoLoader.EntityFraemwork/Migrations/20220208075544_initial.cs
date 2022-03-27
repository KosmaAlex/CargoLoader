using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CargoLoader.EntityFraemwork.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Containers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Marking = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Width = table.Column<double>(type: "float", nullable: false),
                    Height = table.Column<double>(type: "float", nullable: false),
                    Length = table.Column<double>(type: "float", nullable: false),
                    Volume = table.Column<double>(type: "float", nullable: true, computedColumnSql: "Length * Height * Width"),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    IsFragile = table.Column<bool>(type: "bit", nullable: false),
                    IsRotatable = table.Column<bool>(type: "bit", nullable: false),
                    IsProp = table.Column<bool>(type: "bit", nullable: false),
                    IsContainer = table.Column<bool>(type: "bit", nullable: false),
                    Capacity = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Containers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Marking = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Width = table.Column<double>(type: "float", nullable: false),
                    Height = table.Column<double>(type: "float", nullable: false),
                    Length = table.Column<double>(type: "float", nullable: false),
                    Volume = table.Column<double>(type: "float", nullable: true, computedColumnSql: "Length * Height * Width"),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    IsFragile = table.Column<bool>(type: "bit", nullable: false),
                    IsRotatable = table.Column<bool>(type: "bit", nullable: false),
                    IsProp = table.Column<bool>(type: "bit", nullable: false),
                    IsContainer = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LicensePlate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VolumeCapacity = table.Column<double>(type: "float", nullable: false),
                    WeightCapacity = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cargo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Marking = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Width = table.Column<double>(type: "float", nullable: false),
                    Height = table.Column<double>(type: "float", nullable: false),
                    Length = table.Column<double>(type: "float", nullable: false),
                    Volume = table.Column<double>(type: "float", nullable: true, computedColumnSql: "Length * Height * Width"),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    IsFragile = table.Column<bool>(type: "bit", nullable: false),
                    IsRotatable = table.Column<bool>(type: "bit", nullable: false),
                    IsProp = table.Column<bool>(type: "bit", nullable: false),
                    IsContainer = table.Column<bool>(type: "bit", nullable: false),
                    Capacity = table.Column<double>(type: "float", nullable: true),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ContainerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cargo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cargo_Cargo_ContainerId",
                        column: x => x.ContainerId,
                        principalTable: "Cargo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cargo_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cargo_ContainerId",
                table: "Cargo",
                column: "ContainerId");

            migrationBuilder.CreateIndex(
                name: "IX_Cargo_OrderId",
                table: "Cargo",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Containers_Marking",
                table: "Containers",
                column: "Marking",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderNumber",
                table: "Orders",
                column: "OrderNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_Marking",
                table: "Products",
                column: "Marking",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cargo");

            migrationBuilder.DropTable(
                name: "Containers");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Transports");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
