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
            return View(trips.OrderByDescending(t => t.HeureDepart));
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
            createUsersCitiesList();
            ModelState.Remove("VilleDepart");
            ModelState.Remove("VilleArrivee");
            ModelState.Remove("Chauffeur");
            if (ModelState.IsValid)
            {
                bool dateInputOk = checkInputsTime(trajet.HeureDepart, trajet.HeureArrivee);
                bool citiesInputOK = checkInputsCities(trajet.IdVilleDepart, trajet.IdVilleArrivee);
                if(dateInputOk && citiesInputOK)
                {
                    trajet.PlaceMax = trajet.Place;
                    try
                    {
                        _dbConnect.Trajets.Add(trajet);
                        if (_dbConnect.SaveChanges() > 0)
                        {
                            TempData["success"] = "Trajet ajouté";
                            return RedirectToAction(nameof(Index));
                        }
                        TempData["error"] = "Erreur : Trajet non ajouté";

                    }
                    catch (Exception ex)
                    {
                        return View(trajet);
                    }
                }
            }
            return View(trajet);
        }

        // GET: Trips/Edit/5
        public ActionResult Edit(int id)
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
            createUsersCitiesList();
            return View(trajet);
        }

        // POST: Trips/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Trajet trajet)
        {
            ModelState.Remove("VilleDepart");
            ModelState.Remove("VilleArrivee");
            ModelState.Remove("Chauffeur");
            if (ModelState.IsValid)
            {
                bool dateInputOk = checkInputsTime(trajet.HeureDepart, trajet.HeureArrivee);
                bool citiesInputOK = checkInputsCities(trajet.IdVilleDepart, trajet.IdVilleArrivee);
                if (dateInputOk && citiesInputOK)
                {
                    trajet.PlaceMax = trajet.Place;
                    try
                    {
                        _dbConnect.Update(trajet);
                        if (_dbConnect.SaveChanges() > 0)
                        {
                            TempData["success"] = "Trajet modifié";
                            return RedirectToAction(nameof(Index));
                        }
                        TempData["error"] = "Erreur : Trajet non modifié";
                        return RedirectToAction(nameof(Index));
                    }
                    catch
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                else
                {
                    TempData["error"] = "Saisie des heures ou des villes incohérente.";
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(trajet);
        }

        // GET: Trips/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_dbConnect.Trajets.Find(id));
        }

        // POST: Trips/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Trajet trajet)
        {
            try
            {
                _dbConnect.Trajets.Remove(trajet);
                _dbConnect.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        //Ajoute les données des chauffeurs, des villes d'arrivée et de départ dans les objets de la liste trajets.
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
        //Ajoute les données du chauffeur, de la ville d'arrivée et de départ dans l'objet trajet.
        public Trajet completTrajet(Trajet trajet)
        {
                trajet.Chauffeur = _users.Find(u => u.Id == trajet.IdChauffeur);
                trajet.VilleArrivee = _cities.Find(v => v.Id == trajet.IdVilleArrivee);
                trajet.VilleDepart = _cities.Find(v => v.Id == trajet.IdVilleDepart);
            return trajet;
        }
        //Verifie que l'heure de DEPART est inférieur à l'heure d'ARRIVEE et renvoie un BOOLEEN
        public bool checkInputsTime(DateTime depart, DateTime arrivee)
        {
            return depart < arrivee;
        }
        //Verifie que la ville de DEPART n'est pas égale à la ville d'ARRIVEE et renvoie un BOOLEEN
        public bool checkInputsCities(int depart, int arrivee)
        {
            return depart != arrivee;
        }
        //Crée des ViewBag contenant le liste des utilisateurs et des villes
        public void createUsersCitiesList()
        {
            ViewBag.Villes = _cities;
            ViewBag.Users = _users;
        }
    }
}
