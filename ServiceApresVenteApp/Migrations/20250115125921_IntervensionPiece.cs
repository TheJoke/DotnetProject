using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceApresVenteApp.Migrations
{
    /// <inheritdoc />
    public partial class IntervensionPiece : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InterventionPieceDeRechange");

            migrationBuilder.CreateTable(
                name: "IntervensionPiece",
                columns: table => new
                {
                    InterventionId = table.Column<int>(type: "int", nullable: false),
                    PieceDeRechangeId = table.Column<int>(type: "int", nullable: false),
                    Quantite = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntervensionPiece", x => new { x.InterventionId, x.PieceDeRechangeId });
                    table.ForeignKey(
                        name: "FK_IntervensionPiece_Interventions_InterventionId",
                        column: x => x.InterventionId,
                        principalTable: "Interventions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IntervensionPiece_Pieces_PieceDeRechangeId",
                        column: x => x.PieceDeRechangeId,
                        principalTable: "Pieces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IntervensionPiece_PieceDeRechangeId",
                table: "IntervensionPiece",
                column: "PieceDeRechangeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IntervensionPiece");

            migrationBuilder.CreateTable(
                name: "InterventionPieceDeRechange",
                columns: table => new
                {
                    InterventionsId = table.Column<int>(type: "int", nullable: false),
                    PiecesUtiliseesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterventionPieceDeRechange", x => new { x.InterventionsId, x.PiecesUtiliseesId });
                    table.ForeignKey(
                        name: "FK_InterventionPieceDeRechange_Interventions_InterventionsId",
                        column: x => x.InterventionsId,
                        principalTable: "Interventions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InterventionPieceDeRechange_Pieces_PiecesUtiliseesId",
                        column: x => x.PiecesUtiliseesId,
                        principalTable: "Pieces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InterventionPieceDeRechange_PiecesUtiliseesId",
                table: "InterventionPieceDeRechange",
                column: "PiecesUtiliseesId");
        }
    }
}
