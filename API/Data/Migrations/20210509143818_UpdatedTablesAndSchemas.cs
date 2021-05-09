using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class UpdatedTablesAndSchemas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_Brands_BrandId",
                schema: "Vehicles",
                table: "Vehicle");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_VehicleModels_ModelId",
                schema: "Vehicles",
                table: "Vehicle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VehicleModels",
                table: "VehicleModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Brands",
                table: "Brands");

            migrationBuilder.RenameTable(
                name: "VehicleModels",
                newName: "VehicleModel",
                newSchema: "Vehicles");

            migrationBuilder.RenameTable(
                name: "Brands",
                newName: "Manufacturer",
                newSchema: "Vehicles");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "Vehicles",
                table: "VehicleModel",
                type: "VARCHAR(80)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "Vehicles",
                table: "Manufacturer",
                type: "VARCHAR(80)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_VehicleModel",
                schema: "Vehicles",
                table: "VehicleModel",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Manufacturer",
                schema: "Vehicles",
                table: "Manufacturer",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_Manufacturer_BrandId",
                schema: "Vehicles",
                table: "Vehicle",
                column: "BrandId",
                principalSchema: "Vehicles",
                principalTable: "Manufacturer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_VehicleModel_ModelId",
                schema: "Vehicles",
                table: "Vehicle",
                column: "ModelId",
                principalSchema: "Vehicles",
                principalTable: "VehicleModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_Manufacturer_BrandId",
                schema: "Vehicles",
                table: "Vehicle");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_VehicleModel_ModelId",
                schema: "Vehicles",
                table: "Vehicle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VehicleModel",
                schema: "Vehicles",
                table: "VehicleModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Manufacturer",
                schema: "Vehicles",
                table: "Manufacturer");

            migrationBuilder.RenameTable(
                name: "VehicleModel",
                schema: "Vehicles",
                newName: "VehicleModels");

            migrationBuilder.RenameTable(
                name: "Manufacturer",
                schema: "Vehicles",
                newName: "Brands");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "VehicleModels",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(80)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Brands",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(80)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_VehicleModels",
                table: "VehicleModels",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Brands",
                table: "Brands",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_Brands_BrandId",
                schema: "Vehicles",
                table: "Vehicle",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_VehicleModels_ModelId",
                schema: "Vehicles",
                table: "Vehicle",
                column: "ModelId",
                principalTable: "VehicleModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
