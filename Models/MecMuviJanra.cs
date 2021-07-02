using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicApiExercise.Models
{
    public class MecMuviJanra
    {

        public int Id { get; set; }

        public int TmdbMuviNo { get; set; }

        public virtual Movie Movie { get; set; }

        public int TmdbJanraNo { get; set; }

        public virtual Genre Genre { get; set; }


    }
}
