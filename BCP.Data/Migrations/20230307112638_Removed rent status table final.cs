using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BCP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Removedrentstatustablefinal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_rent_bike_bike_id",
                table: "rent");

            migrationBuilder.DropForeignKey(
                name: "fk_rent_bike_rental_status_status_id",
                table: "rent");

            migrationBuilder.DropForeignKey(
                name: "fk_rent_user_user_id",
                table: "rent");

            migrationBuilder.DropTable(
                name: "bike_rental_status");

            migrationBuilder.DropPrimaryKey(
                name: "pk_rent",
                table: "rent");

            migrationBuilder.DropIndex(
                name: "ix_rent_status_id",
                table: "rent");

            migrationBuilder.DropColumn(
                name: "status_id",
                table: "rent");

            migrationBuilder.RenameTable(
                name: "rent",
                newName: "rental_entries");

            migrationBuilder.RenameIndex(
                name: "ix_rent_user_id",
                table: "rental_entries",
                newName: "ix_rental_entries_user_id");

            migrationBuilder.RenameIndex(
                name: "ix_rent_bike_id",
                table: "rental_entries",
                newName: "ix_rental_entries_bike_id");

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "rental_entries",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "pk_rental_entries",
                table: "rental_entries",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_rental_entries_bikes_bike_id",
                table: "rental_entries",
                column: "bike_id",
                principalTable: "Bikes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_rental_entries_users_user_id",
                table: "rental_entries",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_rental_entries_bikes_bike_id",
                table: "rental_entries");

            migrationBuilder.DropForeignKey(
                name: "fk_rental_entries_users_user_id",
                table: "rental_entries");

            migrationBuilder.DropPrimaryKey(
                name: "pk_rental_entries",
                table: "rental_entries");

            migrationBuilder.DropColumn(
                name: "status",
                table: "rental_entries");

            migrationBuilder.RenameTable(
                name: "rental_entries",
                newName: "rent");

            migrationBuilder.RenameIndex(
                name: "ix_rental_entries_user_id",
                table: "rent",
                newName: "ix_rent_user_id");

            migrationBuilder.RenameIndex(
                name: "ix_rental_entries_bike_id",
                table: "rent",
                newName: "ix_rent_bike_id");

            migrationBuilder.AddColumn<int>(
                name: "status_id",
                table: "rent",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "pk_rent",
                table: "rent",
                column: "id");

            migrationBuilder.CreateTable(
                name: "bike_rental_status",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_bike_rental_status", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_rent_status_id",
                table: "rent",
                column: "status_id");

            migrationBuilder.AddForeignKey(
                name: "fk_rent_bike_bike_id",
                table: "rent",
                column: "bike_id",
                principalTable: "Bikes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_rent_bike_rental_status_status_id",
                table: "rent",
                column: "status_id",
                principalTable: "bike_rental_status",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_rent_user_user_id",
                table: "rent",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
