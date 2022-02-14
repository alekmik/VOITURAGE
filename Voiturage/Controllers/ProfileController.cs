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
        /**
         * public IActionResult ListTrips(int? id: user id)
         * 
         */

        public IActionResult ListTrips()
        {
            byte[] userid = new byte[4];
            bool connected = HttpContext.Session.TryGetValue("UserID", out userid);
            if (connected)
                ViewData["UserID"] = BitConverter.ToInt32(userid);
            /*------------------------------*/
            ViewData["Title"] = "Vos trajets";
            var user = _voiturageContext.Utilisateurs;
            Utilisateur currentUser = user.Find(ViewData["UserID"]);
            IEnumerable<Trajet> trajets = _voiturageContext.Trajets.Include(t=> t.VilleArrivee).Include(t=>t.VilleDepart).Where(t => t.Passagers.Contains(currentUser) || t.Chauffeur == currentUser);
            ViewBag.trajets = trajets;
            

            if(ViewData["UserID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            
            return View(currentUser);
        }
    }
}
