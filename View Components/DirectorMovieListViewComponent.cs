using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicApiExercise.Data;
using PublicApiExercise.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicApiExercise.View_Components
{
    public class DirectorMovieListViewComponent : ViewComponent
    {

        public ApplicationDbContext _context { get; }

        public DirectorMovieListViewComponent(ApplicationDbContext dbcontext)
        {
            _context = dbcontext;
        }

        public async Task<IViewComponentResult> InvokeAsync(int person)
        {
            var films = _context.Actor.AsQueryable().Where(t => t.TmdbPersonNo == person).Select(p => p.TmdbMuviNo);

            if (films == null)
            {
                return Content("Yok");
            }
            
            List<Muvi> muvis = new List<Muvi>();

            foreach (int item in films)
            {

                Muvi muv = _context.Muvi.FirstOrDefault(t => t.TmdbMuviNo == item);

                if(muv != null)
                {
                    muvis.Add(muv);
                }

            }

            if (muvis.Count == 0)
            {
                return Content("Yok");
            }

            return View(muvis);
            /*var items = await _context.Director.AsQueryable().Where(p => p.TmdbPersonNo == person).ToListAsync();

            if (items.Count == 0)
            {
                return Content("Yok");
            }
            return View(items);*/

        }
    }
}
