using Microsoft.EntityFrameworkCore.Migrations;

namespace PublicApiExercise.Data.Migrations
{
    public partial class MatchMovieGenre_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MatchMovieGenre",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: false),
                    FilmId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchMovieGenre", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchMovieGenre_Film_FilmId",
                        column: x => x.FilmId,
                        principalTable: "Film",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MatchMovieGenre_Genre_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MatchMovieGenre_Movie_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MatchMovieGenre_FilmId",
                table: "MatchMovieGenre",
                column: "FilmId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchMovieGenre_GenreId",
                table: "MatchMovieGenre",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchMovieGenre_MovieId",
                table: "MatchMovieGenre",
                column: "MovieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MatchMovieGenre");
        }
    }
}
