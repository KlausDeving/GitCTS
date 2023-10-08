using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CTS.Kanban.Migrations
{
    /// <inheritdoc />
    public partial class StatusAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusType",
                table: "KanbanCards",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusType",
                table: "KanbanCards");
        }
    }
}
