using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PublicApiExercise.Data;
using PublicApiExercise.Models;

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
