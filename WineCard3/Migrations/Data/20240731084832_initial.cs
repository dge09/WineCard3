using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WineCard3.Migrations.Data
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardDscp = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Origins",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OriginName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Origins", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Styles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StyleDscp = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Styles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Wines",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WineName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    StyleID = table.Column<int>(type: "int", nullable: false),
                    SweetnessLevel = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<float>(type: "real", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    OriginID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wines", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Wines_Origins_OriginID",
                        column: x => x.OriginID,
                        principalTable: "Origins",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Wines_Styles_StyleID",
                        column: x => x.StyleID,
                        principalTable: "Styles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CardWine",
                columns: table => new
                {
                    CardsID = table.Column<int>(type: "int", nullable: false),
                    WinesID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardWine", x => new { x.CardsID, x.WinesID });
                    table.ForeignKey(
                        name: "FK_CardWine_Cards_CardsID",
                        column: x => x.CardsID,
                        principalTable: "Cards",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CardWine_Wines_WinesID",
                        column: x => x.WinesID,
                        principalTable: "Wines",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CardWine_WinesID",
                table: "CardWine",
                column: "WinesID");

            migrationBuilder.CreateIndex(
                name: "IX_Wines_OriginID",
                table: "Wines",
                column: "OriginID");

            migrationBuilder.CreateIndex(
                name: "IX_Wines_StyleID",
                table: "Wines",
                column: "StyleID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CardWine");

            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "Wines");

            migrationBuilder.DropTable(
                name: "Origins");

            migrationBuilder.DropTable(
                name: "Styles");
        }
    }
}
