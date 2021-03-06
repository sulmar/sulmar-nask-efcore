using Microsoft.EntityFrameworkCore.Migrations;

namespace Sulmar.EFCore.DbEFRepositories.Migrations
{
    public partial class AddSizeToProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Size",
                schema: "dbo",
                table: "Items",
                nullable: true);

            migrationBuilder.Sql("UPDATE dbo.Items SET Size = 'XXL' WHERE Size is null");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Size",
                schema: "dbo",
                table: "Items");
        }
    }
}
