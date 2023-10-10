using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CTS.Kanban.Migrations
{
    /// <inheritdoc />
    public partial class ColumnFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KanbanColumnId",
                table: "KanbanCards",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "KanbanColumns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StatusType = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KanbanColumns", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KanbanCards_KanbanColumnId",
                table: "KanbanCards",
                column: "KanbanColumnId");

            migrationBuilder.AddForeignKey(
                name: "FK_KanbanCards_KanbanColumns_KanbanColumnId",
                table: "KanbanCards",
                column: "KanbanColumnId",
                principalTable: "KanbanColumns",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KanbanCards_KanbanColumns_KanbanColumnId",
                table: "KanbanCards");

            migrationBuilder.DropTable(
                name: "KanbanColumns");

            migrationBuilder.DropIndex(
                name: "IX_KanbanCards_KanbanColumnId",
                table: "KanbanCards");

            migrationBuilder.DropColumn(
                name: "KanbanColumnId",
                table: "KanbanCards");
        }
    }
}
