using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Budgeteer.Plugins.EFDB.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    ExpenseId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ExpenseName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.ExpenseId);
                });

            migrationBuilder.CreateTable(
                name: "Entrys",
                columns: table => new
                {
                    EntryId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Content = table.Column<string>(type: "TEXT", nullable: false),
                    ExpenseId = table.Column<int>(type: "INTEGER", nullable: false),
                    PostExpenseId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entrys", x => x.EntryId);
                    table.ForeignKey(
                        name: "FK_Entrys_Expenses_PostExpenseId",
                        column: x => x.PostExpenseId,
                        principalTable: "Expenses",
                        principalColumn: "ExpenseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Entrys_PostExpenseId",
                table: "Entrys",
                column: "PostExpenseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Entrys");

            migrationBuilder.DropTable(
                name: "Expenses");
        }
    }
}
