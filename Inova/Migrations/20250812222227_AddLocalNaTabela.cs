using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inova.Migrations
{
    /// <inheritdoc />
    public partial class AddLocalNaTabela : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Local",
                table: "Itens",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Local",
                table: "Itens");
        }
    }
}
