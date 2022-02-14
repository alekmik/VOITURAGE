using System.Security.Cryptography;
using System.Text;
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
            //Verification de l'existance de la voiture dans la base de données.
            List<Voiture> voitures = _dbConnect.Voitures.ToList();
            var query = from c in voitures where c.Marque == user.Voiture.Marque && c.Modele == user.Voiture.Modele select c;
            if(query.Count() > 0)
            {
                Voiture car = query.Single();
                user.IdVoiture = car.Id;
            }
            else
            {
                Voiture car = user.Voiture;
                _dbConnect.Voitures.Add(car);
                _dbConnect.SaveChanges();
                user.IdVoiture = car.Id;    
            }
            //Récupération du fichier de la photo
            var photoFile = Request.Form.Files;
            //Sécutisation du mot de passe.
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789_-@&%^+/.?!$";
            user.Salt = new string(Enumerable.Repeat(chars, 5).Select(s => s[random.Next(s.Length)]).ToArray());
            user.Password = ComputeSha256Hash(user.Password+user.Salt);
            try
            {
                //Enregistrement de la photo dans le dossier images
                user.Photo = ChargerFichier(photoFile[0], "");
                //Enregistrement du nouvel utilisateur dans la base de données.
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
        static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
