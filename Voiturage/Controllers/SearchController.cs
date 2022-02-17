using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Voiturage.Models;
using Voiturage.Data;
using Microsoft.EntityFrameworkCore;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Voiturage.Controllers
{
    public class SearchController : Controller
    {
        private readonly voiturageContext _db;

        public SearchController(voiturageContext db)
        {
            _db = db;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(SearchViewModel search)
        {
            if(ModelState.IsValid)
            {
                return View(_db.Trajets.Include(x=>x.Chauffeur).ThenInclude(x=>x.Notes).Include(x=>x.Chauffeur.Voiture).Include(x=>x.VilleDepart).Include(x=>x.VilleArrivee).Where(x=>x.IdVilleDepart==search.IdVilleDepart && x.IdVilleArrivee==search.IdVilleArrivee && x.HeureDepart.Date==search.DateDepart.Date && x.Place>=search.PlacesRequises));
            }
            TempData["Error"] = "Recherche invalide";
            return RedirectToAction("Index","Home");
        }
    }
}

