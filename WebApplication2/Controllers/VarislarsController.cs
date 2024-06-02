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
    public class VarislarsController : Controller
    {
        private readonly IpfinalContext _context;

        public VarislarsController(IpfinalContext context)
        {
            _context = context;
        }

        // GET: Varislars
        public async Task<IActionResult> Index()
        {
            var varislar = _context.Varislars.Include(v => v.Gemi).Include(v => v.Liman);
            return View(await varislar.ToListAsync());
        }

        // GET: Varislars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var varislar = await _context.Varislars
                .FirstOrDefaultAsync(m => m.VarisId == id);
            if (varislar == null)
            {
                return NotFound();
            }

            return View(varislar);
        }

        // GET: Varislars/Create
        public IActionResult Create()
        {
            ViewBag.GemiId = new SelectList(_context.Gemilers, "GemiId", "GemiAdi");
            ViewBag.LimanId = new SelectList(_context.Limanlars, "LimanId", "LimanAdi");
            return View();
        }

        // POST: Varislars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VarisId,GemiId,LimanId,VarisZamani")] Varislar varislar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(varislar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(varislar);
        }

        // GET: Varislars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var varislar = await _context.Varislars.FindAsync(id);
            if (varislar == null)
            {
                return NotFound();
            }
            return View(varislar);
        }

        // POST: Varislars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VarisId,GemiId,LimanId,VarisZamani")] Varislar varislar)
        {
            if (id != varislar.VarisId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(varislar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VarislarExists(varislar.VarisId))
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
            return View(varislar);
        }

        // GET: Varislars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var varislar = await _context.Varislars
                .FirstOrDefaultAsync(m => m.VarisId == id);
            if (varislar == null)
            {
                return NotFound();
            }

            return View(varislar);
        }

        // POST: Varislars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var varislar = await _context.Varislars.FindAsync(id);
            if (varislar != null)
            {
                _context.Varislars.Remove(varislar);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VarislarExists(int id)
        {
            return _context.Varislars.Any(e => e.VarisId == id);
        }
    }
}
