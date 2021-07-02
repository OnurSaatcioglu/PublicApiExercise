using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PublicApiExercise.Data.Migrations
{
    public partial class yeniDeneme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Muvi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TmdbMuviNo = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descrption = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PosterPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImdbId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsReleased = table.Column<bool>(type: "bit", nullable: false),
                    Rating = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Muvi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Janra",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TmdbJanraNo = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MuviId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Janra", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Janra_Muvi_MuviId",
                        column: x => x.MuviId,
                        principalTable: "Muvi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MecMuviJanra",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TmdbMuviNo = table.Column<int>(type: "int", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: true),
                    TmdbJanraNo = table.Column<int>(type: "int", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: true),
                    MuviId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MecMuviJanra", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MecMuviJanra_Genre_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MecMuviJanra_Movie_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MecMuviJanra_Muvi_MuviId",
                        column: x => x.MuviId,
                        principalTable: "Muvi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Janra_MuviId",
                table: "Janra",
                column: "MuviId");

            migrationBuilder.CreateIndex(
                name: "IX_MecMuviJanra_GenreId",
                table: "MecMuviJanra",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_MecMuviJanra_MovieId",
                table: "MecMuviJanra",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_MecMuviJanra_MuviId",
                table: "MecMuviJanra",
                column: "MuviId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Janra");

            migrationBuilder.DropTable(
                name: "MecMuviJanra");

            migrationBuilder.DropTable(
                name: "Muvi");
        }
    }
}
