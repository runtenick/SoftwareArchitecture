using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EFChampions.Migrations
{
    /// <inheritdoc />
    public partial class skinsMgr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Champions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Class = table.Column<int>(type: "INTEGER", nullable: false),
                    Icon = table.Column<string>(type: "TEXT", nullable: true),
                    Bio = table.Column<string>(type: "TEXT", nullable: true),
                    Image = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Champions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Skins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Price = table.Column<float>(type: "REAL", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Image = table.Column<string>(type: "TEXT", nullable: true),
                    Icon = table.Column<string>(type: "TEXT", nullable: true),
                    ChampionForeignKey = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Skins_Champions_ChampionForeignKey",
                        column: x => x.ChampionForeignKey,
                        principalTable: "Champions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Champions",
                columns: new[] { "Id", "Bio", "Class", "Icon", "Image", "Name" },
                values: new object[,]
                {
                    { 1, null, 1, null, null, "Akali" },
                    { 2, null, 2, null, null, "Aatrox" }
                });

            migrationBuilder.InsertData(
                table: "Skins",
                columns: new[] { "Id", "ChampionForeignKey", "Description", "Icon", "Image", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 1, null, null, null, "Skin1", null },
                    { 2, 1, null, null, null, "Skin2", null },
                    { 3, 2, null, null, null, "Skin3", null },
                    { 4, 2, null, null, null, "Skin4", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Skins_ChampionForeignKey",
                table: "Skins",
                column: "ChampionForeignKey");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Skins");

            migrationBuilder.DropTable(
                name: "Champions");
        }
    }
}
