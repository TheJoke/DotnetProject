using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceApresVenteApp.Migrations
{
    /// <inheritdoc />
    public partial class UnifyingClientAndResponsable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Clients_ClientId1",
                table: "Articles");

            migrationBuilder.DropTable(
                name: "Responsables");

            migrationBuilder.RenameColumn(
                name: "ClientId1",
                table: "Articles",
                newName: "UserId1");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "Articles",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Articles_ClientId1",
                table: "Articles",
                newName: "IX_Articles_UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Clients_UserId1",
                table: "Articles",
                column: "UserId1",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Clients_UserId1",
                table: "Articles");

            migrationBuilder.RenameColumn(
                name: "UserId1",
                table: "Articles",
                newName: "ClientId1");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Articles",
                newName: "ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_Articles_UserId1",
                table: "Articles",
                newName: "IX_Articles_ClientId1");

            migrationBuilder.CreateTable(
                name: "Responsables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotDePasse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Responsables", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Clients_ClientId1",
                table: "Articles",
                column: "ClientId1",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
