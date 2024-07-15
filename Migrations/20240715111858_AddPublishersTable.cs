using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore1.Migrations
{
    /// <inheritdoc />
    public partial class AddPublishersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Bookz",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "Publisher",
                table: "Bookz",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Publishers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Publishers");

            migrationBuilder.DropColumn(
                name: "Publisher",
                table: "Bookz");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Bookz",
                newName: "ID");
        }
    }
}
