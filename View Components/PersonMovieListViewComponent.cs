using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicApiExercise.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicApiExercise.View_Components
{
    public class PersonMovieListViewComponent : ViewComponent
    {
        public ApplicationDbContext _dbcontext { get; }

        public PersonMovieListViewComponent(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? person = null)
        {
            var items = await _dbcontext.Actor.AsQueryable().Where(p => p.TmdbPersonNo == person).ToListAsync();
            return View(items);
        }
        
    }
}
