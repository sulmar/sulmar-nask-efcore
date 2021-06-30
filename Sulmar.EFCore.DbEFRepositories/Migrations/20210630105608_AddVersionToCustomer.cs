using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sulmar.EFCore.DbEFRepositories.Migrations
{
    public partial class AddVersionToCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Version",
                table: "Customers",
                rowVersion: true,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Version",
                table: "Customers");
        }
    }
}
