using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Voiturage.Data;
using Voiturage.Models;

namespace Voiturage.Controllers
{
    public class Users : Controller
    {
        private readonly voiturageContext _dbConnect;

        public Users(voiturageContext dbConnect)
        {
            _dbConnect = dbConnect;
        }

         // GET: Users
        public ActionResult Index()
        {
            IEnumerable<Utilisateur> users = _dbConnect.Utilisateurs;
            return View(users);
        }

        // GET: Users/Details/5
        public ActionResult Details(int id)
        {
            Utilisateur user = _dbConnect.Utilisateurs.Find(id);
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: Users/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Users/Edit/5
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

        // GET: Users/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Users/Delete/5
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
