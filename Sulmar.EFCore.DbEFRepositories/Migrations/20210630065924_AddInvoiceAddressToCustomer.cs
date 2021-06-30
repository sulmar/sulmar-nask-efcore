using Microsoft.EntityFrameworkCore.Migrations;

namespace Sulmar.EFCore.DbEFRepositories.Migrations
{
    public partial class AddInvoiceAddressToCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InvoiceAddress_City",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InvoiceAddress_Country",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InvoiceAddress_Street",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InvoiceAddress_ZipCode",
                table: "Customers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvoiceAddress_City",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "InvoiceAddress_Country",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "InvoiceAddress_Street",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "InvoiceAddress_ZipCode",
                table: "Customers");
        }
    }
}
