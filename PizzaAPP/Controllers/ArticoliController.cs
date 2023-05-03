using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PizzaAPP.Data;
using PizzaAPP.Models;

namespace PizzaAPP.Controllers
{
    public class ArticoliController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArticoliController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Articoli
        public async Task<IActionResult> Index()
        {
              return _context.Articolo != null ? 
                          View(await _context.Articolo.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Articolo'  is null.");
        }

        // GET: Articoli/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Articolo == null)
            {
                return NotFound();
            }

            var articolo = await _context.Articolo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (articolo == null)
            {
                return NotFound();
            }

            return View(articolo);
        }

        // GET: Articoli/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Articoli/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descrizione,Prezzo,Valuta,Giacenza")] Articolo articolo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(articolo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(articolo);
        }

        // GET: Articoli/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Articolo == null)
            {
                return NotFound();
            }

            var articolo = await _context.Articolo.FindAsync(id);
            if (articolo == null)
            {
                return NotFound();
            }
            return View(articolo);
        }

        // POST: Articoli/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descrizione,Prezzo,Valuta,Giacenza")] Articolo articolo)
        {
            if (id != articolo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(articolo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticoloExists(articolo.Id))
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
            return View(articolo);
        }

        // GET: Articoli/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Articolo == null)
            {
                return NotFound();
            }

            var articolo = await _context.Articolo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (articolo == null)
            {
                return NotFound();
            }

            return View(articolo);
        }

        // POST: Articoli/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Articolo == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Articolo'  is null.");
            }
            var articolo = await _context.Articolo.FindAsync(id);
            if (articolo != null)
            {
                _context.Articolo.Remove(articolo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticoloExists(int id)
        {
          return (_context.Articolo?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
