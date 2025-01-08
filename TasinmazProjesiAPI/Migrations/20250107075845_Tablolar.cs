using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace TasinmazProjesiAPI.Migrations
{
    public partial class Tablolar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "iller",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IlAdi = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_iller", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ilceler",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IlceAdi = table.Column<string>(nullable: true),
                    IlId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ilceler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ilceler_iller_IlId",
                        column: x => x.IlId,
                        principalTable: "iller",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "mahalleler",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MahalleAdi = table.Column<string>(nullable: true),
                    IlceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mahalleler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_mahalleler_ilceler_IlceId",
                        column: x => x.IlceId,
                        principalTable: "ilceler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tasinmazlar",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TasinmazIsim = table.Column<string>(nullable: true),
                    TasinmazParsel = table.Column<int>(nullable: false),
                    TasinmazNitelik = table.Column<string>(nullable: true),
                    TasinmazAdres = table.Column<string>(nullable: true),
                    MahalleId = table.Column<int>(nullable: false),
                    Ada = table.Column<string>(nullable: true),
                    KoordinatBilgisi = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tasinmazlar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tasinmazlar_mahalleler_MahalleId",
                        column: x => x.MahalleId,
                        principalTable: "mahalleler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ilceler_IlId",
                table: "ilceler",
                column: "IlId");

            migrationBuilder.CreateIndex(
                name: "IX_mahalleler_IlceId",
                table: "mahalleler",
                column: "IlceId");

            migrationBuilder.CreateIndex(
                name: "IX_tasinmazlar_MahalleId",
                table: "tasinmazlar",
                column: "MahalleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tasinmazlar");

            migrationBuilder.DropTable(
                name: "mahalleler");

            migrationBuilder.DropTable(
                name: "ilceler");

            migrationBuilder.DropTable(
                name: "iller");
        }
    }
}
