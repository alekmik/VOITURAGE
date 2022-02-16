using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Voiturage.Data;
using Voiturage.Models;

namespace Voiturage.Controllers
{
    public class Trips : Controller
    {
        private readonly voiturageContext _dbConnect;
        private readonly List<Ville> _cities;
        private readonly List<Utilisateur> _users;
        public Trips(voiturageContext dbConnect, IWebHostEnvironment webHostEnvironment)
        {
            _dbConnect = dbConnect;
            _cities = _dbConnect.Villes.ToList();
            _users = _dbConnect.Utilisateurs.ToList();
        }
        // GET: Trips
        public ActionResult Index()
        {
            IEnumerable<Trajet> trips = _dbConnect.Trajets;
            trips = completTrajets(trips);
            return View(trips);
        }

        // GET: Trips/Details/5
        public ActionResult Details(int id)
        {
            if (id == null || id == 0)
            {
                return RedirectToAction("Index");
            }
            var trajet = _dbConnect.Trajets.Find(id);

            if (trajet == null)
            {
                return NotFound();
            }
            trajet = completTrajet(trajet);
            return View(trajet);
        }

        // GET: Trips/Create
        public ActionResult Create()
        {
            ViewBag.Villes = _cities;
            ViewBag.Users = _users;
            return View();
        }

        // POST: Trips/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Trajet trajet)
        {
            try
            {
                _dbConnect.Trajets.Add(trajet);
                if (_dbConnect.SaveChanges() > 0)
                {
                    TempData["success"] = "Trajet ajouté";
                    return RedirectToAction(nameof(Index));
                }
                TempData["error"] = "Erreur : Trajet non ajouté";
                return View(trajet);
            }
            catch(Exception ex)
            {
                return View(trajet);
            }
        }

        // GET: Trips/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Trips/Edit/5
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

        // GET: Trips/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Trips/Delete/5
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
        public IEnumerable<Trajet> completTrajets(IEnumerable<Trajet> trajets)
        {
            foreach(Trajet trajet in trajets)
            {

                trajet.Chauffeur = _users.Find(u => u.Id == trajet.IdChauffeur);
                trajet.VilleArrivee = _cities.Find(v => v.Id == trajet.IdVilleArrivee);
                trajet.VilleDepart = _cities.Find(v => v.Id == trajet.IdVilleDepart);
            }

            return trajets;
        }
        public Trajet completTrajet(Trajet trajet)
        {
                trajet.Chauffeur = _users.Find(u => u.Id == trajet.IdChauffeur);
                trajet.VilleArrivee = _cities.Find(v => v.Id == trajet.IdVilleArrivee);
                trajet.VilleDepart = _cities.Find(v => v.Id == trajet.IdVilleDepart);
            return trajet;
        }
    }
}
