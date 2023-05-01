using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TrenRezervasyonAPI.Migrations
{
    /// <inheritdoc />
    public partial class ilk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RezervasyonCevaplari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RezervasyonOnayi = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RezervasyonCevaplari", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rezervasyonlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KisiSayisi = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rezervasyonlar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tren",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tren", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Oturmaplanlari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VagonAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KisiSayisi = table.Column<int>(type: "int", nullable: false),
                    RezervasyonCevabiId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oturmaplanlari", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Oturmaplanlari_RezervasyonCevaplari_RezervasyonCevabiId",
                        column: x => x.RezervasyonCevabiId,
                        principalTable: "RezervasyonCevaplari",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Vagonlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kapasite = table.Column<int>(type: "int", nullable: false),
                    DoluKoltukAdet = table.Column<int>(type: "int", nullable: false),
                    TrenId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vagonlar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vagonlar_Tren_TrenId",
                        column: x => x.TrenId,
                        principalTable: "Tren",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Tren",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Başkent" },
                    { 2, "İzmir" }
                });

            migrationBuilder.InsertData(
                table: "Vagonlar",
                columns: new[] { "Id", "DoluKoltukAdet", "Kapasite", "Name", "TrenId" },
                values: new object[,]
                {
                    { 1, 35, 80, "BaşkentV1", 1 },
                    { 2, 40, 100, "BaşkentV2", 1 },
                    { 3, 55, 60, "BaşkentV3", 1 },
                    { 4, 30, 70, "İzmirV1", 2 },
                    { 5, 25, 60, "İzmirV2", 2 },
                    { 6, 50, 90, "İzmirV3", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Oturmaplanlari_RezervasyonCevabiId",
                table: "Oturmaplanlari",
                column: "RezervasyonCevabiId");

            migrationBuilder.CreateIndex(
                name: "IX_Vagonlar_TrenId",
                table: "Vagonlar",
                column: "TrenId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Oturmaplanlari");

            migrationBuilder.DropTable(
                name: "Rezervasyonlar");

            migrationBuilder.DropTable(
                name: "Vagonlar");

            migrationBuilder.DropTable(
                name: "RezervasyonCevaplari");

            migrationBuilder.DropTable(
                name: "Tren");
        }
    }
}
