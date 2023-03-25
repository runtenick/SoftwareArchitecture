using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EFChampions.Migrations
{
    /// <inheritdoc />
    public partial class stubbedCtx : Migration
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
                name: "RunePages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RunePages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Runes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    RuneFamily = table.Column<int>(type: "INTEGER", nullable: false),
                    Icon = table.Column<string>(type: "TEXT", nullable: true),
                    Image = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Runes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Skill",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    SkillType = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skill", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Skins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Price = table.Column<float>(type: "REAL", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "ChampionEntityRunePageEntity",
                columns: table => new
                {
                    ChampionsId = table.Column<int>(type: "INTEGER", nullable: false),
                    PagesRuneId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChampionEntityRunePageEntity", x => new { x.ChampionsId, x.PagesRuneId });
                    table.ForeignKey(
                        name: "FK_ChampionEntityRunePageEntity_Champions_ChampionsId",
                        column: x => x.ChampionsId,
                        principalTable: "Champions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChampionEntityRunePageEntity_RunePages_PagesRuneId",
                        column: x => x.PagesRuneId,
                        principalTable: "RunePages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RuneEntityRunePageEntity",
                columns: table => new
                {
                    PagesId = table.Column<int>(type: "INTEGER", nullable: false),
                    RunesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RuneEntityRunePageEntity", x => new { x.PagesId, x.RunesId });
                    table.ForeignKey(
                        name: "FK_RuneEntityRunePageEntity_RunePages_PagesId",
                        column: x => x.PagesId,
                        principalTable: "RunePages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RuneEntityRunePageEntity_Runes_RunesId",
                        column: x => x.RunesId,
                        principalTable: "Runes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChampionEntitySkillEntity",
                columns: table => new
                {
                    ChampionsId = table.Column<int>(type: "INTEGER", nullable: false),
                    SkillsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChampionEntitySkillEntity", x => new { x.ChampionsId, x.SkillsId });
                    table.ForeignKey(
                        name: "FK_ChampionEntitySkillEntity_Champions_ChampionsId",
                        column: x => x.ChampionsId,
                        principalTable: "Champions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChampionEntitySkillEntity_Skill_SkillsId",
                        column: x => x.SkillsId,
                        principalTable: "Skill",
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
                    { 1, 1, null, null, null, "Aiguillon", 650f },
                    { 2, 1, null, null, null, "All-Star", 1050f },
                    { 3, 2, null, null, null, "Justicer", 975f },
                    { 4, 2, null, null, null, "Mecha Aatrox", 1350f }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChampionEntityRunePageEntity_PagesRuneId",
                table: "ChampionEntityRunePageEntity",
                column: "PagesRuneId");

            migrationBuilder.CreateIndex(
                name: "IX_ChampionEntitySkillEntity_SkillsId",
                table: "ChampionEntitySkillEntity",
                column: "SkillsId");

            migrationBuilder.CreateIndex(
                name: "IX_RuneEntityRunePageEntity_RunesId",
                table: "RuneEntityRunePageEntity",
                column: "RunesId");

            migrationBuilder.CreateIndex(
                name: "IX_Skins_ChampionForeignKey",
                table: "Skins",
                column: "ChampionForeignKey");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChampionEntityRunePageEntity");

            migrationBuilder.DropTable(
                name: "ChampionEntitySkillEntity");

            migrationBuilder.DropTable(
                name: "RuneEntityRunePageEntity");

            migrationBuilder.DropTable(
                name: "Skins");

            migrationBuilder.DropTable(
                name: "Skill");

            migrationBuilder.DropTable(
                name: "RunePages");

            migrationBuilder.DropTable(
                name: "Runes");

            migrationBuilder.DropTable(
                name: "Champions");
        }
    }
}
