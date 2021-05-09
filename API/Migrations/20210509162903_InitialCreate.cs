using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Vehicles");

            migrationBuilder.EnsureSchema(
                name: "Customers");

            migrationBuilder.CreateTable(
                name: "Manufacturer",
                schema: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "VARCHAR(80)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "VARCHAR(128)", nullable: true),
                    FirstName = table.Column<string>(type: "VARCHAR(30)", nullable: true),
                    LastName = table.Column<string>(type: "VARCHAR(40)", nullable: true),
                    City = table.Column<string>(type: "VARCHAR(50)", nullable: true),
                    Country = table.Column<string>(type: "VARCHAR(50)", nullable: true),
                    Email = table.Column<string>(type: "VARCHAR(128)", nullable: true),
                    Phone = table.Column<string>(type: "VARCHAR(15)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VehicleModel",
                schema: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "VARCHAR(80)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicle",
                schema: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegNum = table.Column<string>(type: "VARCHAR(10)", nullable: true),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    ModelId = table.Column<int>(type: "int", nullable: false),
                    ModelYear = table.Column<short>(type: "SMALLINT", nullable: false),
                    FuelType = table.Column<string>(type: "VARCHAR(15)", nullable: true),
                    GearType = table.Column<string>(type: "VARCHAR(40)", nullable: true),
                    Color = table.Column<string>(type: "VARCHAR(30)", nullable: true),
                    Mileage = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicle_Manufacturer_BrandId",
                        column: x => x.BrandId,
                        principalSchema: "Vehicles",
                        principalTable: "Manufacturer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vehicle_VehicleModel_ModelId",
                        column: x => x.ModelId,
                        principalSchema: "Vehicles",
                        principalTable: "VehicleModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_BrandId",
                schema: "Vehicles",
                table: "Vehicle",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_ModelId",
                schema: "Vehicles",
                table: "Vehicle",
                column: "ModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User",
                schema: "Customers");

            migrationBuilder.DropTable(
                name: "Vehicle",
                schema: "Vehicles");

            migrationBuilder.DropTable(
                name: "Manufacturer",
                schema: "Vehicles");

            migrationBuilder.DropTable(
                name: "VehicleModel",
                schema: "Vehicles");
        }
    }
}
