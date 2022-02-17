using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Voiturage.Models;
using Voiturage.Data;
using Microsoft.EntityFrameworkCore;


namespace Voiturage.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly voiturageContext _db;

    public HomeController(ILogger<HomeController> logger,voiturageContext db)
    {
        _logger = logger;
        _db = db;
    }

    public IActionResult Index()
    {
        ViewData["Cities"] = _db.Villes;
        ViewData["BestRides"] = _db.Trajets.Where(x => x.HeureDepart > DateTime.Now.AddYears(-5)).GroupBy(x => new { x.IdVilleDepart, x.IdVilleArrivee }).Select(x => new BestRides { VilleDepart = x.FirstOrDefault().VilleDepart, VilleArrivee = x.FirstOrDefault().VilleArrivee, prixMini = x.Min(x => x.Prix), nbTrajetPropose = x.Count() }).OrderByDescending(x => x.nbTrajetPropose).Take(3);
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

