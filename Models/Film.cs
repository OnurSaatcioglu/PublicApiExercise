using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicApiExercise.Models
{
    public class Film
    {

        public int Id { get; set; }

        public int TmdbId { get; set; }

        public string Title { get; set; }

        public DateTime ReleaseDate { get; set; }

        public virtual List<Genre> Genres { get; set; }

        public string Descrption { get; set; }

        public string PosterPath { get; set; }

        public string ImdbId { get; set; }

        public bool IsReleased { get; set; }

        public float Rating { get; set; }

        public virtual List<MatchMovieGenre> MatchMovieGenres { get; set; }

    }
}
