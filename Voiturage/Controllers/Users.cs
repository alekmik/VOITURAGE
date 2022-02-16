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
            user = setCar(user);
            //Récupération du fichier de la photo
            var photoFile = Request.Form.Files;
            //Sécutisation du mot de passe.
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789_-@&%^+/.?!$";
            user.Salt = new string(Enumerable.Repeat(chars, 5).Select(s => s[random.Next(s.Length)]).ToArray());
            user.Password = ComputeSha256Hash(user.Password+user.Salt);
            try
            {
                if (photoFile.Count() > 0)
                {
                    user.Photo = ChargerFichier(photoFile[0], "");
                }
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
        public ActionResult Edit(int id, Utilisateur user)
        {
            try
            {   
                user.Id = id;
                //Récupére les anciennes données de l'utilisateur et complet l'objet utilisateur avec les données manquantes (password, salt). 
                //Utilisateur oldUser = GetUser(id);
                //user.Password = oldUser.Password;
                //user.Salt = oldUser.Salt;
                user = setCar(user);
                //Récupération du fichier de la photo
                var photoFile = Request.Form.Files;
                //Enregistrement de la photo dans le dossier images
                if (photoFile.Count() > 0)
                {
                    user.Photo = ChargerFichier(photoFile[0], user.Photo);
                }
                //Enregistrement du nouvel utilisateur dans la base de données.
                _dbConnect.Utilisateurs.Update(user);
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
                return View();
            }
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int id)
        {
            return View(GetUser(id));
        }

        // POST: Users/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Utilisateur user)
        {
            try
            {
                _dbConnect.Utilisateurs.Remove(user);
                _dbConnect.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public Utilisateur GetUser(int id)
        {
            Utilisateur user = new Utilisateur();
            try
            {
                user = _dbConnect.Utilisateurs.Find(id);
                user.Voiture = _dbConnect.Voitures.Find(user.IdVoiture);
                return (user);
            }
            catch(Exception ex)
            {
                return user;
            }
            
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

        public Utilisateur setCar(Utilisateur user)
        {
            //Verifie si la voiture est dans la base de données et si elle n'est pas dans la BD l'enregistre.
            Voiture car = _dbConnect.Voitures.FirstOrDefault(x => x.Modele == user.Voiture.Modele);
            if (car != null)
            {
                user.IdVoiture = car.Id;
            }
            else
            {
                _dbConnect.Voitures.Add(user.Voiture);
                _dbConnect.SaveChanges();
            }
            return user;
        }
    }
}
