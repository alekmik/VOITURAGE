using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Voiturage.Models;

namespace Voiturage.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        byte[] userid = new byte[4];
        bool connected = HttpContext.Session.TryGetValue("UserID", out userid);
        if (connected)
            ViewData["UserID"] = BitConverter.ToInt32(userid);
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

