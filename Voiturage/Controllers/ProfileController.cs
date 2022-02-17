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
            int? userid = HttpContext.Session.GetInt32("userid");
            if (userid!=null)
            {
                ViewData["User"] = _voiturageContext.Utilisateurs.FirstOrDefault(x => x.Id == userid);
                ViewData["UserID"] = userid;
            }
            /*------------------------------*/
            ViewData["Title"] = "Vos trajets";
            ViewData["Avis"] = _voiturageContext.Avis;
            var users = _voiturageContext.Utilisateurs;
            Utilisateur currentUser = users.Find(ViewData["UserID"]);
            IEnumerable<Trajet> trajets = _voiturageContext.Trajets.Include(t=> t.VilleArrivee).Include(t=>t.VilleDepart).Include(t=>t.Avis).Where(t => t.Passagers.Contains(currentUser) || t.Chauffeur == currentUser).OrderBy(t => t.HeureDepart);
            IEnumerable<Avis> avis = _voiturageContext.Avis.Include(a => a.Trajet).Include(a => a.UtilisateurNotant).Include(a => a.UtilisateurNote);
            ViewBag.trajets = trajets;
            ViewBag.avis = avis;
            

            if(ViewData["UserID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            //var avis = _voiturageContext.Avis;
            //ViewData["Avis"] = avis;

            return View(currentUser);
        }
    }
}
