using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceApresVenteApp.Migrations
{
    /// <inheritdoc />
    public partial class ReclamationFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Interventions_ReclamationId",
                table: "Interventions");

            migrationBuilder.CreateIndex(
                name: "IX_Interventions_ReclamationId",
                table: "Interventions",
                column: "ReclamationId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Interventions_ReclamationId",
                table: "Interventions");

            migrationBuilder.CreateIndex(
                name: "IX_Interventions_ReclamationId",
                table: "Interventions",
                column: "ReclamationId");
        }
    }
}
