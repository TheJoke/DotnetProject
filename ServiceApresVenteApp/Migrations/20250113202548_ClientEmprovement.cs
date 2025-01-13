using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceApresVenteApp.Migrations
{
    /// <inheritdoc />
    public partial class ClientEmprovement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reclamations_Clients_ClientId",
                table: "Reclamations");

            migrationBuilder.DropIndex(
                name: "IX_Reclamations_ClientId",
                table: "Reclamations");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Reclamations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "Reclamations",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reclamations_ClientId",
                table: "Reclamations",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reclamations_Clients_ClientId",
                table: "Reclamations",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");
        }
    }
}
