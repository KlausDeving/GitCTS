using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CTS.Kanban.Migrations
{
    /// <inheritdoc />
    public partial class ColumnFKVirtual : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KanbanCards_KanbanColumns_KanbanColumnId",
                table: "KanbanCards");

            migrationBuilder.AlterColumn<int>(
                name: "KanbanColumnId",
                table: "KanbanCards",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_KanbanCards_KanbanColumns_KanbanColumnId",
                table: "KanbanCards",
                column: "KanbanColumnId",
                principalTable: "KanbanColumns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KanbanCards_KanbanColumns_KanbanColumnId",
                table: "KanbanCards");

            migrationBuilder.AlterColumn<int>(
                name: "KanbanColumnId",
                table: "KanbanCards",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_KanbanCards_KanbanColumns_KanbanColumnId",
                table: "KanbanCards",
                column: "KanbanColumnId",
                principalTable: "KanbanColumns",
                principalColumn: "Id");
        }
    }
}
