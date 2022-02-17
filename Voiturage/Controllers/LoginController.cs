using Microsoft.AspNetCore.Mvc;
using Voiturage.Data;
using Voiturage.Models;
using System.Text;
using System.Security.Cryptography;
using System.Web;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Voiturage.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly voiturageContext _db;


        public LoginController(ILogger<HomeController> logger,voiturageContext db)
        {
            _logger = logger;
            _db = db;

        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            if(HttpContext.Session.Keys.Contains("UserID"))
            {
                TempData["Error"] = "Vous êtes déjà connecté.";
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(LoginViewModel user)
        {
            if (HttpContext.Session.Keys.Contains("UserID"))
            {
                TempData["Error"] = "Vous êtes déjà connecté.";
                return RedirectToAction("Index", "Home");
            }
            Utilisateur ? theUser = _db.Utilisateurs.FirstOrDefault(x => x.Username == user.Username);
            if(theUser==null)
            {
                TempData["Error"] = "Le nom d'utilisateur ou le mot de passe est incorrect";
                return View();
            }

            string toCompare = ComputeSha256Hash(user.Password+theUser.Salt);

            if(toCompare!=theUser.Password)
            {
                TempData["Error"] = "Le nom d'utilisateur ou le mot de passe est incorrect";
                return View();
            }

            
            TempData["Success"] = "Bienvenue, " + theUser.Username;
            HttpContext.Session.SetInt32("userid", theUser.Id);
            HttpContext.Session.SetInt32("isadmin", theUser.Admin ? 1 :0 );
            HttpContext.Session.SetString("nom", theUser.Nom);
            HttpContext.Session.SetString("prenom", theUser.Prenom);
            HttpContext.Session.SetString("username", theUser.Prenom);
            HttpContext.Session.SetString("photo", theUser.Photo ?? "");
            return RedirectToAction("Index","Home");


        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Utilisateur user)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }
           

            bool incorrect = false;

            if (_db.Utilisateurs.Any(x => x.Username == user.Username))
            {
                TempData["userExist"] = "Ce nom d'utilisateur est déjà utilisé.";
                incorrect = true;
            }

            if (_db.Utilisateurs.Any(x => x.Mail == user.Mail))
            {
                TempData["mailExist"] = "Il y a déjà un compte avec cet e-mail";
                incorrect = true;
            }

            if (Request.Form["confirmpass"]!=user.Password)
            {
                TempData["wrong_confirm"] = "Les mots de passe doivent correspondre";
                incorrect = true;
            }

            if(incorrect)
            {
                return View();
            }

            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789_-@&%^+/.?!$";
            user.Salt = new string(Enumerable.Repeat(chars, 5).Select(s => s[random.Next(s.Length)]).ToArray());
            user.Password = ComputeSha256Hash(user.Password + user.Salt);
            _db.Utilisateurs.Add(user);
            _db.SaveChanges();
            TempData["Success"] = "Vous avez été enregistré avec succès.";
            return RedirectToAction("Index");
        }

        public IActionResult LogOut()
        { 
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
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

