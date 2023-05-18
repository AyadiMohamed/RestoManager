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
    public class RestaurantsController : Controller
    {
        private readonly RestoDbContext _context;

        public RestaurantsController(RestoDbContext context)
        {
            _context = context;
        }

        // GET: Restaurants
        public async Task<IActionResult> Index()
        {
            var restosDbContext = _context.restaurants.Include(r => r.LePropio).Include(r => r.LesAvis);
            return View(await restosDbContext.ToListAsync());
        }

        // GET: Restaurants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.restaurants == null)
            {
                return NotFound();
            }

            var restaurant = await _context.restaurants
                .Include(r => r.LePropio)
                .FirstOrDefaultAsync(m => m.CodeResto == id);
            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }

        // GET: Restaurants/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Restaurants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodeResto,NomResto,Specialite,Ville,Tel,NumProp")] Restaurant restaurant)
        {

            if (ModelState.IsValid)
            {
                _context.Add(restaurant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(restaurant);


        }

        // GET: Restaurants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.restaurants == null)
            {
                return NotFound();
            }

            var restaurant = await _context.restaurants.FindAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }
            return View(restaurant);
        }

        // POST: Restaurants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CodeResto,NomResto,Specialite,Ville,Tel,NumProp")] Restaurant restaurant)
        {
            if (id != restaurant.CodeResto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(restaurant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RestaurantExists(restaurant.CodeResto))
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
            return View(restaurant);
        }

        // GET: Restaurants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.restaurants == null)
            {
                return NotFound();
            }

            var restaurant = await _context.restaurants
                .Include(r => r.LePropio)
                .FirstOrDefaultAsync(m => m.CodeResto == id);
            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }

        // POST: Restaurants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.restaurants == null)
            {
                return Problem("Entity set 'RestosDbContext.Restaurants'  is null.");
            }
            var restaurant = await _context.restaurants.FindAsync(id);
            if (restaurant != null)
            {
                _context.restaurants.Remove(restaurant);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RestaurantExists(int id)
        {
            return (_context.restaurants?.Any(e => e.CodeResto == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> RestaurantAvis()
        {
            var res = _context.restaurants.Include(r => r.LesAvis).Include(r => r.LePropio);

            return View(await res.ToListAsync());
        }

        public async Task<IActionResult> AvisLink(int id)
        {

            var avis = from a in _context.avis.Include(a => a.leResto)
                       join r in _context.restaurants.Include(r => r.LesAvis).Include(r => r.LePropio)
                       on a.leResto.CodeResto equals r.CodeResto
                       where r.CodeResto == id
                       select a;

            return View(await avis.ToListAsync());
        }
        public async Task<IActionResult> AvisLinkNote()
        {
            var result = from r in _context.restaurants.Include(r => r.LesAvis).Include(r => r.LePropio)
                         join a in _context.avis.Include(a => a.leResto)
                         on r.CodeResto equals a.leResto.CodeResto
                         where a.Note > 3.5
                         select r;
            return View(await result.ToListAsync());
        }
    }
}
