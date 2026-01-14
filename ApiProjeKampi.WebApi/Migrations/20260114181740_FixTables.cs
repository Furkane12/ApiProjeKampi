using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiProjeKampi.WebApi.Migrations
{
    public partial class FixTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProducyId",
                table: "Products",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "Emaail",
                table: "Messages",
                newName: "Email");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Products",
                newName: "ProducyId");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Messages",
                newName: "Emaail");
        }
    }
}
