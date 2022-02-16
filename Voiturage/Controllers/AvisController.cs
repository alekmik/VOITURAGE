using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Voiturage.Data;
using Voiturage.Models;

namespace Voiturage.Controllers
{
    public class AvisController : Controller
    {
        private readonly voiturageContext _voiturageContext;
        public AvisController(voiturageContext db)
        {
            _voiturageContext = db;
        }
        // GET: AvisController
        public ActionResult Index()
        {
            return View();
        }

        // GET: AvisController/Details/id
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AvisController/Create
        public ActionResult CreateAvis(int idTrajet)
        {
            ViewData["Title"] = "Votre avis sur le voyage :";

            byte[] userid = new byte[4];
            bool connected = HttpContext.Session.TryGetValue("UserID", out userid);
            if (connected)
            {
                ViewData["User"] = _voiturageContext.Utilisateurs.FirstOrDefault(x => x.Id == BitConverter.ToInt32(userid));
                ViewData["UserID"] = BitConverter.ToInt32(userid);
            }
            /*-------------------------------*/

            TempData["idTrajet"] = idTrajet;
            return View();
        }

        // POST: AvisController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAvis(Avis avis,int idTrajet)
        {
            byte[] userid = new byte[4];
            bool connected = HttpContext.Session.TryGetValue("UserID", out userid);
            if (connected)
            {
                avis.IdNotant = BitConverter.ToInt32(userid);
                avis.IdTrajet = idTrajet;
                Trajet trajet = _voiturageContext.Trajets.Include(t => t.Passagers).FirstOrDefault(t=>t.Id == idTrajet);
                if(trajet == null)
                {
                    TempData["Error"] = "Trajet introuvable";
                    return RedirectToAction("ListTrips", "Profile");
                }
                if(!trajet.Passagers.Any( p => p.Id == avis.IdNotant))
                {
                    TempData["Error"] = "Vous n'avez pas participé à ce trajet !";
                    return RedirectToAction("ListTrips", "Profile");
                }
                avis.IdNote = trajet.IdChauffeur;
            }


            if (ModelState.IsValid)
            {
                _voiturageContext.Avis.Add(avis);
                _voiturageContext.SaveChanges();
            }
            
            return RedirectToAction("ListTrips", "Profile");
            
        }

        // GET: AvisController/Edit/id
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AvisController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AvisController/Delete/id
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AvisController/Delete/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
