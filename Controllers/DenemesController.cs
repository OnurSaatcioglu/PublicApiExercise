using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PublicApiExercise.Data;
using PublicApiExercise.Models;
using Newtonsoft;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace PublicApiExercise.Controllers
{
    public class DenemesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DenemesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Denemes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Deneme.ToListAsync());
        }

        // GET: Denemes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deneme = await _context.Deneme
                .FirstOrDefaultAsync(m => m.Id == id);

            string Url = "https://api.themoviedb.org/3/movie/" + id + "?api_key=1e467d81ad561e2cbf2f23427a0095b6";

            string jsonString = new WebClient().DownloadString(Url);

            //string jsonString = @"{""genres"":[{""id"":28,""name"":""Action""},{""id"":12,""name"":""Adventure""},{""id"":16,""name"":""Animation""},{""id"":35,""name"":""Comedy""},{""id"":80,""name"":""Crime""},{""id"":99,""name"":""Documentary""},{""id"":18,""name"":""Drama""},{""id"":10751,""name"":""Family""},{""id"":14,""name"":""Fantasy""},{""id"":36,""name"":""History""},{""id"":27,""name"":""Horror""},{""id"":10402,""name"":""Music""},{""id"":9648,""name"":""Mystery""},{""id"":10749,""name"":""Romance""},{""id"":878,""name"":""Science Fiction""},{""id"":10770,""name"":""TV Movie""},{""id"":53,""name"":""Thriller""},{""id"":10752,""name"":""War""},{""id"":37,""name"":""Western""}]}";

            //JArray movie = JArray.Parse(jsonString);

            dynamic data = JObject.Parse(jsonString);

            deneme.Deger = data.genres[1].name;


            ///deneme.Deger = 



            if (deneme == null)
            {
                return NotFound();
            }

            return View(deneme);
        }

        // GET: Denemes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Denemes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Deger")] Deneme deneme)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deneme);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(deneme);
        }

        // GET: Denemes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deneme = await _context.Deneme.FindAsync(id);
            if (deneme == null)
            {
                return NotFound();
            }
            return View(deneme);
        }

        // POST: Denemes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Deger")] Deneme deneme)
        {
            if (id != deneme.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deneme);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DenemeExists(deneme.Id))
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
            return View(deneme);
        }

        // GET: Denemes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deneme = await _context.Deneme
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deneme == null)
            {
                return NotFound();
            }

            return View(deneme);
        }

        // POST: Denemes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deneme = await _context.Deneme.FindAsync(id);
            _context.Deneme.Remove(deneme);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DenemeExists(int id)
        {
            return _context.Deneme.Any(e => e.Id == id);
        }
    }
}
