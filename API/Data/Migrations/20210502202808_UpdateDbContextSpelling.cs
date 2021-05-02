using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class UpdateDbContextSpelling : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Brand_BrandId",
                table: "Vehicles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Brand",
                table: "Brand");

            migrationBuilder.RenameTable(
                name: "Brand",
                newName: "Brands");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Brands",
                table: "Brands",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Brands_BrandId",
                table: "Vehicles",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Brands_BrandId",
                table: "Vehicles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Brands",
                table: "Brands");

            migrationBuilder.RenameTable(
                name: "Brands",
                newName: "Brand");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Brand",
                table: "Brand",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Brand_BrandId",
                table: "Vehicles",
                column: "BrandId",
                principalTable: "Brand",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
