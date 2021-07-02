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
    public class JanrasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JanrasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Janras
        public async Task<IActionResult> Index()
        {
            return View(await _context.Janra.ToListAsync());
        }

        // GET: Janras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var janra = await _context.Janra
                .FirstOrDefaultAsync(m => m.Id == id);
            if (janra == null)
            {
                return NotFound();
            }

            return View(janra);
        }

        // GET: Janras/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Janras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TmdbJanraNo,Name")] Janra janra)
        {
            if (ModelState.IsValid)
            {
                _context.Add(janra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(janra);
        }

        // GET: Janras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var janra = await _context.Janra.FindAsync(id);
            if (janra == null)
            {
                return NotFound();
            }
            return View(janra);
        }

        // POST: Janras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TmdbJanraNo,Name")] Janra janra)
        {
            if (id != janra.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(janra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JanraExists(janra.Id))
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
            return View(janra);
        }

        // GET: Janras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var janra = await _context.Janra
                .FirstOrDefaultAsync(m => m.Id == id);
            if (janra == null)
            {
                return NotFound();
            }

            return View(janra);
        }

        // POST: Janras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var janra = await _context.Janra.FindAsync(id);
            _context.Janra.Remove(janra);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JanraExists(int id)
        {
            return _context.Janra.Any(e => e.Id == id);
        }
    }
}
