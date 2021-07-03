using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using PublicApiExercise.Data;
using PublicApiExercise.Models;

namespace PublicApiExercise.Controllers
{
    public class MuvisController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MuvisController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Muvis
        public async Task<IActionResult> Index()
        {
            return View(await _context.Muvi.ToListAsync());
        }

        // GET: Muvis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var muvi = await _context.Muvi
                .FirstOrDefaultAsync(m => m.Id == id);
            if (muvi == null)
            {
                return NotFound();
            }

            return View(muvi);
        }

        // GET: Muvis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Muvis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TmdbMuviNo,Title,ReleaseDate,Descrption,PosterPath,ImdbId,IsReleased,Rating")] Muvi muvi)
        {
            if (ModelState.IsValid)
            {

                string Url = "https://api.themoviedb.org/3/movie/" + muvi.TmdbMuviNo + "?api_key=1e467d81ad561e2cbf2f23427a0095b6";
                string jsonString = new WebClient().DownloadString(Url);
                dynamic data = JObject.Parse(jsonString);

                for(int i=0; i < data.genres.Count; i++)
                {
                    MecMuviJanra tur = new MecMuviJanra();
                    tur.TmdbMuviNo = muvi.TmdbMuviNo;
                    tur.TmdbJanraNo = data.genres[i].id;

                    _context.Add(tur);
                }

                muvi.Title = data.title;
                muvi.ReleaseDate = Convert.ToDateTime(data.release_date);
                muvi.Rating = data.vote_average;

                if (data.overview != null)
                {
                    muvi.Descrption = data.overview;
                }
                if (data.poster_path != null)
                {
                    muvi.PosterPath = data.poster_path;
                }
                if (data.imdb_id != null)
                {
                    muvi.ImdbId = data.imdb_id;
                }

                _context.Add(muvi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(muvi);
        }

        // GET: Muvis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var muvi = await _context.Muvi.FindAsync(id);
            if (muvi == null)
            {
                return NotFound();
            }
            return View(muvi);
        }

        // POST: Muvis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TmdbMuviNo,Title,ReleaseDate,Descrption,PosterPath,ImdbId,IsReleased,Rating")] Muvi muvi)
        {
            if (id != muvi.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(muvi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MuviExists(muvi.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(muvi);
        }

        // GET: Muvis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var muvi = await _context.Muvi
                .FirstOrDefaultAsync(m => m.Id == id);
            if (muvi == null)
            {
                return NotFound();
            }

            return View(muvi);
        }

        // POST: Muvis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var muvi = await _context.Muvi.FindAsync(id);
            _context.Muvi.Remove(muvi);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MuviExists(int id)
        {
            return _context.Muvi.Any(e => e.Id == id);
        }
    }
}
