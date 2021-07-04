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
    public class PeopleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PeopleController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: People
        public async Task<IActionResult> Index()
        {

            //var result = await _context.Person.ToListAsync();

            var para = _context.Actor.AsQueryable().Where(t => t.TmdbMuviNo == 95).Select(p => p.TmdbPersonNo);

            //para.

            List<Person> ppl = new List<Person>();

            foreach(int item in para){

                Person peo = _context.Person.FirstOrDefault(t => t.TmdbPersonNo == item);

                ppl.Add(peo);

            }

            


            //AsQueryable().Where(t => t.TmdbPersonNo == para.)
            //var result = await _context.Person.ToListAsync();

            return View(ppl);
        }

        // GET: People/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Person
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: People/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TmdbPersonNo,Name,PicturePath,BirthDay,DeathDay,PlaceOfBirth")] Person person)
        {
            if (ModelState.IsValid)
            {

                string DetailsUrl = "https://api.themoviedb.org/3/person/" + person.TmdbPersonNo + "?api_key=1e467d81ad561e2cbf2f23427a0095b6";
                string jsonString = new WebClient().DownloadString(DetailsUrl);
                dynamic data = JObject.Parse(jsonString);

                string CreditsUrl = "https://api.themoviedb.org/3/person/" + person.TmdbPersonNo + "/movie_credits?api_key=1e467d81ad561e2cbf2f23427a0095b6";
                string jsonString2 = new WebClient().DownloadString(CreditsUrl);
                dynamic creditsData = JObject.Parse(jsonString2);

                if(creditsData.cast.HasValues == true)
                {
                    for (int i = 0; i < creditsData.cast.Count; i++)
                    {
                        Actor aktor = new Actor();
                        aktor.TmdbPersonNo = person.TmdbPersonNo;
                        aktor.TmdbMuviNo = creditsData.cast[i].id;

                        _context.Add(aktor);
                    }
                }

                if (creditsData.crew.HasValues == true)
                {
                    for (int i = 0; i < creditsData.crew.Count; i++)
                    {
                        if(creditsData.crew[i].job == "Director")
                        {
                            Director yonetmen = new Director();
                            yonetmen.TmdbPersonNo = person.TmdbPersonNo;
                            yonetmen.TmdbMuviNo = creditsData.crew[i].id;

                            _context.Add(yonetmen);
                        }
                    }
                }

                person.Name = data.name;

                if (data.birthday != null)
                {
                    person.BirthDay = Convert.ToDateTime(data.birthday);
                }               
                if(data.deathday != null)
                {
                    person.DeathDay = Convert.ToDateTime(data.deathday);
                }
                if (data.place_of_birth != null)
                {
                    person.PlaceOfBirth = data.place_of_birth;
                }
                if (data.profile_path != null)
                {
                    person.PicturePath = data.profile_path;
                }
                
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: People/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Person.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TmdbPersonNo,Name,PicturePath,BirthDay,DeathDay,PlaceOfBirth")] Person person)
        {
            if (id != person.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.Id))
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
            return View(person);
        }

        // GET: People/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Person
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var person = await _context.Person.FindAsync(id);

            int realId = person.TmdbPersonNo;
            var actorList = _context.Actor.Where(t => t.TmdbPersonNo == realId);
            
            if(actorList != null)
            {
                foreach (var item in actorList)
                {
                    _context.Actor.Remove(item);
                }
            }


            _context.Person.Remove(person);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));




        }

        private bool PersonExists(int id)
        {
            return _context.Person.Any(e => e.Id == id);
        }
    }
}
