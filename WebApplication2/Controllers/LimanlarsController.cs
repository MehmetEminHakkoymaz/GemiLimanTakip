using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class LimanlarsController : Controller
    {
        private readonly IpfinalContext _context;

        public LimanlarsController(IpfinalContext context)
        {
            _context = context;
        }

        // GET: Limanlars
        [HttpGet]
        public async Task<IActionResult> Index(string searchString, string sortOrder)
        {
            ViewBag.CurrentFilter = searchString;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.CitySortParm = sortOrder == "City" ? "city_desc" : "City";
            ViewBag.CountrySortParm = sortOrder == "Country" ? "country_desc" : "Country";

            var limanlar = from l in _context.Limanlars
                           select l;

            if (!String.IsNullOrEmpty(searchString))
            {
                limanlar = limanlar.Where(s => s.LimanAdi.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    limanlar = limanlar.OrderByDescending(s => s.LimanAdi);
                    break;
                case "City":
                    limanlar = limanlar.OrderBy(s => s.Sehir);
                    break;
                case "city_desc":
                    limanlar = limanlar.OrderByDescending(s => s.Sehir);
                    break;
                case "Country":
                    limanlar = limanlar.OrderBy(s => s.Ulke);
                    break;
                case "country_desc":
                    limanlar = limanlar.OrderByDescending(s => s.Ulke);
                    break;
                default:
                    limanlar = limanlar.OrderBy(s => s.LimanAdi);
                    break;
            }

            return View(await limanlar.AsNoTracking().ToListAsync());
        }


        // GET: Limanlars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var limanlar = await _context.Limanlars
                .FirstOrDefaultAsync(m => m.LimanId == id);
            if (limanlar == null)
            {
                return NotFound();
            }

            return View(limanlar);
        }

        // GET: Limanlars/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Limanlars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LimanId,LimanAdi,Sehir,Ulke")] Limanlar limanlar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(limanlar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(limanlar);
        }

        // GET: Limanlars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var limanlar = await _context.Limanlars.FindAsync(id);
            if (limanlar == null)
            {
                return NotFound();
            }
            return View(limanlar);
        }

        // POST: Limanlars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LimanId,LimanAdi,Sehir,Ulke")] Limanlar limanlar)
        {
            if (id != limanlar.LimanId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(limanlar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LimanlarExists(limanlar.LimanId))
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
            return View(limanlar);
        }

        // GET: Limanlars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var limanlar = await _context.Limanlars
                .FirstOrDefaultAsync(m => m.LimanId == id);
            if (limanlar == null)
            {
                return NotFound();
            }

            return View(limanlar);
        }

        // POST: Limanlars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var limanlar = await _context.Limanlars.FindAsync(id);
            if (limanlar != null)
            {
                _context.Limanlars.Remove(limanlar);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LimanlarExists(int id)
        {
            return _context.Limanlars.Any(e => e.LimanId == id);
        }
    }
}
