using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PublicApiExercise.Models
{
    public class Person
    {
        public int Id { get; set; }

        public int TmdbPersonNo { get; set; }

        public string Name { get; set; }

        #nullable enable
        public string? PicturePath { get; set; }

        [DataType(DataType.Date)]
        public DateTime? BirthDay { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DeathDay { get; set; }

        public string? PlaceOfBirth { get; set; }

        #nullable disable
        public virtual List<Actor> Actor { get; set; }

        public virtual List<Director> Director { get; set; }



    }
}
