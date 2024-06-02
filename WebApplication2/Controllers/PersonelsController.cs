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
    public class PersonelsController : Controller
    {
        private readonly IpfinalContext _context;

        public PersonelsController(IpfinalContext context)
        {
            _context = context;
        }

        // GET: Personels
        public async Task<IActionResult> Index(string sortOrder)
        {
            if (sortOrder != null && ViewBag.CurrentSort == sortOrder)
            {
                sortOrder = sortOrder.EndsWith("_desc") ? sortOrder.Substring(0, sortOrder.Length - 5) : sortOrder + "_desc";
            }

            ViewBag.CurrentSort = sortOrder;

            IQueryable<Personel> personelIQ = from s in _context.Personel.Include(p => p.Gemi)
                                              select s;
            switch (sortOrder)
            {
                case "name_desc":
                    personelIQ = personelIQ.OrderByDescending(s => s.Adi);
                    break;
                case "surname_desc":
                    personelIQ = personelIQ.OrderByDescending(s => s.Soyadi);
                    break;
                case "role_desc":
                    personelIQ = personelIQ.OrderByDescending(s => s.Gorev);
                    break;
                case "ship_desc":
                    personelIQ = personelIQ.OrderByDescending(s => s.Gemi.GemiAdi);
                    break;
                case "name":
                    personelIQ = personelIQ.OrderBy(s => s.Adi);
                    break;
                case "surname":
                    personelIQ = personelIQ.OrderBy(s => s.Soyadi);
                    break;
                case "role":
                    personelIQ = personelIQ.OrderBy(s => s.Gorev);
                    break;
                case "ship":
                    personelIQ = personelIQ.OrderBy(s => s.Gemi.GemiAdi);
                    break;
                default:
                    personelIQ = personelIQ.OrderBy(s => s.Adi);
                    break;
            }

            return View(await personelIQ.AsNoTracking().ToListAsync());
        }

        // GET: Personels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personel = await _context.Personel
                .FirstOrDefaultAsync(m => m.PersonelId == id);
            if (personel == null)
            {
                return NotFound();
            }

            return View(personel);
        }

        // GET: Personels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Personels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonelId,Adi,Soyadi,Gorev,GemiId")] Personel personel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(personel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(personel);
        }

        // GET: Personels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personel = await _context.Personel.FindAsync(id);
            if (personel == null)
            {
                return NotFound();
            }
            return View(personel);
        }

        // POST: Personels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PersonelId,Adi,Soyadi,Gorev,GemiId")] Personel personel)
        {
            if (id != personel.PersonelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonelExists(personel.PersonelId))
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
            return View(personel);
        }

        // GET: Personels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personel = await _context.Personel
                .FirstOrDefaultAsync(m => m.PersonelId == id);
            if (personel == null)
            {
                return NotFound();
            }

            return View(personel);
        }

        // POST: Personels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var personel = await _context.Personel.FindAsync(id);
            if (personel != null)
            {
                _context.Personel.Remove(personel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonelExists(int id)
        {
            return _context.Personel.Any(e => e.PersonelId == id);
        }
    }
}
