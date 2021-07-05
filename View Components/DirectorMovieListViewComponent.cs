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

        public ApplicationDbContext _dbcontext { get; }

        public DirectorMovieListViewComponent(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<IViewComponentResult> InvokeAsync(int person)
        {

            var items = await _dbcontext.Director.AsQueryable().Where(p => p.TmdbPersonNo == person).ToListAsync();
            
            if(items.Count == 0)
            {
                return Content("Yok");
            }
            return View(items);

        }
    }
}
