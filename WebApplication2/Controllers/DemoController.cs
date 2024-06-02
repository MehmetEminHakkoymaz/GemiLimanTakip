

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication2.Models;
using System.Collections.Generic;

public class CascadeController : Controller
{
    private readonly IpfinalContext _context;
    Cascade cd = new Cascade();

    public CascadeController(IpfinalContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        cd = new Cascade(); // Cascade modelini oluşturduk
        var gemiList = _context.Gemilers.ToList();
        var kalkisList = _context.Kalkislars.ToList();
        if (gemiList != null && kalkisList != null)
        {
            cd.GemiList = new SelectList(gemiList, "GemiId", "GemiAdi");
            cd.KalkisList = new SelectList(kalkisList, "KalkisId", "KalkisZamani");
        }
        return View(cd);
    }



    public JsonResult GetKalkis(int GemiId)
    {
        var kalkisList = (from kalkis in _context.Kalkislars
                          where kalkis.GemiId == GemiId
                          select new
                          {
                              Text = kalkis.KalkisZamani.ToString(), // KalkisZamani kullanıldı ve string'e dönüştürüldü
                              Value = kalkis.KalkisId
                          }).ToList();

        return Json(kalkisList, new System.Text.Json.JsonSerializerOptions());
    }
}