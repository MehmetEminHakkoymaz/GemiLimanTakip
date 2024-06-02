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
    public class GemilersController : Controller
    {
        private readonly IpfinalContext _context;

        public GemilersController(IpfinalContext context)
        {
            _context = context;
        }

        // GET: Gemilers
        public async Task<IActionResult> Index(string searchString, string sortOrder)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.FlagSortParm = sortOrder == "Flag" ? "flag_desc" : "Flag";
            ViewBag.TonnageSortParm = sortOrder == "Tonnage" ? "tonnage_desc" : "Tonnage";

            var gemiler = from g in _context.Gemilers
                          select g;

            if (!String.IsNullOrEmpty(searchString))
            {
                gemiler = gemiler.Where(s => s.GemiAdi.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    gemiler = gemiler.OrderByDescending(s => s.GemiAdi);
                    break;
                case "Flag":
                    gemiler = gemiler.OrderBy(s => s.Bayrak);
                    break;
                case "flag_desc":
                    gemiler = gemiler.OrderByDescending(s => s.Bayrak);
                    break;
                case "Tonnage":
                    gemiler = gemiler.OrderBy(s => s.Tonaj);
                    break;
                case "tonnage_desc":
                    gemiler = gemiler.OrderByDescending(s => s.Tonaj);
                    break;
                default:
                    gemiler = gemiler.OrderBy(s => s.GemiAdi);
                    break;
            }

            return View(await gemiler.AsNoTracking().ToListAsync());
        }

        // GET: Gemilers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gemiler = await _context.Gemilers
                .FirstOrDefaultAsync(m => m.GemiId == id);
            if (gemiler == null)
            {
                return NotFound();
            }

            return View(gemiler);
        }

        // GET: Gemilers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Gemilers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GemiId,GemiAdi,Bayrak,Tonaj,LimanId")] Gemiler gemiler)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gemiler);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gemiler);
        }

        // GET: Gemilers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gemiler = await _context.Gemilers.FindAsync(id);
            if (gemiler == null)
            {
                return NotFound();
            }
            return View(gemiler);
        }

        // POST: Gemilers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GemiId,GemiAdi,Bayrak,Tonaj,LimanId")] Gemiler gemiler)
        {
            if (id != gemiler.GemiId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gemiler);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GemilerExists(gemiler.GemiId))
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
            return View(gemiler);
        }

        // GET: Gemilers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gemiler = await _context.Gemilers
                .FirstOrDefaultAsync(m => m.GemiId == id);
            if (gemiler == null)
            {
                return NotFound();
            }

            return View(gemiler);
        }

        // POST: Gemilers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gemiler = await _context.Gemilers.FindAsync(id);
            if (gemiler != null)
            {
                _context.Gemilers.Remove(gemiler);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GemilerExists(int id)
        {
            return _context.Gemilers.Any(e => e.GemiId == id);
        }
    }
}
