using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class YuklersController : Controller
    {
        private readonly IpfinalContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public YuklersController(IpfinalContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Yuklers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Yuklers.ToListAsync());
        }

        // GET: Yuklers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yukler = await _context.Yuklers
                .FirstOrDefaultAsync(m => m.YukId == id);
            if (yukler == null)
            {
                return NotFound();
            }

            return View(yukler);
        }

        // GET: Yuklers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Yuklers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("YukId,YukTuru,Agirlik,UrunPhoto,ImageFile")] Yukler yukler)
        {
            if (ModelState.IsValid)
            {
                string wwwrootpath = _hostEnvironment.WebRootPath;
                string fileName = yukler.ImageFile != null ? Path.GetFileNameWithoutExtension(yukler.ImageFile.FileName) : "default";
                string extension = yukler.ImageFile != null ? Path.GetExtension(yukler.ImageFile.FileName) : ".jpg";
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                yukler.UrunPhoto = "~/Contents/" + fileName;
                string path = Path.Combine(wwwrootpath + "/Contents/", fileName);
                if (yukler.ImageFile != null)
                {
                    try
                    {
                        using (var filestream = new FileStream(path, FileMode.Create))
                        {
                            await yukler.ImageFile.CopyToAsync(filestream);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log the exception or display an error message
                        Console.WriteLine(ex.ToString());

                    }
                }
                _context.Add(yukler);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(yukler);
        }
        // GET: Yuklers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yukler = await _context.Yuklers.FindAsync(id);
            if (yukler == null)
            {
                return NotFound();
            }
            return View(yukler);
        }

        // POST: Yuklers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("YukId,YukTuru,Agirlik,UrunPhoto,ImageFile")] Yukler yukler)
        {
            if (id != yukler.YukId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (yukler.ImageFile != null)
                    {
                        string wwwrootpath = _hostEnvironment.WebRootPath;
                        string fileName = Path.GetFileNameWithoutExtension(yukler.ImageFile.FileName);
                        string extension = Path.GetExtension(yukler.ImageFile.FileName);
                        fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        yukler.UrunPhoto = "~/Contents/" + fileName;
                        string path = Path.Combine(wwwrootpath + "/Contents/", fileName);
                        using (var filestream = new FileStream(path, FileMode.Create))
                        {
                            await yukler.ImageFile.CopyToAsync(filestream);
                        }
                    }

                    _context.Update(yukler);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!YuklerExists(yukler.YukId))
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
            return View(yukler);
        }
        // GET: Yuklers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yukler = await _context.Yuklers
                .FirstOrDefaultAsync(m => m.YukId == id);
            if (yukler == null)
            {
                return NotFound();
            }

            return View(yukler);
        }

        // POST: Yuklers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var yukler = await _context.Yuklers.FindAsync(id);
            if (yukler != null)
            {
                _context.Yuklers.Remove(yukler);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool YuklerExists(int id)
        {
            return _context.Yuklers.Any(e => e.YukId == id);
        }
    }
}
