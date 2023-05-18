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
    public class AvisController : Controller
    {
        private readonly RestoDbContext _context;

        public AvisController(RestoDbContext context)
        {
            _context = context;
        }

        // GET: Avis
        public async Task<IActionResult> Index()
        {
            var restoDbContext = _context.avis.Include(a => a.leResto);
            return View(await restoDbContext.ToListAsync());
        }

        // GET: Avis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.avis == null)
            {
                return NotFound();
            }

            var avis = await _context.avis
                .Include(a => a.leResto)
                .FirstOrDefaultAsync(m => m.CodeAvis == id);
            if (avis == null)
            {
                return NotFound();
            }

            return View(avis);
        }

        // GET: Avis/Create
        public IActionResult Create()
        {
            ViewData["NumResto"] = new SelectList(_context.restaurants, "CodeResto", "NomResto");
            return View();
        }

        // POST: Avis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodeAvis,nomPersonne,Note,Commentaire,NumResto")] Avis avis)
        {
            //if (ModelState.IsValid)
            //{
                _context.Add(avis);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            ViewData["NumResto"] = new SelectList(_context.restaurants, "CodeResto", "NomResto", avis.NumResto);
            return View(avis);
        }

        // GET: Avis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.avis == null)
            {
                return NotFound();
            }

            var avis = await _context.avis.FindAsync(id);
            if (avis == null)
            {
                return NotFound();
            }
            ViewData["NumResto"] = new SelectList(_context.restaurants, "CodeResto", "NomResto", avis.NumResto);
            return View(avis);
        }

        // POST: Avis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CodeAvis,nomPersonne,Note,Commentaire,NumResto")] Avis avis)
        {
            if (id != avis.CodeAvis)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(avis);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AvisExists(avis.CodeAvis))
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
            ViewData["NumResto"] = new SelectList(_context.restaurants, "CodeResto", "NomResto", avis.NumResto);
            return View(avis);
        }

        // GET: Avis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.avis == null)
            {
                return NotFound();
            }

            var avis = await _context.avis
                .Include(a => a.leResto)
                .FirstOrDefaultAsync(m => m.CodeAvis == id);
            if (avis == null)
            {
                return NotFound();
            }

            return View(avis);
        }

        // POST: Avis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.avis == null)
            {
                return Problem("Entity set 'RestoDbContext.avis'  is null.");
            }
            var avis = await _context.avis.FindAsync(id);
            if (avis != null)
            {
                _context.avis.Remove(avis);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AvisExists(int id)
        {
          return (_context.avis?.Any(e => e.CodeAvis == id)).GetValueOrDefault();
        }
    }
}
