
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Voiturage.Data;
using Voiturage.Models;

namespace Voiturage.Controllers
{
    public class Users : Controller
    {
        private readonly voiturageContext _dbConnect;
        private readonly IWebHostEnvironment webHostEnvironment;

        public Users(voiturageContext dbConnect, IWebHostEnvironment webHostEnvironment)
        {
            _dbConnect = dbConnect;
            this.webHostEnvironment = webHostEnvironment;
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
            return View(GetUser(id));
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Utilisateur user)
        {
            var photoFile = Request.Form.Files;
            user.Salt = "fdq";
            try
            {
                user.Photo = ChargerFichier(photoFile[0], "");
                _dbConnect.Utilisateurs.Add(user);
                if (_dbConnect.SaveChanges() > 0)
                {
                    TempData["success"] = "Utilisateur ajouté";
                    return RedirectToAction(nameof(Index));
                }
                TempData["error"] = "Erreur : Utilisateur non ajouté";
                return View(user);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View();
            }
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int id)
        {

            return View(GetUser(id));
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
        public Utilisateur GetUser(int id)
        {
            Utilisateur user = _dbConnect.Utilisateurs.Find(id);
            user.Voiture = _dbConnect.Voitures.Find(user.IdVoiture);
            return (user);
        }
        //Charge la photo dans le dossier images
        private string ChargerFichier(IFormFile photoFile, string existFileName)
        {
            string photo = "";
            if (photoFile != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                if (existFileName == "")
                {
                    photo = Guid.NewGuid().ToString() + "_" + photoFile.FileName;
                }
                else
                {
                    photo = existFileName;
                }
                string filePath = Path.Combine(uploadsFolder, photo);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    photoFile.CopyTo(fileStream);
                }
            }
            return photo;
        }
    }
}
