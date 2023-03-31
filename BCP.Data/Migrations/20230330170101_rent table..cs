using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BCP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class renttable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "image_url",
                table: "rental_entries",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "image_url",
                table: "rental_entries");
        }
    }
}
