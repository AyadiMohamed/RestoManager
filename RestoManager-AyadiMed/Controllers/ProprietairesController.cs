using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestoManager_AyadiMed.Models.RestosModel;

namespace RestoManager_AyadiMed.Controllers
{
    public class ProprietairesController : Controller
    {
        private readonly RestoDbContext _context;

        public ProprietairesController(RestoDbContext context)
        {
            _context = context;
        }

        // GET: Proprietaires
        public async Task<IActionResult> Index()
        {
              return _context.proprietaires != null ? 
                          View(await _context.proprietaires.ToListAsync()) :
                          Problem("Entity set 'RestoDbContext.proprietaires'  is null.");
        }

        // GET: Proprietaires/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.proprietaires == null)
            {
                return NotFound();
            }

            var proprietaires = await _context.proprietaires
                .FirstOrDefaultAsync(m => m.Numero == id);
            if (proprietaires == null)
            {
                return NotFound();
            }

            return View(proprietaires);
        }

        // GET: Proprietaires/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Proprietaires/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Numero,Nom,Email,Gsm")] Proprietaires proprietaires)
        {
           // if (ModelState.IsValid)
            //{
                _context.Add(proprietaires);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
           // }
            return View(proprietaires);
        }

        // GET: Proprietaires/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.proprietaires == null)
            {
                return NotFound();
            }

            var proprietaires = await _context.proprietaires.FindAsync(id);
            if (proprietaires == null)
            {
                return NotFound();
            }
            return View(proprietaires);
        }

        // POST: Proprietaires/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Numero,Nom,Email,Gsm")] Proprietaires proprietaires)
        {
            if (id != proprietaires.Numero)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proprietaires);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProprietairesExists(proprietaires.Numero))
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
            return View(proprietaires);
        }

        // GET: Proprietaires/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.proprietaires == null)
            {
                return NotFound();
            }

            var proprietaires = await _context.proprietaires
                .FirstOrDefaultAsync(m => m.Numero == id);
            if (proprietaires == null)
            {
                return NotFound();
            }

            return View(proprietaires);
        }

        // POST: Proprietaires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.proprietaires == null)
            {
                return Problem("Entity set 'RestoDbContext.proprietaires'  is null.");
            }
            var proprietaires = await _context.proprietaires.FindAsync(id);
            if (proprietaires != null)
            {
                _context.proprietaires.Remove(proprietaires);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProprietairesExists(int id)
        {
          return (_context.proprietaires?.Any(e => e.Numero == id)).GetValueOrDefault();
        }
    }
}
