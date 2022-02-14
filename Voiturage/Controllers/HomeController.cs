using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Voiturage.Models;
using Voiturage.Data;

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
        byte[] userid = new byte[4];
        bool connected = HttpContext.Session.TryGetValue("User", out userid!);
        if (connected)
            ViewData["User"] = _db.Utilisateurs.FirstOrDefault(x=>x.Id==BitConverter.ToInt32(userid));
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

