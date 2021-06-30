using Microsoft.EntityFrameworkCore.Migrations;

namespace Sulmar.EFCore.DbEFRepositories.Migrations
{
    public partial class AddAmountToCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "Customers",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Customers");
        }
    }
}
