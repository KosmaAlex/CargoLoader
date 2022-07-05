using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CargoLoader.EntityFraemwork.Migrations
{
    public partial class remove_computed_volume_column : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Volume",
                table: "Products",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true,
                oldComputedColumnSql: "Length * Height * Width");

            migrationBuilder.AlterColumn<double>(
                name: "Volume",
                table: "Containers",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true,
                oldComputedColumnSql: "Length * Height * Width");

            migrationBuilder.AlterColumn<double>(
                name: "Volume",
                table: "Cargo",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true,
                oldComputedColumnSql: "Length * Height * Width");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Volume",
                table: "Products",
                type: "float",
                nullable: true,
                computedColumnSql: "Length * Height * Width",
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Volume",
                table: "Containers",
                type: "float",
                nullable: true,
                computedColumnSql: "Length * Height * Width",
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Volume",
                table: "Cargo",
                type: "float",
                nullable: true,
                computedColumnSql: "Length * Height * Width",
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);
        }
    }
}
