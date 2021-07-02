using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PublicApiExercise.Data.Migrations
{
    public partial class addedFilms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FilmId",
                table: "Genre",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Film",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TmdbId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_Film", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Genre_FilmId",
                table: "Genre",
                column: "FilmId");

            migrationBuilder.AddForeignKey(
                name: "FK_Genre_Film_FilmId",
                table: "Genre",
                column: "FilmId",
                principalTable: "Film",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Genre_Film_FilmId",
                table: "Genre");

            migrationBuilder.DropTable(
                name: "Film");

            migrationBuilder.DropIndex(
                name: "IX_Genre_FilmId",
                table: "Genre");

            migrationBuilder.DropColumn(
                name: "FilmId",
                table: "Genre");
        }
    }
}
