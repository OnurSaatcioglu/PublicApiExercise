using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicApiExercise.Models
{
    public class Director
    {
        public int Id { get; set; }

        public int TmdbMuviNo { get; set; }

        public virtual Movie Movie { get; set; }

        public int TmdbPersonNo { get; set; }

        public virtual Person Person { get; set; }
    }
}
