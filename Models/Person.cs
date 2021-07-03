using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicApiExercise.Models
{
    public class Person
    {
        public int Id { get; set; }

        public int TmdbPersonNo { get; set; }

        public string Name { get; set; }

        public string PicturePath { get; set; }

        public DateTime BirthDay { get; set; }

        public DateTime? DeathDay { get; set; }

        public string PlaceOfBirth { get; set; }

        public virtual List<Actor> Actor { get; set; }

        public virtual List<Director> Director { get; set; }



    }
}
