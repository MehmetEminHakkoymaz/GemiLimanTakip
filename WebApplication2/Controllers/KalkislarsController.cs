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
    public class KalkislarsController : Controller
    {
        private readonly IpfinalContext _context;

        public KalkislarsController(IpfinalContext context)
        {
            _context = context;
        }

        // GET: Kalkislars
        public async Task<IActionResult> Index()
        {
            var kalkislar = _context.Kalkislars.Include(k => k.Gemi).Include(k => k.Liman);
            return View(await kalkislar.ToListAsync());
        }

        // GET: Kalkislars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kalkislar = await _context.Kalkislars
                .FirstOrDefaultAsync(m => m.KalkisId == id);
            if (kalkislar == null)
            {
                return NotFound();
            }

            return View(kalkislar);
        }

        // GET: Kalkislars/Create
        public IActionResult Create()
        {
            ViewBag.GemiId = new SelectList(_context.Gemilers, "GemiId", "GemiAdi");
            ViewBag.LimanId = new SelectList(_context.Limanlars, "LimanId", "LimanAdi");
            return View();
        }

        // POST: Kalkislars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KalkisId,GemiId,LimanId,KalkisZamani")] Kalkislar kalkislar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kalkislar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kalkislar);
        }

        // GET: Kalkislars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kalkislar = await _context.Kalkislars.FindAsync(id);
            if (kalkislar == null)
            {
                return NotFound();
            }
            return View(kalkislar);
        }

        // POST: Kalkislars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KalkisId,GemiId,LimanId,KalkisZamani")] Kalkislar kalkislar)
        {
            if (id != kalkislar.KalkisId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kalkislar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KalkislarExists(kalkislar.KalkisId))
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
            return View(kalkislar);
        }

        // GET: Kalkislars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kalkislar = await _context.Kalkislars
                .FirstOrDefaultAsync(m => m.KalkisId == id);
            if (kalkislar == null)
            {
                return NotFound();
            }

            return View(kalkislar);
        }

        // POST: Kalkislars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kalkislar = await _context.Kalkislars.FindAsync(id);
            if (kalkislar != null)
            {
                _context.Kalkislars.Remove(kalkislar);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KalkislarExists(int id)
        {
            return _context.Kalkislars.Any(e => e.KalkisId == id);
        }
    }
}
