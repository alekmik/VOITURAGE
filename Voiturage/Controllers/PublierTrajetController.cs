using Microsoft.AspNetCore.Mvc;
using Voiturage.Data;
using Voiturage.Models;

namespace Voiturage.Controllers
{
    public class PublierTrajetController : Controller
    {
        private readonly voiturageContext _db;
        //
        /*constructeur - ctor TAB TAB (fait apparaitre le public suivant*/
        public PublierTrajetController(/*parametre*/voiturageContext db)
        {
            /*fonction*/
            _db = db;
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("userid") == null)
            {
                TempData["Error"] = "Veuillez vous connecter pour ajouter un trajet";
                return RedirectToAction("Index","Home");
            }

            ViewData["AllTheCities"] = _db.Villes; //recherche de la colonne "Ville" dans la database 
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Add(Trajet trajet)
        {
            if (ModelState.IsValid)
            {
                trajet.Place = trajet.PlaceMax;
                _db.Trajets.Add(trajet);
                _db.SaveChanges();//un utilisateur doit être connecté (en attente....)

                TempData["success"] = "le trajet a bien été ajouté";

                return RedirectToAction("Index", "Home");

            }
            return RedirectToAction("Index");
        }
    }
}
