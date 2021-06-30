using Microsoft.EntityFrameworkCore.Migrations;

namespace Sulmar.EFCore.DbEFRepositories.Migrations
{
    public partial class AddShipAddressToCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShipAddress_City",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShipAddress_Country",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShipAddress_Street",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShipAddress_ZipCode",
                table: "Customers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShipAddress_City",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "ShipAddress_Country",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "ShipAddress_Street",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "ShipAddress_ZipCode",
                table: "Customers");
        }
    }
}
