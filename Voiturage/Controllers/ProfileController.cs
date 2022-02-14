using Microsoft.AspNetCore.Mvc;
using System;
using Voiturage.Data;
using Voiturage.Models;
using Microsoft.EntityFrameworkCore;


namespace Voiturage.Controllers
{
    public class ProfileController : Controller
    {
        private readonly voiturageContext _voiturageContext;

        public ProfileController(voiturageContext db)
        {
            _voiturageContext = db;
        }
        public IActionResult ListTrips(int? id)
        {
            ViewData["Title"] = "Vos trajets";
            var user = _voiturageContext.Utilisateurs;
            Utilisateur currentUser = user.Find(id);
            IEnumerable<Trajet> trajets = _voiturageContext.Trajets.Include(t=> t.VilleArrivee).Include(t=>t.VilleDepart).Where(t => t.Passagers.Contains(currentUser) || t.Chauffeur == currentUser);
            ViewBag.trajets = trajets;
            

            if(id == null || id == 0)
            {
                return RedirectToAction("Home", "Index");
            }
            
            return View(currentUser);
        }
    }
}
