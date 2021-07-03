using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PublicApiExercise.Models
{
    public class Muvi
    {
        public int Id { get; set; }

        public int TmdbMuviNo { get; set; }

        public string Title { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        public string Descrption { get; set; }

        public string PosterPath { get; set; }

        public string ImdbId { get; set; }

        public bool IsReleased { get; set; }

        public float Rating { get; set; }

        public virtual List<MecMuviJanra> MecMuviJanras { get; set; }

        public virtual List<Janra> Janras { get; set; }

    }
}
